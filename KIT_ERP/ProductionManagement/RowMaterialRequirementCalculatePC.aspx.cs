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

using System.Data.SqlClient;
using Microsoft.Win32;
using System.Configuration;

namespace KIT_ERP.ProductionManagement
{
	/// <summary>
	/// RowMaterialRequirementCalculatePC에 대한 요약 설명입니다.
	/// </summary>
	public class RowMaterialRequirementCalculatePC : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label searchTitle;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwgPP_HT;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcMinDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcMaxDate;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.WebControls.Button btnPre;
		protected System.Web.UI.WebControls.Button btnNow;
		protected System.Web.UI.WebControls.Button btnNext;
		protected System.Web.UI.WebControls.Button btnExcel;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter uwgExcel;
		protected System.Web.UI.WebControls.Button btnCancle;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPresentRowMaterialUsed;
		protected System.Web.UI.HtmlControls.HtmlInputHidden volumNum;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		
		private DataSet dsPP_HT = new DataSet();
		private static string buttonState, buttonEventPosition;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidMinimumGapUsed;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOrderGapInStorehouseUsed;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOrderNonInStorehouseUsed;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSafeyRowMaterialUsed;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOrderRequestStandbyUsed;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification1;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcFromDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcToDate;
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

			if(!Page.IsPostBack)
			{
				btnCancle.Attributes.Add("onclick", "return Confirm('취소하시겠습니까?');");

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
				SqlCommand comm = new SqlCommand("SELECT MAX(MaterialRequirementVolumNum) FROM PP_HT", conn);
				
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
			this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
			this.btnCancle.Click += new System.EventHandler(this.button_Click);
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

					SqlCommand comm = new SqlCommand("SELECT MAX(MaterialRequirementVolumNum) FROM PP_HT", conn);
					
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
					Division = 1;//검색버튼이 눌러지면 0  비디오버튼이 눌러지면 1
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

					// TODO : 취소버튼 클릭시
				case "Cancle" :
					Infragistics.WebUI.UltraWebGrid.UltraWebGrid  uwgCancle = new Infragistics.WebUI.UltraWebGrid.UltraWebGrid();
					uwgCancle = uwgPP_HT;
					uwgCancle.DisplayLayout.Pager.AllowPaging = false;
					switch(buttonState.ToString())
					{
						case "Pre" :
							buttonState = "Pre";
							uwgCancle.DataSource = this.videoButton(this.btnPre, null);
							break;
						case "Now" :
							buttonState = "Now";
							uwgCancle.DataSource = this.videoButton(this.btnNow, null);
							break;
						case "Next" :
							buttonState = "Next";
							uwgCancle.DataSource = this.videoButton(this.btnNext, null);
							break;
						case "Search" :
							buttonState = "Search";
							uwgCancle.DataSource = this.searchButton();
							break;
					}
					uwgCancle.DataBind();

					// 취소객체 생성
					KIT_ERP.Cancel cancle = new KIT_ERP.Cancel(uwgCancle, "RowMaterialRequirementCalculatePC", volumNum.Value);
					int check = cancle.MainRowCancel(1);

					if(check == 0)
					{
						conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
						conn.Open();

						comm = new SqlCommand("SELECT TOP 1 * FROM MRC_HT WHERE VolumNum = @VolumNum", conn);
						
						comm.Parameters.Add("@VolumNum", uwgPP_HT.Rows[0].Cells.FromKey("MaterialRequirementVolumNum").Value);

						SqlDataReader reader = comm.ExecuteReader();

						while(reader.Read())
						{
							hidPresentRowMaterialUsed.Value = reader["RowMaterialQuantityReflection"].ToString();
							hidMinimumGapUsed.Value = reader["MinimumOrderQuantityReflection"].ToString();
							hidOrderGapInStorehouseUsed.Value = reader["OrderGapQuantityReflection"].ToString();
							hidOrderNonInStorehouseUsed.Value = reader["OrderNonInStorehouseReflection"].ToString();
							hidSafeyRowMaterialUsed.Value = reader["SafyRowMaterialQuantityReflection"].ToString();
							hidOrderRequestStandbyUsed.Value = reader["OrderRequestReadyQuantityReflection"].ToString();
						}
						reader.Close();
						conn.Close();

					}
					
					uwgPP_HT.DisplayLayout.Pager.AllowPaging = true;
					
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
							uwgCancle.DataSource = this.searchButton();
							break;
					}
					uwgPP_HT.DataBind();
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

