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

namespace KIT_ERP.EtcPresentCondition
{
	/// <summary>
	/// BuyingDeliveryPC에 대한 요약 설명입니다.
	/// </summary>
	public class BuyingDeliveryPC : System.Web.UI.Page
	{
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcStartDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcEndDate;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification1;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwgBD_HT;
		protected System.Web.UI.WebControls.Button btnExcel;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter uwgExcel;
		protected System.Web.UI.WebControls.Literal Literal1;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}


			//품목정보 초기화
			//btnInit.Attributes.Add("onClick","ResetTextBox()");

			ItemSearchControl1.RawMaterials = true; //원자재 바인딩
			ItemSearchControl1.Commodity = true; //상품 바인딩
			CSC1.UnitCostDistinction = "구매거래처";

			if(!Page.IsPostBack)
			{

				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			
				//	*************************************************
				//	**  품목분류1 드롭다운리스트... 데이타바인딩... **		
				//	*************************************************
				//코드분류표에서 대분류명이 품목분류1인 소분류명을 가지고 옴.
				string str_ItemClassification1 = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '0210'" ;
				SqlCommand comm_ItemClassification1 = new SqlCommand(str_ItemClassification1,conn);
				SqlDataAdapter da_ItemClassification1 = new SqlDataAdapter(comm_ItemClassification1) ;
				DataSet ds_ItemClassification1 = new DataSet() ;
				da_ItemClassification1.Fill(ds_ItemClassification1);
				
				ddlItemClassification1.DataSource = ds_ItemClassification1;
				ddlItemClassification1.DataTextField = ds_ItemClassification1.Tables[0].Columns[0].ToString();
				ddlItemClassification1.DataValueField = ds_ItemClassification1.Tables[0].Columns[1].ToString();
				ddlItemClassification1.DataBind();
				ddlItemClassification1.Items.Insert(0, "-선택하세요-") ;
				ddlItemClassification1.Items[0].Value = "";
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
			this.uwgBD_HT.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.uwgBD_HT_PageIndexChanged);
			this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			uwgBD_HT.DisplayLayout.Pager.CurrentPageIndex = 1;
			this.uwgBD_HT.DataSource = Search();

			this.uwgBD_HT.DataBind();

			DataSet ds = new DataSet();
			ds = Search();

			decimal TotalCost = 0;
			foreach(DataRow dr in ds.Tables[0].Rows)
			{
				TotalCost += decimal.Parse(dr["TotalCost"].ToString());
			}

			Literal1.Text = "총 금액<b>\" " + TotalCost + " \"</b> 원" ;
		}

		private DataSet Search()
		{
			Search search;
			if(ItemSearchControl1.hdItem.Trim() == "")
				search = new KIT_ERP.Search("BuyingDeliveryPC",ItemSearchControl1.ItemNum,ItemSearchControl1.ItemDrawNum,ItemSearchControl1.ItemName,wdcStartDate, ddlItemClassification1.SelectedItem.Value, wdcEndDate, CSC1.Company, CSC1.BusinessRegistrationNum);
			else
				search = new KIT_ERP.Search("BuyingDeliveryPC",ItemSearchControl1.ItemNum, "", "",wdcStartDate, ddlItemClassification1.SelectedItem.Value, wdcEndDate, CSC1.Company, CSC1.BusinessRegistrationNum);

			return search.DataSet_search();
		}

		

		//그리드에 현제 나타난 항목을 엑셀 파일로 다운로드
		private void btnExcel_Click(object sender, System.EventArgs e)
		{
			// 현재 Grid의 내용을 Excel로 Export
			Infragistics.WebUI.UltraWebGrid.UltraWebGrid  uwgExcelImport = new Infragistics.WebUI.UltraWebGrid.UltraWebGrid();
			uwgExcelImport = uwgBD_HT;
			uwgExcelImport.DisplayLayout.Pager.AllowPaging = false;

			this.uwgBD_HT.DataSource = Search();
			this.uwgBD_HT.DataBind();

			uwgExcel.Export(uwgBD_HT);	
	
			DataSet ds = new DataSet();
			ds = Search();

			decimal TotalCost = 0;
			foreach(DataRow dr in ds.Tables[0].Rows)
			{
				TotalCost += decimal.Parse(dr["TotalCost"].ToString());
			}

			Literal1.Text = "총 금액<b>\" " + TotalCost + " \"</b> 원" ;
		}

		private void uwgBD_HT_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			uwgBD_HT.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			this.uwgBD_HT.DataSource = Search();
			uwgBD_HT.DataBind();
		}
	}
}
