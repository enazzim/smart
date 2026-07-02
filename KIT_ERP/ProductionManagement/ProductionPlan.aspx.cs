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

using Microsoft.Win32;
using System.Data.SqlClient;
using System.Configuration;


namespace KIT_ERP.ProductionManagement
{
	/// <summary>
	/// ProductionPlan에 대한 요약 설명입니다.
	/// </summary>
	public class ProductionPlan : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label searchTitle;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Image Image1;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcMinDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcMaxDate;
		protected System.Web.UI.WebControls.Button btnClear;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwgPR_HT;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcStartDate;
		protected System.Web.UI.WebControls.Button Button1;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.CheckBox CheckBox1;
		protected System.Web.UI.WebControls.Button btnOK;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification1;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			ItemSearchControl1.Products = true;
			ItemSearchControl1.HalfFinishedProducts = true;
			ItemSearchControl1.Phantom = true;

			if(!Page.IsPostBack)
			{
				
				// 생산계획에서 생사시작일은 현재 일자의 하루 뒷날을 표시
				wdcStartDate.NullDateLabel = DateTime.Today.AddDays(1.0).ToShortDateString();
				UltraWebGrid1.Visible = false;
				
				if(!Page.IsPostBack)
				{
						//	*************************************************
						//	**  품목분류1 드롭다운리스트... 데이타바인딩... **		
						//	*************************************************
						//코드분류표에서 대분류명이 품목분류1인 소분류명을 가지고 옴.
					SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
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
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.btnClear.Click += new System.EventHandler(this.button_Click);
			this.btnSearch.Click += new System.EventHandler(this.button_Click);
			this.uwgPR_HT.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.uwgPR_HT_PageIndexChanged);
			this.btnOK.Click += new System.EventHandler(this.button_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		// TODO : 각 버튼들의 대한 이벤트 처리루틴
		private void button_Click(object sender, System.EventArgs e)
		{
			// sender 매개 변수를 이용하여 이벤트를 발생시킨 버튼의 참조를 얻는다.
			Button button = (Button)sender;
			
			// 각 버튼 Click시 버튼의 CommandName에 따라 비디오버튼동작에 대한 객체를 호출한다.
			switch(button.CommandName.ToString())
			{
				// TODO : 검색버튼 클릭시
				case "Search" :
					if(uwgPR_HT.Visible)
					{
						// 검색객체 생성
						uwgPR_HT.DisplayLayout.Pager.CurrentPageIndex = 1;
						KIT_ERP.Search search = new KIT_ERP.Search("ProductionPlan", ItemSearchControl1.ItemNum, ItemSearchControl1.ItemDrawNum, ItemSearchControl1.ItemName, wdcMinDate, wdcMaxDate, ddlItemClassification1.SelectedItem.Value);

						uwgPR_HT.DataSource = search.DataSet_search();;
						uwgPR_HT.DataBind();
					}
					else
					{
						Search search = new Search(ItemSearchControl1.ItemNum, ItemSearchControl1.ItemDrawNum, ItemSearchControl1.ItemName,wdcMaxDate,wdcMinDate);//, ddlItemClassification1.SelectedItem.Value);

						UltraWebGrid1.DataSource = search.DataSet_search();;
						UltraWebGrid1.DataBind();
					}
					break;

				// TODO : 초기화버튼 클릭시
				case "Clear" :
					// 생산시작일과 품목명 검색을 초기화
					wdcMinDate.Value = null;
					wdcMaxDate.Value = null;
					ddlItemClassification1.SelectedIndex = 0;					
					ItemSearchControl1.ClearTextBox();
					break;
				
				// TODO : 수립버튼 클릭시
				case "Found" :
					if(uwgPR_HT.Visible == true)
					{
						// 생산계획수립 객체 생성
						KIT_ERP.Register register = new KIT_ERP.Register(uwgPR_HT, wdcStartDate, "ProductionPlan", Session["ID"].ToString());
						register.MainRegistration();
					}
					else
					{
						Register reg = new Register(UltraWebGrid1, wdcStartDate, "ExecutionPlanInfo", Session["ID"].ToString());
						reg.MainRegistration();
					}

						// 검색을 다시 호출한다.(생산계획중 진행된 품목의 대해서는 List의 출력되지 않게 하기 위해)
						this.button_Click(this.btnSearch, null);
					
					break;
			}
		}

		/// <summary>
		/// 실행계획 생산수립버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button1_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.Visible = true;
			uwgPR_HT.Visible = false;
			
			Search search = new Search(ItemSearchControl1.ItemNum, ItemSearchControl1.ItemDrawNum, ItemSearchControl1.ItemName,wdcMinDate,wdcMaxDate);//,ddlItemClassification1.SelectedItem.Value);

			UltraWebGrid1.DataSource = search.DataSet_search();;
			UltraWebGrid1.DataBind();
		}

		private void uwgPR_HT_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			uwgPR_HT.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			KIT_ERP.Search search = new KIT_ERP.Search("ProductionPlan", ItemSearchControl1.ItemNum, ItemSearchControl1.ItemDrawNum, ItemSearchControl1.ItemName, wdcMinDate, wdcMaxDate,ddlItemClassification1.SelectedItem.Value);
			uwgPR_HT.DataSource = search.DataSet_search();;
			uwgPR_HT.DataBind();			
		}
	}
}