		// TODO : 검색버튼 클릭시
		private DataSet searchButton()
		{
			Division = 0;//검색버튼이 눌러지면 0  비디오버튼이 눌러지면 1
			// 검색객체를 생성
			KIT_ERP.Search search = new KIT_ERP.Search("RowMaterialRequirementCalculatePC", ItemSearchControl1.ItemNum, ItemSearchControl1.ItemDrawNum, ItemSearchControl1.ItemName, wdcMinDate, wdcMaxDate,wdcFromDate,wdcToDate,ddlItemClassification1.SelectedItem.Value);

			buttonState = "Search";
			btnPre.Enabled = true;
			btnNext.Enabled = true;

			if(search.DataSet_search().Tables[0].Rows.Count > 0)
			{
				btnCancle.Enabled = false;
				btnExcel.Enabled = true;
			}
			else
			{
				btnCancle.Enabled = false;
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
					
			switch(button.CommandArgument.ToString())
			{
					// 이전(비디오버튼)버튼 클릭시
				case "Pre" :
					if(buttonEventPosition.ToString() == "gridPageEvent")
						strSQL = "SELECT MaterialRequirementVolumNum FROM PP_HT WHERE MaterialRequirementVolumNum = @VolumNum";
					else
						strSQL = "SELECT MAX(MaterialRequirementVolumNum) FROM PP_HT WHERE MaterialRequirementVolumNum < @VolumNum";
					btnNext.Enabled = true;
					break;
						

					// 현재(비디오버튼)버튼 클릭시
				case "Now" :
					strSQL = "SELECT MAX(MaterialRequirementVolumNum) FROM PP_HT";
					btnPre.Enabled = true;
					btnNext.Enabled = true;
					break;

					// 다음(비디오버튼)버튼 클릭시
				case "Next" :
					if(buttonEventPosition.ToString() == "gridPageEvent")
						strSQL = "SELECT MaterialRequirementVolumNum FROM PP_HT WHERE MaterialRequirementVolumNum = @VolumNum";
					else
						strSQL = "SELECT MIN(MaterialRequirementVolumNum) FROM PP_HT WHERE MaterialRequirementVolumNum > @VolumNum";
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
					SqlCommand comm1 = new SqlCommand("SELECT MIN(MaterialRequirementVolumNum) FROM PP_HT" , conn);
					volumNum.Value = Convert.ToString(comm1.ExecuteScalar());
					btnPre.Enabled = false;
					
						
				}
				else
				{
					if(Temp !="Cancle")
						RegisterStartupScript("","<script>alert('맨 끝입니다!');</script>");
					SqlCommand comm1 = new SqlCommand("SELECT MAX(MaterialRequirementVolumNum) FROM PP_HT" , conn);
					volumNum.Value = Convert.ToString(comm1.ExecuteScalar());
					btnNext.Enabled = false;
				}
			}
			else
				volumNum.Value = Convert.ToString(comm.ExecuteScalar());
				
			conn.Close();

			KIT_ERP.VideoButton videoButton = new VideoButton(uwgPP_HT, "RowMaterialRequirementCalculatePC", volumNum.Value);

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
				btnExcel.Enabled = true;
			}
			else
			{
				btnCancle.Enabled = false;
				btnExcel.Enabled = false;
			}

			return dsPP_HT;
		}


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

		private void btnExcel_Click(object sender, System.EventArgs e)
		{
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
					KIT_ERP.VideoButton videoButton = new VideoButton(uwgPP_HT, "RowMaterialRequirementCalculatePC", volumNum.Value);
					uwgExcelImport.DataSource = videoButton.FindVolum();
				}

				uwgExcelImport.DataBind();
				uwgExcel.Export(uwgExcelImport);

			}
		}
	}
}