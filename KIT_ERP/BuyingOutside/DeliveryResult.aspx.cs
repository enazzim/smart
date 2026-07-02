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
using Infragistics.WebUI.UltraWebGrid;

namespace KIT_ERP.BuyingOutside
{
	/// <summary>
	/// DeliveryResult에 대한 요약 설명입니다.
	/// </summary>
	public class DeliveryResult : System.Web.UI.Page
	{
		protected Infragistics.WebUI.WebCombo.WebCombo wcCompany;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcFromDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcToDate;
		protected System.Web.UI.WebControls.Button btComSearch;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Button Button1;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			ItemSearchControl1.Commodity = true;
			ItemSearchControl1.Products = true;
			ItemSearchControl1.RawMaterials = true;
			ItemSearchControl1.Phantom = false;
			ItemSearchControl1.HalfFinishedProducts = true;

			if( !IsPostBack )
			{
				this.Load_WebCombo_Bind();				
			}
		}

		/// <summary>
		/// 초기 바인딩
		/// </summary>
		private void Load_WebCombo_Bind()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			// 거래처바인딩
			string strSQL = "SELECT CompanyName, PresidentName, BusinessRegistrationNum, CompanyInfoIndex FROM CI_MT where RecodingState = 1 and (OutSideOrderCompany = 1 or BuyingCompany = 1) order by CompanyName";
			SqlCommand commCom = new SqlCommand(strSQL,conn);
			SqlDataAdapter daCom = new SqlDataAdapter(commCom) ;
			DataSet dsCom = new DataSet() ;
			daCom.Fill(dsCom);

			wcCompany.DataSource = dsCom;
			wcCompany.DataTextField = dsCom.Tables[0].Columns[0].ToString();
			wcCompany.DataValueField = dsCom.Tables[0].Columns[2].ToString();
			wcCompany.DataBind();

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
			this.btComSearch.Click += new System.EventHandler(this.btComSearch_Click);
			this.UltraWebGrid1.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.UltraWebGrid1_PageIndexChanged);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btComSearch_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = 1;
			UltraWebGrid1.DataSource = ComSearch();
			UltraWebGrid1.DataBind();
			UltraWebGrid1.Bands[0].Columns.FromKey("CompanyName").MergeCells = true;
		}

		private DataTable ComSearch()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			SqlDataAdapter adap = new SqlDataAdapter("SPDeliveryResult",conn);
			adap.SelectCommand.Parameters.Add("@ItemNum",ItemSearchControl1.ItemNum);
			if(ItemSearchControl1.hdItem.Trim() == "")
			{
				adap.SelectCommand.Parameters.Add("@ItemDrawNum",ItemSearchControl1.ItemDrawNum);
				adap.SelectCommand.Parameters.Add("@ItemName",ItemSearchControl1.ItemName);
			}
			else
			{
				adap.SelectCommand.Parameters.Add("@ItemDrawNum","");
				adap.SelectCommand.Parameters.Add("@ItemName","");
			}
			if(wcCompany.DataValue == null)
				adap.SelectCommand.Parameters.Add("@BusinessRegistrationNum","");
			else
				adap.SelectCommand.Parameters.Add("@BusinessRegistrationNum",wcCompany.DataValue.ToString());
			adap.SelectCommand.Parameters.Add("@StartDate",wdcFromDate.Text.Trim());
			adap.SelectCommand.Parameters.Add("@EndDate",wdcToDate.Text.Trim());
			adap.SelectCommand.CommandType = CommandType.StoredProcedure;
			
			DataSet ds = new DataSet();
			
			adap.Fill(ds);

			return ds.Tables[0];
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid uwg = new UltraWebGrid();
			uwg = UltraWebGrid1;
			uwg.DisplayLayout.Pager.AllowPaging = false;
			uwg.DataSource = ComSearch();
			uwg.DataBind();
			UltraWebGridExcelExporter1.Export(uwg);
		}

		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			UltraWebGrid1.DataSource = ComSearch();
			UltraWebGrid1.DataBind();
		}
	}
}
