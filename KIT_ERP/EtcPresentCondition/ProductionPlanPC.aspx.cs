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

namespace KIT_ERP.EtcPresentCondition
{
	/// <summary>
	/// ProductionPlanPC에 대한 요약 설명입니다.
	/// </summary>
	public class ProductionPlanPC : System.Web.UI.Page
	{
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcMinDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcMaxDate;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.WebControls.Button btnPre;
		protected System.Web.UI.WebControls.Button btnNow;
		protected System.Web.UI.WebControls.Button btnNext;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwgPP_HT;
		protected System.Web.UI.WebControls.Button btnExcel;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter uwgExcel;
		protected System.Web.UI.HtmlControls.HtmlInputHidden volumNum;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification1;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;

		private DataSet dsPP_HT = new DataSet();
		private static string buttonState, buttonEventPosition;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcStartDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcEndDate;
		protected System.Web.UI.WebControls.DropDownList ddlState;
		protected System.Web.UI.HtmlControls.HtmlInputHidden ProductionPlanHistoryIndex;
		private string Temp = "";
	
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
				//	*************************************************
				//	**  품목분류1 드롭다운리스트... 데이타바인딩... **		
				//	*************************************************
				//코드분류표에서 대분류명이 품목분류1인 소분류명을 가지고 옴.
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				conn.Open();
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


				///////////////////////////////////////
				// 비디오버튼을 위해
				// lb_vol에 볼륨번호 최고값을 넣어둔다.
				//////////////////////////////////////
				
				// 비디오버튼 클릭시 원장에서 가장큰 인덱스 번호를 가져옴
				SqlCommand comm = new SqlCommand("SELECT MAX(VolumNum) FROM PP_HT", conn);
				
				if(!Convert.IsDBNull(comm.ExecuteScalar()))
					volumNum.Value = Convert.ToString(comm.ExecuteScalar());
				conn.Close();
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
			this.btnSearch.Click += new System.EventHandler(this.button_Click);
			this.uwgPP_HT.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.uwgPP_HT_PageIndexChanged);
			this.btnExcel.Click += new System.EventHandler(this.button_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void button_Click(object sender, System.EventArgs e)
		{
			
			// sender 매개 변수를 이용하여 이벤트를 발생시킨 버튼의 참조를 얻는다.
			Button button = (Button)sender;
			
			// 각 버튼 Click시 버튼의 CommandName에 따라 비디오버튼동작에 대한 객체를 호출한다.
			switch(button.CommandName.ToString())
			{
					// TODO : 검색버튼 클릭시
				case "Search" :
					SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
					conn.Open();

					SqlCommand comm = new SqlCommand("SELECT MAX(VolumNum) FROM PP_HT", conn);
					
					if(!Convert.IsDBNull(comm.ExecuteScalar()))
						volumNum.Value = Convert.ToString(comm.ExecuteScalar());
					conn.Close();
					
					uwgPP_HT.DisplayLayout.Pager.CurrentPageIndex = 1;
					uwgPP_HT.DataSource = searchButton();
					uwgPP_HT.DataBind();
					uwgPP_HT.Columns[0].AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.Yes;
					
					break;

					// TODO : 비디오버튼 클릭시
				case "Video"	:
					buttonEventPosition = "buttonEvent";
					uwgPP_HT.DisplayLayout.Pager.CurrentPageIndex = 1;
					
				switch(button.CommandArgument.ToString())
				{
					case "Pre" :
						buttonState = "Pre";
						uwgPP_HT.DataSource = this.videoButton(this.btnPre, null);
						break;
					case "Now" :
						buttonState = "Now";
						uwgPP_HT.DataSource = this.videoButton(this.btnNow, null);
						break;
					case "Next" :
						buttonState = "Next";
						uwgPP_HT.DataSource = this.videoButton(this.btnNext, null);
						break;
				}
					uwgPP_HT.DataBind();
					uwgPP_HT.Columns[0].AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No;
					
					break;
					// TODO : 엑셀버튼 클릭시
				case "Excel":
					// 현재 Grid의 내용을 Excel로 Export
					Infragistics.WebUI.UltraWebGrid.UltraWebGrid  uwgExcelImport = new Infragistics.WebUI.UltraWebGrid.UltraWebGrid();
					uwgExcelImport = uwgPP_HT;
					uwgExcelImport.DisplayLayout.Pager.AllowPaging = false;
					uwgExcelImport.Columns.FromKey("chk").Hidden = true;
				switch(buttonState.ToString())
				{
					case "Pre" :
						buttonState = "Pre";
						uwgExcelImport.DataSource = this.videoButton(this.btnPre, null);
						break;
					case "Now" :
						buttonState = "Now";
						uwgExcelImport.DataSource = this.videoButton(this.btnNow, null);
						break;
					case "Next" :
						buttonState = "Next";
						uwgExcelImport.DataSource = this.videoButton(this.btnNext, null);
						break;
					case "Search" :
						buttonState = "Search";
						uwgExcelImport.DataSource = this.searchButton();
						break;
				}
					uwgExcelImport.DataBind();
					uwgExcel.Export(uwgExcelImport);
					break;
			}
		}

		// TODO : 그리드의 페이지 변경시
		private void uwgPP_HT_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			buttonEventPosition = "gridPageEvent";
			uwgPP_HT.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			
			switch(buttonState.ToString())
			{
				case "Pre" :
					buttonState = "Pre";
					uwgPP_HT.DataSource = this.videoButton(this.btnPre, null);
					break;
				case "Now" :
					buttonState = "Now";
					uwgPP_HT.DataSource = this.videoButton(this.btnNow, null);
					break;
				case "Next" :
					buttonState = "Next";
					uwgPP_HT.DataSource = this.videoButton(this.btnNext, null);
					break;
				case "Search" :
					buttonState = "Search";
					uwgPP_HT.DataSource = this.searchButton();
					break;
			}
			uwgPP_HT.DataBind();
		}

