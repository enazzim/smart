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
	/// OutSideDeliveryPC에 대한 요약 설명입니다.
	/// </summary>
	public class OutSideDeliveryPC : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DropDownList ddlState;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcStartDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcEndDate;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification1;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwgOSD_HT;
		protected System.Web.UI.WebControls.Literal Literal1;
		protected System.Web.UI.WebControls.Button btnExcel;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter uwgExcel;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}


			ItemSearchControl1.HalfFinishedProducts = true; //반제품 바인딩
			ItemSearchControl1.Products = true; //제품 바인딩
			CSC1.UnitCostDistinction = "외주거래처";

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
			this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.uwgOSD_HT.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.uwgOSD_HT_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private DataSet Search()
		{
			KIT_ERP.Search search;
			if(ItemSearchControl1.hdItem.Trim() == "")
                search = new KIT_ERP.Search("OutSideDeliveryPC",ItemSearchControl1.ItemNum,ItemSearchControl1.ItemDrawNum,ItemSearchControl1.ItemName, wdcStartDate,wdcEndDate,CSC1.Company, CSC1.BusinessRegistrationNum, ddlState.SelectedItem.Value,ddlItemClassification1.SelectedItem.Value);
			else
				search = new KIT_ERP.Search("OutSideDeliveryPC",ItemSearchControl1.ItemNum,"", "", wdcStartDate,wdcEndDate,CSC1.Company, CSC1.BusinessRegistrationNum, ddlState.SelectedItem.Value,ddlItemClassification1.SelectedItem.Value);
			return search.DataSet_search();
		}

		//검색버튼
		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			uwgOSD_HT.DisplayLayout.Pager.CurrentPageIndex = 1;
			this.uwgOSD_HT.DataSource = Search();
			this.uwgOSD_HT.DataBind();

			DataSet ds = new DataSet();
			ds = Search();

			decimal TotalCost = 0;
			foreach(DataRow dr in ds.Tables[0].Rows)
			{
				TotalCost += decimal.Parse(dr["TotalCost"].ToString());
			}

				
			//Literal1.Text = "총 금액<b>\" " + TotalCost + " \"</b> 원" ;
			Literal1.Text = "총 금액<b>\" " + TotalCost.ToString().Substring(0, TotalCost.ToString().IndexOf(".") + 2) + " \"</b> 원" ;
		}

		//현제 그리드에 나타나있는 데이터를 엑셀파일로 다운로드
		private void btnExcel_Click(object sender, System.EventArgs e)
		{
			// 현재 Grid의 내용을 Excel로 Export
			Infragistics.WebUI.UltraWebGrid.UltraWebGrid  uwgExcelImport = new Infragistics.WebUI.UltraWebGrid.UltraWebGrid();
			uwgExcelImport = uwgOSD_HT;
			uwgExcelImport.DisplayLayout.Pager.AllowPaging = false;
			uwgExcelImport.Columns.FromKey("chk").Hidden = true;
			uwgOSD_HT.DataSource = Search();
			uwgExcelImport.DataBind();
			
			uwgExcel.Export(uwgExcelImport);
		}

		//Grid Paging 기능
		private void uwgOSD_HT_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			uwgOSD_HT.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			uwgOSD_HT.DataSource =  Search();
			uwgOSD_HT.DataBind();

			DataSet ds = new DataSet();
			ds =  Search();

			decimal TotalCost = 0;
			foreach(DataRow dr in ds.Tables[0].Rows)
			{
				TotalCost += decimal.Parse(dr["TotalCost"].ToString());
			}

				
			//Literal1.Text = "총 금액<b>\" " + TotalCost + " \"</b> 원" ;
			Literal1.Text = "총 금액<b>\" " + TotalCost.ToString().Substring(0, TotalCost.ToString().IndexOf(".") + 2) + " \"</b> 원" ;
		}
	}
}
