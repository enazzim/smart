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
	/// RowMaterialRequriementPC에 대한 요약 설명입니다.
	/// </summary>
	public class RowMaterialRequriementPC : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.WebControls.Button btnExcel;
		protected System.Web.UI.WebControls.Button btnPre;
		protected System.Web.UI.WebControls.Button btnNow;
		protected System.Web.UI.WebControls.Button btnNext;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter uwgExcel;
		protected System.Web.UI.HtmlControls.HtmlInputHidden HistoryIndex;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwgBR_HT;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;

		private DataSet dsBR_HT = new DataSet();
		private static string buttonState, buttonEventPosition;
		protected System.Web.UI.HtmlControls.HtmlInputHidden volumNum;
		protected System.Web.UI.HtmlControls.HtmlInputHidden chkAll;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtOrderQuantity;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification1;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcFromDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcToDate;
		protected System.Web.UI.WebControls.DropDownList ddlState;
		protected string Temp = "";
		
		
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			ItemSearchControl1.RawMaterials = true;
			CSC1.UnitCostDistinction="구매거래처";

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


				// 비디오버튼 클릭시 원장에서 가장큰 인덱스 번호를 가져옴
				SqlCommand comm = new SqlCommand("SELECT MAX(VolumNum) FROM BR_HT Where PropertyClassification = '원자재'", conn);
				
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
			this.uwgBR_HT.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.uwgBR_HT_PageIndexChanged);
			this.btnExcel.Click += new System.EventHandler(this.button_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		// TODO : 각종버튼 클릭시
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

					SqlCommand comm = new SqlCommand("SELECT MAX(VolumNum) FROM BR_HT where PropertyClassification = '원자재'", conn);
					
					if(!Convert.IsDBNull(comm.ExecuteScalar()))
						volumNum.Value = Convert.ToString(comm.ExecuteScalar());
					conn.Close();
					
					uwgBR_HT.DisplayLayout.Pager.CurrentPageIndex = 1;
					uwgBR_HT.DataSource = searchButton();
					uwgBR_HT.DataBind();
					uwgBR_HT.Columns[0].AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.Yes;
					
					break;

					// TODO : 비디오버튼 클릭시
				case "Video"	:
					buttonEventPosition = "buttonEvent";
					uwgBR_HT.DisplayLayout.Pager.CurrentPageIndex = 1;
					
				switch(button.CommandArgument.ToString())
				{
					case "Pre" :
						buttonState = "Pre";
						uwgBR_HT.DataSource = this.videoButton(this.btnPre, null);
						break;
					case "Now" :
						buttonState = "Now";
						uwgBR_HT.DataSource = this.videoButton(this.btnNow, null);
						break;
					case "Next" :
						buttonState = "Next";
						uwgBR_HT.DataSource = this.videoButton(this.btnNext, null);
						break;
				}
					uwgBR_HT.DataBind();
					uwgBR_HT.Columns[0].AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No;
					
					break;
				
					// TODO : 엑셀버튼 클릭시
				case "Excel":
					// 현재 Grid의 내용을 Excel로 Export
					Infragistics.WebUI.UltraWebGrid.UltraWebGrid  uwgExcelImport = new Infragistics.WebUI.UltraWebGrid.UltraWebGrid();
					uwgExcelImport = uwgBR_HT;
					uwgExcelImport.DisplayLayout.Pager.AllowPaging = false;
					uwgExcelImport.Columns.FromKey("chk").Hidden = true;
				switch(buttonState.ToString())
				{
					case "Pre" :
						buttonState = "Pre";
						uwgBR_HT.DataSource = this.videoButton(this.btnPre, null);
						break;
					case "Now" :
						buttonState = "Now";
						uwgBR_HT.DataSource = this.videoButton(this.btnNow, null);
						break;
					case "Next" :
						buttonState = "Next";
						uwgBR_HT.DataSource = this.videoButton(this.btnNext, null);
						break;
					case "Search" :
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
			// 검색객체를 생성
			//KIT_ERP.Search search = new KIT_ERP.Search("RowMaterialRequriementPC", ItemSearchControl1.ItemNum, ItemSearchControl1.ItemDrawNum, ItemSearchControl1.ItemName, wdcMinDate, wdcMaxDate, wdcFromDate, wdcToDate, ddlItemClassification1.SelectedItem.Value);
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			
			string str = @"SELECT BR_HT.ItemNum, BR_HT.ItemDrawNum, BR_HT.ItemName, CI_MT.CompanyName, II_MT.Standard, BR_HT.PropertyClassification, BuyingRequestSourceCode, BuyingRequestSource, FirstDeliveryDemandQuantity, FirstDeliveryDemandDate, SecondDeliveryDemandQuantity, SecondDeliveryDemandDate, ThirdDeliveryDemandQuantity, ThirdDeliveryDemandDate, FourthDeliveryDemandQuantity, FourthDeliveryDemandDate, FifthDeliveryDemandQuantity, FifthDeliveryDemandDate, OrderQuantity, UCI_MT.StandardUnitCost as ApplyUnitCost,(UCI_MT.StandardUnitCost * FirstDeliveryDemandQuantity) as TotalCost, VolumNum, ProgressCondition, BR_HT.RegistrationPerson, BR_HT.RegistrationPersonID, BR_HT.RegistrationDate, BR_HT.UpdatingPerson, BR_HT.UpdatingPersonID, BR_HT.UpdatingDate, HistoryIndex, HistorySection, BuyingRequestHistoryIndex FROM BR_HT
				Inner join II_MT on BR_HT.ItemNum = II_MT.ItemNum 				
				inner join UCI_MT on II_MT.ItemNum = UCI_MT.ItemNum
				inner join CI_MT on UCI_MT.BusinessRegistrationNum = CI_MT.BusinessRegistrationNum
				WHERE CI_MT.RecodingState = 1 and BR_HT.PropertyClassification = '원자재' and  II_MT.RecodingState = 1 and CI_MT.RecodingState =1 and UCI_MT.RecodingState = 1 and UCI_MT.UnitCostDistinction = '구매단가' 
				and UCI_MT.OrderRate = (Select Max(OrderRate) From UCI_MT where ItemNum like @ItemNum and UnitCostDistinction = '구매단가')				
				and BR_HT.ItemNum like @ItemNum and BR_HT.ItemDrawNum like @ItemDrawNum and BR_HT.ItemName like @ItemName
				and CI_MT.CompanyName Like @CompanyName
				and CI_MT.BusinessRegistrationNum Like @BusinessRegistrationNum
				and ItemClassification1 Like @ItemClassification
				and FirstDeliveryDemandDate >= @begin and FirstDeliveryDemandDate <= @end
				and BR_HT.ProgressCondition Like @ProgressCondition
				order by BuyingRequestHistoryIndex desc";

			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = str;
			comm.Parameters.Add("@ItemNum", "%"+ItemSearchControl1.ItemNum+"%");
			if(ItemSearchControl1.hdItem.Trim() == "")
			{
				comm.Parameters.Add("@ItemDrawNum", "%"+ItemSearchControl1.ItemDrawNum+"%");
				comm.Parameters.Add("@ItemName", "%"+ItemSearchControl1.ItemName+"%");
			}
			else
			{
				comm.Parameters.Add("@ItemDrawNum", "%%");
				comm.Parameters.Add("@ItemName", "%%");
			}
			comm.Parameters.Add("@CompanyName", "%"+CSC1.Company+"%");
			comm.Parameters.Add("@BusinessRegistrationNum", "%"+CSC1.BusinessRegistrationNum+"%");
			comm.Parameters.Add("@ItemClassification", "%"+ddlItemClassification1.SelectedItem.Value+"%");
			if(wdcFromDate.Text.Trim() == "")
				comm.Parameters.Add("@begin", "1999-01-01");
			else
				comm.Parameters.Add("@begin", wdcFromDate.Text.Trim());

			if(wdcToDate.Text.Trim() == "")
				comm.Parameters.Add("@end", "2050-12-31");
			else
				comm.Parameters.Add("@end", wdcToDate.Text.Trim());

			comm.Parameters.Add("@ProgressCondition", "%"+ddlState.SelectedItem.Value.Trim()+"%");

			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet dsSearch = new DataSet();
			da.Fill(dsSearch);

			buttonState = "Search";
			btnPre.Enabled = true;
			btnNext.Enabled = true;

			if(dsSearch.Tables[0].Rows.Count > 0)
			{
				btnExcel.Enabled = true;
			}
			else
			{
				btnExcel.Enabled = false;
			}

			return dsSearch;

			
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
				RegisterStartupScript("","<script>alert('볼륨번호가 없습니다!');</script>");
			}
			else
			{
				switch(button.CommandArgument.ToString())
				{
						// 이전(비디오버튼)버튼 클릭시
					case "Pre" :
						if(buttonEventPosition.ToString() == "gridPageEvent")
							strSQL = "SELECT VolumNum FROM BR_HT WHERE PropertyClassification = '원자재' and VolumNum = @VolumNum";
						else
							strSQL = "SELECT MAX(VolumNum) FROM BR_HT WHERE PropertyClassification = '원자재' and VolumNum < @VolumNum";
						break;

						// 현재(비디오버튼)버튼 클릭시
					case "Now" :
						strSQL = "SELECT MAX(VolumNum) FROM BR_HT";
						btnPre.Enabled = true;
						btnNext.Enabled = true;
						break;

						// 다음(비디오버튼)버튼 클릭시
					case "Next" :
						if(buttonEventPosition.ToString() == "gridPageEvent")
							strSQL = "SELECT VolumNum FROM BR_HT WHERE PropertyClassification = '원자재' and VolumNum = @VolumNum";
						else
							strSQL = "SELECT MIN(VolumNum) FROM BR_HT WHERE PropertyClassification = '원자재' and VolumNum > @VolumNum";
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
						SqlCommand comm1 = new SqlCommand("SELECT MIN(VolumNum) FROM BR_HT where PropertyClassification = '원자재'" , conn);
						volumNum.Value = Convert.ToString(comm1.ExecuteScalar());
						btnPre.Enabled = false;
						
					}
					else
					{
						if(Temp !="Cancle")
							RegisterStartupScript("","<script>alert('맨 끝입니다!');</script>");
						SqlCommand comm1 = new SqlCommand("SELECT MAX(VolumNum) FROM BR_HT where PropertyClassification = '원자재'" , conn);
						volumNum.Value = Convert.ToString(comm1.ExecuteScalar());
						btnNext.Enabled = false;
					}
				}
				else
					volumNum.Value = Convert.ToString(comm.ExecuteScalar());
			}
			conn.Close();

			KIT_ERP.VideoButton videoButton = new VideoButton(uwgBR_HT, "RowMaterialRequriementPC", volumNum.Value);

			switch(button.CommandArgument.ToString())
			{
					// 이전(비디오버튼)버튼 클릭시
				case "Pre" :
					dsBR_HT = videoButton.FindVolum();
					buttonState = "Pre";
					break;

					// 현재(비디오버튼)버튼 클릭시
				case "Now" :
					dsBR_HT = videoButton.FindVolum();
					buttonState = "Now";
					break;

					// 다음(비디오버튼)버튼 클릭시
				case "Next" :
					dsBR_HT = videoButton.FindVolum();
					buttonState = "Next";
					break;
			}

			if(dsBR_HT.Tables[0].Rows.Count > 0)
			{
				btnExcel.Enabled = true;
			}
			else
			{
				btnExcel.Enabled = false;
			}
			
			return dsBR_HT;
		}

		private void uwgBR_HT_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			buttonEventPosition = "gridPageEvent";
			uwgBR_HT.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			
			switch(buttonState.ToString())
			{
				case "Pre" :
					uwgBR_HT.DataSource = this.videoButton(this.btnPre, null);
					break;
				case "Now" :
					uwgBR_HT.DataSource = this.videoButton(this.btnNow, null);
					break;
				case "Next" :
					uwgBR_HT.DataSource = this.videoButton(this.btnNext, null);
					break;
				case "Search" :
					uwgBR_HT.DataSource = this.searchButton();
					break;
			}
			uwgBR_HT.DataBind();
		}

		

		protected DataSet DDL_DataSource
		{
			get
			{
				// 품목명 선택을 위한 WebComBo버튼에 해당(제품, 상품) Data를 바인딩
				//				RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\KIT_ERP");
				//				SqlConnection conn = new SqlConnection(registryKey.GetValue("ConnectionString").ToString());
				//				conn.Open();
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				conn.Open();


				// 구매의뢰부서선택을 위한 DropDownList에 해당 Data를 바인딩
				string strSQL = @"SELECT SmallClassificationCode, SmallClassificationName FROM PUC_MT 
									WHERE LargeClassificationCode LIKE '0800%' AND RecodingState = '1'";
				SqlCommand comm = new SqlCommand(strSQL, conn);
				DataSet ds = new DataSet();
				ds.Tables.Add();
				ds.Tables.Add();
				
				SqlDataAdapter adapter = new SqlDataAdapter(comm);
				adapter.Fill(ds.Tables[0]);				

				// 구매의뢰원천선택을 위한 DropDownList에 해당 Data를 바인딩
				strSQL = @"SELECT SmallClassificationCode, SmallClassificationName FROM PUC_MT 
								WHERE LargeClassificationCode LIKE '0330%' AND RecodingState = '1' AND SmallClassificationName != '정상'";
				comm = new SqlCommand(strSQL, conn);
				
				adapter = new SqlDataAdapter(comm);
				adapter.Fill(ds.Tables[1]);
				conn.Close();
					
				return ds;
			}
		}
	}
}
