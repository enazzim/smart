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
using Infragistics.WebUI;
using Infragistics.WebUI.WebCombo;
using Infragistics.WebUI.UltraWebGrid;

namespace KIT_ERP.ManagementInfomation
{
	/// <summary>
	/// SalePresentCondition에 대한 요약 설명입니다.
	/// </summary>
	public class SalePresentCondition : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button bt_Search;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser txtStartDate;
		protected Infragistics.WebUI.WebCombo.WebCombo wcCompanyName;
		protected System.Web.UI.WebControls.Button bt_Excel;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser txtEndDate;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected DataSet dataset = new DataSet();

		private void Page_Load(object sender, System.EventArgs e)
		{
			ItemSearchControl1.Commodity = true; //상품 바인딩				
			ItemSearchControl1.Products = true; //제품 바인딩
			ItemSearchControl1.Phantom = true;//펜텀 바인딩

//			if(Session["ID"] == null)
//			{	
//				Session.Abandon();
//				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Homepage/default.htm';</script>");
//			}

			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(!Page.IsPostBack)
			{
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				string str = "SELECT CompanyName, PresidentName, BusinessRegistrationNum, CompanyInfoIndex FROM CI_MT where RecodingState = 1 and ReceiveingOrderCompany = 1 order by CompanyName";
				SqlCommand comm = new SqlCommand(str,conn);
				SqlDataAdapter da = new SqlDataAdapter(comm) ;
				DataSet ds = new DataSet() ;
				da.Fill(ds);

				wcCompanyName.DataSource = ds;
				wcCompanyName.DataTextField = ds.Tables[0].Columns[0].ToString();
				wcCompanyName.DataValueField = ds.Tables[0].Columns[0].ToString();
				wcCompanyName.DataBind();
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void bt_Search_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.DataSource = Search();
			UltraWebGrid1.DataBind();
		}

		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			UltraWebGrid1.DataSource = Search();
			UltraWebGrid1.DataBind();
		}

		private DataSet Search()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			string str = "SPSalePresentCondition";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.CommandType = CommandType.StoredProcedure;
			if(wcCompanyName.DataValue == null)
				comm.Parameters.Add("@BusinessRegistrationNum","");
			else
				comm.Parameters.Add("@BusinessRegistrationNum",'%'+wcCompanyName.DataValue.ToString().Trim()+'%');
			comm.Parameters.Add("@ItemNum",ItemSearchControl1.ItemNum.Trim());
			comm.Parameters.Add("@ItemDrawNum",ItemSearchControl1.ItemDrawNum.Trim());
			comm.Parameters.Add("@ItemName",ItemSearchControl1.ItemName.Trim());
			if(txtStartDate.Text.Trim() == "")
				comm.Parameters.Add("@StartDate","1900-01-01");
			else
				comm.Parameters.Add("@StartDate",txtStartDate.Text.Trim());

			if(txtEndDate.Text.Trim() == "")
				comm.Parameters.Add("@EndDate","2045-12-31");
			else
				comm.Parameters.Add("@EndDate",txtEndDate.Text.Trim());
			//DataSet dataset = new DataSet();
			SqlDataAdapter da = new SqlDataAdapter(comm);
			da.Fill(dataset);

			return dataset;
		}
	}
}
