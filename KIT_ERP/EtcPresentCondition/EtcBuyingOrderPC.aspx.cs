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
using System.Text;


namespace KIT_ERP.EtcPresentCondition
{
	/// <summary>
	/// EtcBuyingOrderPC에 대한 요약 설명입니다.
	/// </summary>
	public class EtcBuyingOrderPC : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox txtItemName;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wcDeliveryDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wcDeliveryDate1;
		protected System.Web.UI.WebControls.DropDownList ddlState;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Button btnExcel;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			CSC1.UnitCostDistinction = "구매거래처";
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
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
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.UltraWebGrid1.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.UltraWebGrid1_PageIndexChanged);
			this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		/// <summary>
		/// 엑셀저장
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnExcel_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.AllowPaging = false;
			
			SqlDataAdapter adap = new SqlDataAdapter("EtcBuyingOrderPC_Search", ConfigurationSettings.AppSettings["DSN"]);
			adap.SelectCommand.Parameters.Add("@ItemName", SqlDbType.VarChar).Value = txtItemName.Text;
			adap.SelectCommand.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = CSC1.Company;
			adap.SelectCommand.Parameters.Add("@BusinessRegistrationNum", SqlDbType.VarChar).Value = CSC1.BusinessRegistrationNum;
			adap.SelectCommand.Parameters.Add("@DeliveryDemandDate", SqlDbType.VarChar).Value = wcDeliveryDate.Text;
			adap.SelectCommand.Parameters.Add("@DeliveryDemandDate1", SqlDbType.VarChar).Value = wcDeliveryDate1.Text;
			adap.SelectCommand.Parameters.Add("@RecodingState", SqlDbType.VarChar).Value =ddlState.SelectedItem.Value;

			adap.SelectCommand.CommandType = CommandType.StoredProcedure;
			DataSet ds = new DataSet();
			adap.Fill(ds);

			UltraWebGrid1.DataSource = ds.Tables[0].DefaultView;
			UltraWebGrid1.DataBind();

			UltraWebGridExcelExporter1.Export(UltraWebGrid1);
		}

		/// <summary>
		/// 페이지이동
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			
			SqlDataAdapter adap = new SqlDataAdapter("EtcBuyingOrderPC_Search", ConfigurationSettings.AppSettings["DSN"]);
			adap.SelectCommand.Parameters.Add("@ItemName", SqlDbType.VarChar).Value = txtItemName.Text;
			adap.SelectCommand.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = CSC1.Company;
			adap.SelectCommand.Parameters.Add("@BusinessRegistrationNum", SqlDbType.VarChar).Value = CSC1.BusinessRegistrationNum;
			adap.SelectCommand.Parameters.Add("@DeliveryDemandDate", SqlDbType.VarChar).Value = wcDeliveryDate.Text;
			adap.SelectCommand.Parameters.Add("@DeliveryDemandDate1", SqlDbType.VarChar).Value = wcDeliveryDate1.Text;
			adap.SelectCommand.Parameters.Add("@RecodingState", SqlDbType.VarChar).Value =ddlState.SelectedItem.Value;

			adap.SelectCommand.CommandType = CommandType.StoredProcedure;
			DataSet ds = new DataSet();
			adap.Fill(ds);

			UltraWebGrid1.DataSource = ds.Tables[0].DefaultView;
			UltraWebGrid1.DataBind();
		}

		/// <summary>
		/// 검색버튼
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = 1;
			string[] strArrValue = new string[6];
			strArrValue[0] = txtItemName.Text;
			strArrValue[1] = CSC1.Company;
			strArrValue[2] = CSC1.BusinessRegistrationNum;
			strArrValue[3] = wcDeliveryDate.Text;
			strArrValue[4] = wcDeliveryDate1.Text;
			strArrValue[5] = ddlState.SelectedItem.Value;

			this.DataBind(strArrValue);
		}
		private void DataBind(string[] strArrValue)
		{
			SqlDataAdapter adap = new SqlDataAdapter("EtcBuyingOrderPC_Search", ConfigurationSettings.AppSettings["DSN"]);
			adap.SelectCommand.Parameters.Add("@ItemName", SqlDbType.VarChar).Value = strArrValue[0];
			adap.SelectCommand.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = strArrValue[1];
			adap.SelectCommand.Parameters.Add("@BusinessRegistrationNum", SqlDbType.VarChar).Value = strArrValue[2];
			adap.SelectCommand.Parameters.Add("@DeliveryDemandDate", SqlDbType.VarChar).Value = strArrValue[3] ;
			adap.SelectCommand.Parameters.Add("@DeliveryDemandDate1", SqlDbType.VarChar).Value = strArrValue[4] ;
			adap.SelectCommand.Parameters.Add("@RecodingState", SqlDbType.VarChar).Value = strArrValue[5];

			adap.SelectCommand.CommandType = CommandType.StoredProcedure;
			DataSet ds = new DataSet();
			adap.Fill(ds);

			UltraWebGrid1.DataSource = ds.Tables[0].DefaultView;
			UltraWebGrid1.DataBind();
		}
	}
}