		// TODO : 검색버튼 클릭시
		private DataSet searchButton()
		{
			// 검색객체를 생성
			KIT_ERP.Search search;
			if(ItemSearchControl1.hdItem.Trim() == "")
				search = new KIT_ERP.Search("ProductionPlanPC", ItemSearchControl1.ItemNum, ItemSearchControl1.ItemDrawNum, ItemSearchControl1.ItemName, wdcMinDate, wdcMaxDate,ddlItemClassification1.SelectedItem.Value);
			else
				search = new KIT_ERP.Search("ProductionPlanPC", ItemSearchControl1.ItemNum, "", "", wdcMinDate, wdcMaxDate,ddlItemClassification1.SelectedItem.Value);

			buttonState = "Search";
			btnPre.Enabled = true;
			btnNext.Enabled = true;

			if(search.DataSet_search().Tables[0].Rows.Count > 0)
			{
				btnExcel.Enabled = true;
			}
			else
			{
				btnExcel.Enabled = false;
			}

			return search.DataSet_search();
			
		}

		// TODO : 비디오버튼 클릭시
		private DataSet videoButton(object sender, System.EventArgs e)
		{
			// sender 매개 변수를 이용하여 이벤트를 발생시킨 버튼의 참조를 얻는다.
			Button button = (Button)sender;
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string strSQL = "";
					
			if(volumNum.Value == "0")
			{
				switch(button.CommandArgument.ToString())
				{
						// 이전(비디오버튼)버튼 클릭시
					case "Pre" :
						if(buttonState.ToString() == "Pre")
						{
							strSQL = "SELECT MIN(VolumNum) FROM PP_HT";
							btnPre.Enabled = true;
						}
						else
						{
							strSQL = "SELECT MAX(VolumNum) FROM PP_HT WHERE VolumNum < (SELECT MAX(VolumNum) FROM PP_HT)";
							btnNext.Enabled = true;
						}
						break;

						// 현재(비디오버튼)버튼 클릭시
					case "Now" :
						strSQL = "SELECT MAX(VolumNum) FROM PP_HT";
						btnPre.Enabled = true;
						btnNext.Enabled = true;
						break;

						// 다음(비디오버튼)버튼 클릭시
					case "Next" :
						if(buttonState.ToString() == "Next")
						{
							strSQL = "SELECT MAX(VolumNum) FROM PP_HT";
							btnNext.Enabled = true;
						}
						else
						{
							strSQL = "SELECT MIN(VolumNum) FROM PP_HT WHERE VolumNum > (SELECT MIN(VolumNum) FROM PP_HT)";
							btnPre.Enabled = true;
						}
						break;
				}

				SqlCommand comm = new SqlCommand(strSQL, conn);
				if(Convert.IsDBNull(comm.ExecuteScalar()))
					RegisterStartupScript("","<script>alert('볼륨번호가 없습니다!');</script>");
				else
					volumNum.Value = Convert.ToString(comm.ExecuteScalar());
			}
			else
			{
				switch(button.CommandArgument.ToString())
				{
						// 이전(비디오버튼)버튼 클릭시
					case "Pre" :
						if(buttonEventPosition.ToString() == "gridPageEvent")
							strSQL = "SELECT VolumNum FROM PP_HT WHERE VolumNum = @VolumNum";
						else
							strSQL = "SELECT MAX(VolumNum) FROM PP_HT WHERE VolumNum < @VolumNum";
						btnNext.Enabled = true;
						break;
						

						// 현재(비디오버튼)버튼 클릭시
					case "Now" :
						strSQL = "SELECT MAX(VolumNum) FROM PP_HT";
						btnPre.Enabled = true;
						btnNext.Enabled = true;
						break;

						// 다음(비디오버튼)버튼 클릭시
					case "Next" :
						if(buttonEventPosition.ToString() == "gridPageEvent")
							strSQL = "SELECT VolumNum FROM PP_HT WHERE VolumNum = @VolumNum";
						else
							strSQL = "SELECT MIN(VolumNum) FROM PP_HT WHERE VolumNum > @VolumNum";
						btnPre.Enabled = true;
						break;
						
				}

				SqlCommand comm = new SqlCommand(strSQL, conn);
				comm.Parameters.Add("@VolumNum", SqlDbType.Int).Value = int.Parse(volumNum.Value);
				
						
				if(Convert.IsDBNull(comm.ExecuteScalar()))
				{
					if(button.CommandArgument.ToString() == "Pre")
					{
						if(Temp !="Cancle")
							RegisterStartupScript("","<script>alert('맨 처음입니다!');</script>");
						SqlCommand comm1 = new SqlCommand("SELECT MIN(VolumNum) FROM PP_HT" , conn);
						volumNum.Value = Convert.ToString(comm1.ExecuteScalar());
						btnPre.Enabled = false;
						
					}
					else
					{
						if(Temp !="Cancle")
							RegisterStartupScript("","<script>alert('맨 끝입니다!');</script>");
						SqlCommand comm1 = new SqlCommand("SELECT MAX(VolumNum) FROM PP_HT" , conn);
						volumNum.Value = Convert.ToString(comm1.ExecuteScalar());
						btnNext.Enabled = false;
					}
				}
				else
					volumNum.Value = Convert.ToString(comm.ExecuteScalar());
				
			}
			conn.Close();

			KIT_ERP.VideoButton videoButton = new VideoButton(uwgPP_HT, "ProductionPlanPC", volumNum.Value);

			switch(button.CommandArgument.ToString())
			{
					// 이전(비디오버튼)버튼 클릭시
				case "Pre" :
					dsPP_HT = videoButton.FindVolum();
					buttonState = "Pre";
					break;

					// 현재(비디오버튼)버튼 클릭시
				case "Now" :
					dsPP_HT = videoButton.FindVolum();
					buttonState = "Now";
					break;

					// 다음(비디오버튼)버튼 클릭시
				case "Next" :
					dsPP_HT = videoButton.FindVolum();
					buttonState = "Next";
					break;
			}
			
			if(dsPP_HT.Tables[0].Rows.Count > 0)
			{
				btnExcel.Enabled = true;
				
			}
			else
			{
				btnExcel.Enabled = false;
			}

			return dsPP_HT;
		}

		
	}
}
