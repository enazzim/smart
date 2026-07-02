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
	/// ProductionPlanPC에 대한 요약 설명입니다.
	/// </summary>
	public class ProductionPlanPC : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwgPP_HT;
		protected System.Web.UI.WebControls.Button btnExcel;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter uwgExcel;
		protected System.Web.UI.WebControls.Button btnDelete;
		protected System.Web.UI.WebControls.Button btnCancle;
		protected System.Web.UI.WebControls.Button btnStop;
		protected System.Web.UI.WebControls.Button btnPre;
		protected System.Web.UI.WebControls.Button btnNow;
		protected System.Web.UI.WebControls.Button btnNext;
		protected System.Web.UI.WebControls.LinkButton lnkRowUpDate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden ProductionPlanHistoryIndex;
		protected System.Web.UI.WebControls.Label searchTitle;
		
		private DataSet dsPP_HT = new DataSet();
		private static string buttonState, buttonEventPosition;
		protected System.Web.UI.HtmlControls.HtmlInputHidden volumNum;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification1;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcMinDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcMaxDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcStartDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcEndDate;
		protected System.Web.UI.WebControls.DropDownList ddlState;
		private string Temp = "";
		protected static int Division;
	
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
				// 삭제버튼에 대하여 onclick속성을 추가 시킴
				btnDelete.Attributes.Add("onclick", "return Confirm('선택한 품목을 삭제하시겠습니까?');");
				// 취소버튼에 대하여 onclick속성을 추가 시킴
				btnCancle.Attributes.Add("onclick", "return Confirm('선택한 품목을 취소하시겠습니까?');");
				// 중단버튼에 대하여 onclick속성을 추가 시킴
				btnStop.Attributes.Add("onclick", "return Confirm('선택한 품목을 중단하시겠습니까?');");

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
			this.btnPre.Click += new System.EventHandler(this.button_Click);
			this.btnNow.Click += new System.EventHandler(this.button_Click);
			this.btnNext.Click += new System.EventHandler(this.button_Click);
			this.uwgPP_HT.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.uwgPP_HT_PageIndexChanged);
			this.btnExcel.Click += new System.EventHandler(this.button_Click);
			this.lnkRowUpDate.Click += new System.EventHandler(this.lnkRowUpDate_Click);
			this.btnDelete.Click += new System.EventHandler(this.button_Click);
			this.btnCancle.Click += new System.EventHandler(this.button_Click);
			this.btnStop.Click += new System.EventHandler(this.button_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		// TODO : 수정창 이용 수정완료시
		private void lnkRowUpDate_Click(object sender, System.EventArgs e)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			// 생산계획원장의 내용(생산계획수량, 생산시작일)을 업데이트
			string strSQL = @"UPDATE PP_HT SET ProductionPlanQuantity = @productionPlanQuantity, ProductionBeginDate = @productionBeginDate,
								UpdatingPerson = @UpdatingPerson, UpdatingPersonID = @UpdatingPersonID, UpdatingDate = @UpdatingDate
							  WHERE ProductionPlanHistoryIndex = @productionPlanHistoryIndex";
            
			SqlCommand comm = new SqlCommand(strSQL, conn);
			comm.Parameters.Add("@productionPlanQuantity", SqlDbType.Decimal).Value = Decimal.Parse(uwgPP_HT.Rows[int.Parse(ProductionPlanHistoryIndex.Value.Substring(10))].Cells.FromKey("ProductionPlanQuantity").Value.ToString());
			comm.Parameters.Add("@productionBeginDate", SqlDbType.SmallDateTime).Value = DateTime.Parse(uwgPP_HT.Rows[int.Parse(ProductionPlanHistoryIndex.Value.Substring(10))].Cells.FromKey("ProductionBeginDate").Value.ToString());
			comm.Parameters.Add("@productionPlanHistoryIndex", SqlDbType.Int).Value = int.Parse(uwgPP_HT.Rows[int.Parse(ProductionPlanHistoryIndex.Value.Substring(10))].Cells.FromKey("ProductionPlanHistoryIndex").Value.ToString());

			comm.Parameters.Add("@UpdatingPerson", Session["UserName"].ToString());
			comm.Parameters.Add("@UpdatingPersonID",Session["ID"].ToString());
			comm.Parameters.Add("@UpdatingDate", DateTime.Now.ToShortDateString());
			
			SqlTransaction trans= conn.BeginTransaction();
			comm.Transaction = trans;

			try
			{
				comm.ExecuteNonQuery();
				trans.Commit();

				Response.Write("<script language=javascript>");
				Response.Write("alert('수정되었습니다.');");
				Response.Write("</script>");
			}
			catch(Exception message)
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('" + message.Message + "');");
				Response.Write("</script>");
				trans.Rollback();
			}
			finally
			{
				conn.Close();
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
			
		}

		// TODO : 각종버튼 클릭시
		private void button_Click(object sender, System.EventArgs e)
		{
			
			// sender 매개 변수를 이용하여 이벤트를 발생시킨 버튼의 참조를 얻는다.
			Button button = (Button)sender;
			
			// 각 버튼 Click시 버튼의 CommandName에 따라 비디오버튼동작에 대한 객체를 호출한다.
			switch(button.CommandName.ToString())
			{
					// TODO : 초기화버튼 클릭시
				case "Clear" :
					// 생산시작일과 품목명 검색을 초기화
					wdcMinDate.Value = null;
					wdcMaxDate.Value = null;

					//wcbItemName.DataValue = "";
					ItemSearchControl1.ClearTextBox();

					btnPre.Enabled = true;
					btnNext.Enabled = true;
					break;
				
					// TODO : 검색버튼 클릭시
				case "Search" :
					Division = 0;
					SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
					conn.Open();

					SqlCommand comm = new SqlCommand("SELECT MAX(VolumNum) FROM PP_HT where HistorySection != '기종등록'", conn);
					
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
					Division = 1;
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

					// TODO : 삭제버튼 클릭시
				case "Delete" :
					// 삭제객체 생성
					KIT_ERP.Delete delete = new KIT_ERP.Delete(uwgPP_HT, "ProductionPlanPC");
					delete.MainRowDelete();

					
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
					break;

					// TODO : 취소버튼 클릭시
				case "Cancle" :
					// 취소객체 생성
					KIT_ERP.Cancel cancle = new KIT_ERP.Cancel(uwgPP_HT, "ProductionPlanPC", volumNum.Value);
					cancle.MainRowCancel();
					
					uwgPP_HT.DisplayLayout.Pager.AllowPaging = true;

					Temp = "Cancle";
					
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
					break;

					// TODO : 중단버튼 클릭시
				case "Stop" :
					// 중단객체 생성
					KIT_ERP.Stop stop = new KIT_ERP.Stop(uwgPP_HT, "ProductionPlanPC", "1");
					stop.MainTableStop();
					
					this.button_Click(this.btnSearch, null);
					break;
				
					// TODO : 엑셀버튼 클릭시
				case "Excel":

					if(uwgPP_HT.Rows.Count == 0)
					{
						Response.Write("<script language=javascript>");
						Response.Write("alert('저장할 항목이 없습니다!');");
						Response.Write("</script>");
					}
					else
					{
						// 현재 Grid의 내용을 Excel로 Export
						Infragistics.WebUI.UltraWebGrid.UltraWebGrid  uwgExcelImport = new Infragistics.WebUI.UltraWebGrid.UltraWebGrid();
						uwgExcelImport = uwgPP_HT;
						uwgExcelImport.DisplayLayout.Pager.AllowPaging = false;
						uwgExcelImport.Columns.FromKey("chk").Hidden = true;
				
						if(Division == 0)
						{
							uwgExcelImport.DataSource = this.searchButton();
						}
						else
						{
							KIT_ERP.VideoButton videoButton = new VideoButton(uwgPP_HT, "ProductionPlanPC", volumNum.Value);
							uwgExcelImport.DataSource = videoButton.FindVolum();
						}

						uwgExcelImport.DataBind();
						uwgExcel.Export(uwgExcelImport);

					}
			
					
//					// 현재 Grid의 내용을 Excel로 Export
//					Infragistics.WebUI.UltraWebGrid.UltraWebGrid  uwgExcelImport = new Infragistics.WebUI.UltraWebGrid.UltraWebGrid();
//					uwgExcelImport = uwgPP_HT;
//					uwgExcelImport.DisplayLayout.Pager.AllowPaging = false;
//					uwgExcelImport.Columns.FromKey("chk").Hidden = true;
//				switch(buttonState.ToString())
//				{
//					case "Pre" :
//						buttonState = "Pre";
//						uwgExcelImport.DataSource = this.videoButton(this.btnPre, null);
//						break;
//					case "Now" :
//						buttonState = "Now";
//						uwgExcelImport.DataSource = this.videoButton(this.btnNow, null);
//						break;
//					case "Next" :
//						buttonState = "Next";
//						uwgExcelImport.DataSource = this.videoButton(this.btnNext, null);
//						break;
//					case "Search" :
//						buttonState = "Search";
//						uwgExcelImport.DataSource = this.searchButton();
//						break;
//				}
//					uwgExcelImport.DataBind();
//					uwgExcel.Export(uwgExcelImport);
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
			KIT_ERP.Search search = new KIT_ERP.Search(ItemSearchControl1.ItemNum, ItemSearchControl1.ItemDrawNum, ItemSearchControl1.ItemName, wdcMinDate, wdcMaxDate, wdcStartDate, wdcEndDate,ddlItemClassification1.SelectedItem.Value, ddlState.SelectedItem.Value.Trim(),"ProductionPlanPC");

			buttonState = "Search";
			btnPre.Enabled = true;
			btnNext.Enabled = true;

			if(search.DataSet_search().Tables[0].Rows.Count > 0)
			{
				btnCancle.Enabled = false;
				btnDelete.Enabled = true;
				btnExcel.Enabled = true;
				btnStop.Enabled = true;
			}
			else
			{
				btnCancle.Enabled = false;
				btnDelete.Enabled = false;
				btnExcel.Enabled = false;
				btnStop.Enabled = false;
			}

			return search.DataSet_search();
			
		}

		// TODO : 비디오버튼 클릭시
		private DataSet videoButton(object sender, System.EventArgs e)
		{
			// sender 매개 변수를 이용하여 이벤트를 발생시킨 버튼의 참조를 얻는다.
			Button button = (Button)sender;

			//			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\KIT_ERP");
			//			SqlConnection conn = new SqlConnection(registryKey.GetValue("ConnectionString").ToString());
			//			conn.Open();
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
							strSQL = "SELECT MIN(VolumNum) FROM PP_HT where HistorySection != '기종등록'";
							btnPre.Enabled = true;
						}
						else
						{
							strSQL = "SELECT MAX(VolumNum) FROM PP_HT WHERE VolumNum < (SELECT MAX(VolumNum) FROM PP_HT where HistorySection != '기종등록') and HistorySection != '기종등록'";
							btnNext.Enabled = true;
						}
						break;

						// 현재(비디오버튼)버튼 클릭시
					case "Now" :
						strSQL = "SELECT MAX(VolumNum) FROM PP_HT where HistorySection != '기종등록'";
						btnPre.Enabled = true;
						btnNext.Enabled = true;
						break;

						// 다음(비디오버튼)버튼 클릭시
					case "Next" :
						if(buttonState.ToString() == "Next")
						{
							strSQL = "SELECT MAX(VolumNum) FROM PP_HT where HistorySection != '기종등록'";
							btnNext.Enabled = true;
						}
						else
						{
							strSQL = "SELECT MIN(VolumNum) FROM PP_HT WHERE HistorySection != '기종등록' and VolumNum > (SELECT MIN(VolumNum) FROM PP_HT where HistorySection != '기종등록')";
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
							strSQL = "SELECT VolumNum FROM PP_HT WHERE HistorySection != '기종등록' and VolumNum = @VolumNum";
						else
							strSQL = "SELECT MAX(VolumNum) FROM PP_HT WHERE HistorySection != '기종등록' and VolumNum < @VolumNum";
						btnNext.Enabled = true;
						break;
						

						// 현재(비디오버튼)버튼 클릭시
					case "Now" :
						strSQL = "SELECT MAX(VolumNum) FROM PP_HT where HistorySection != '기종등록'";
						btnPre.Enabled = true;
						btnNext.Enabled = true;
						break;

						// 다음(비디오버튼)버튼 클릭시
					case "Next" :
						if(buttonEventPosition.ToString() == "gridPageEvent")
							strSQL = "SELECT VolumNum FROM PP_HT WHERE HistorySection != '기종등록' and VolumNum = @VolumNum";
						else
							strSQL = "SELECT MIN(VolumNum) FROM PP_HT WHERE HistorySection != '기종등록' and VolumNum > @VolumNum";
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
						SqlCommand comm1 = new SqlCommand("SELECT MIN(VolumNum) FROM PP_HT where HistorySection != '기종등록' " , conn);
						volumNum.Value = Convert.ToString(comm1.ExecuteScalar());
						btnPre.Enabled = false;
						
					}
					else
					{
						if(Temp !="Cancle")
							RegisterStartupScript("","<script>alert('맨 끝입니다!');</script>");
						SqlCommand comm1 = new SqlCommand("SELECT MAX(VolumNum) FROM PP_HT where HistorySection != '기종등록' " , conn);
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
				btnCancle.Enabled = true;
				btnDelete.Enabled = false;
				btnExcel.Enabled = true;
				btnStop.Enabled = false;
			}
			else
			{
				btnCancle.Enabled = false;
				btnDelete.Enabled = false;
				btnExcel.Enabled = false;
				btnStop.Enabled = false;
			}

			return dsPP_HT;
		}
	}
}
