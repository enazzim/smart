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
	/// QualityIndex에 대한 요약 설명입니다.
	/// </summary>
	public class QualityIndex : System.Web.UI.Page
	{
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Button btnInit;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcStartDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcEndDate;
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;

	
		//페이지 로드시
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
				Session["id"] = "admin";
				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 협력사정보 > 품질지표';</script>");

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
			this.UltraWebGrid1.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.UltraWebGrid1_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

	
		//검색버튼 클릭시
		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			string strItemName = ItemSearchControl1.ItemName;
			string strStartDate = wdcStartDate.Text;
			string strEndDate =  wdcEndDate.Text;
			
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			SqlDataAdapter da = new SqlDataAdapter("QualityIndex_Search",conn);
			da.SelectCommand.CommandType = CommandType.StoredProcedure;
			da.SelectCommand.Parameters.Add("@ItemName_arg",SqlDbType.VarChar).Value = strItemName;
			da.SelectCommand.Parameters.Add("@startDate",SqlDbType.VarChar).Value = strStartDate;
			da.SelectCommand.Parameters.Add("@endDate",SqlDbType.VarChar).Value = strEndDate;

			DataSet ds = new DataSet();
			da.Fill(ds);
			this.UltraWebGrid1.DataSource = ds.Tables[0];
			this.UltraWebGrid1.DataBind();		
			
		}


		//그리드 페이징기능 ...아래의 페이지 버튼을 눌렀을때...
		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			
			string strItemName = ItemSearchControl1.ItemName;
			string strStartDate = wdcStartDate.Text;
			string strEndDate =  wdcEndDate.Text;
			
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			SqlDataAdapter da = new SqlDataAdapter("QualityIndex_Search",conn);
			da.SelectCommand.CommandType = CommandType.StoredProcedure;
			da.SelectCommand.Parameters.Add("@ItemName_arg",SqlDbType.VarChar).Value = strItemName;
			da.SelectCommand.Parameters.Add("@startDate",SqlDbType.VarChar).Value = strStartDate;
			da.SelectCommand.Parameters.Add("@endDate",SqlDbType.VarChar).Value = strEndDate;

			DataSet ds = new DataSet();
			da.Fill(ds);
			this.UltraWebGrid1.DataSource = ds.Tables[0];
			this.UltraWebGrid1.DataBind();		
		}

		//초기화 버튼 클릭시
		private void btnInit_Click(object sender, System.EventArgs e)
		{
			
			this.wdcStartDate.Value = null;
			this.wdcEndDate.Value = null;
		}
	}
}
