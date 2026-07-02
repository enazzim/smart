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

namespace KIT_ERP.BasisInformation
{
	/// <summary>
	/// UpdateUnitCostHistory에 대한 요약 설명입니다.
	/// </summary>
	public class UpdateUnitCostHistory : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label searchTitle;
		protected System.Web.UI.WebControls.Button bt_Search;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Label Label2;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcEndDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcBeginDate;
		protected System.Web.UI.WebControls.DropDownList ddlUserName;
		protected System.Web.UI.WebControls.Button Button1;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			if(!Page.IsPostBack)
			{
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				conn.Open();
				
				//	*************************************************
				//	**  대표공정 드롭다운리스트... 데이타바인딩... ** 
				//	*************************************************
				//코드분류표에서 대분류명이 단위인 소분류명을 가지고 옴.
				string str_Process = "Select Name, [ID] from UI_MT where RecodingState = 1 order by Name";
				SqlCommand comm_Process = new SqlCommand(str_Process,conn);
				SqlDataAdapter da_Process = new SqlDataAdapter(comm_Process) ;
				DataSet ds_Process = new DataSet() ;
				da_Process.Fill(ds_Process);
				
				ddlUserName.DataSource = ds_Process;
				ddlUserName.DataTextField = ds_Process.Tables[0].Columns[0].ToString();
				ddlUserName.DataValueField = ds_Process.Tables[0].Columns[1].ToString();
				ddlUserName.DataBind();
				ddlUserName.Items.Insert(0, "-선택-") ;
				ddlUserName.Items[0].Value = "";
				
				conn.Close();
			}

			ItemSearchControl1.Commodity = true; //상품 바인딩
			ItemSearchControl1.RawMaterials = true; //원자재 바인딩	
			//ItemSearchControl1.Phantom = true; //팬텀 바인딩	
			ItemSearchControl1.HalfFinishedProducts =  true; //반제품 바인딩	
			ItemSearchControl1.Products = true; //제품 바인딩
			ItemSearchControl1.IsDoPost = false;
			CSC1.UnitCostDistinction = "";
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
			this.UltraWebGrid1.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.UltraWebGrid1_PageIndexChanged);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.bt_Search.Click += new System.EventHandler(this.bt_Search_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void bt_Search_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = 1;

			UltraWebGrid1.DataSource = Search();
			UltraWebGrid1.DataBind();
		
		}

		private DataSet Search()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			SqlCommand comm = new SqlCommand("SPUpdateReasonUCI",conn);
			comm.CommandType=CommandType.StoredProcedure;
			comm.Parameters.Add("@ItemNum",ItemSearchControl1.ItemNum.Trim());
			comm.Parameters.Add("@ItemDrawNum",ItemSearchControl1.ItemDrawNum.Trim());
			comm.Parameters.Add("@ItemName",ItemSearchControl1.ItemName.Trim());
			comm.Parameters.Add("@BeginDate",wdcBeginDate.Text.Trim());
			comm.Parameters.Add("@EndDate",wdcEndDate.Text.Trim());
			comm.Parameters.Add("@CompanyName",CSC1.Company.Trim());
			comm.Parameters.Add("@UserName",ddlUserName.SelectedItem.Value.Trim());
			comm.Parameters.Add("@BusinessRegistrationNum",CSC1.BusinessRegistrationNum.Trim());

			SqlDataAdapter da = new SqlDataAdapter(comm);

			DataSet ds = new DataSet();

			da.Fill(ds);

			return ds;
		}

		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			UltraWebGrid1.DataSource = Search();
			UltraWebGrid1.DataBind();
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.DataSource = Search();
			UltraWebGrid1.DisplayLayout.Pager.AllowPaging = false;
			UltraWebGrid1.DataBind();
			UltraWebGridExcelExporter1.Export(UltraWebGrid1);
		}		
	}
}
