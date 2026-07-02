using System;
using System.Configuration;
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
using Infragistics.WebUI;
using Infragistics.WebUI.WebCombo;
using Infragistics.WebUI.UltraWebGrid;

namespace KIT_ERP.BusinessManagement
{
	/// <summary>
	/// GoodsManufactureOutStorehousePC에 대한 요약 설명입니다.
	/// </summary>
	public class GoodsManufactureOutStorehousePC : System.Web.UI.Page
	{
		protected System.Web.UI.HtmlControls.HtmlInputHidden chkAll;
		protected System.Web.UI.WebControls.Label searchTitle;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Button bt_Search;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Button bt_Excel;
		protected System.Web.UI.WebControls.LinkButton lnk_Update;
		protected System.Web.UI.WebControls.Button bt_Update;
		protected System.Web.UI.WebControls.Button bt_Delete;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_RowIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_RowSelectIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Store;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Quantity;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_UnInspectionQuantity;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_SuitabilityQuantity;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_UnSuitabilityQuantity;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdyear;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdmon;
		protected System.Web.UI.WebControls.DropDownList dlProgressState;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcFromDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcToDate;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;

		private DataSet ds = new DataSet();


		/// <summary>
		/// 부적합원인
		/// </summary>
		protected DataSet UnSuitabilityCause
		{
			get
			{
				Update source = new Update(Session["ID"].ToString());
				DataSet ds_Cause = source.UnSuitabilityCause();			
				return ds_Cause;
			}
		}

		/// <summary>
		/// 부적합현상
		/// </summary>
		protected DataSet UnSuitabilityStatus
		{
			get
			{
				Update source = new Update(Session["ID"].ToString());
				DataSet ds_Status = source.UnSuitabilityStatus();
				return ds_Status;
			}
		}

		/// <summary>
		/// 검사판정
		/// </summary>
		protected DataSet InspectionDecision
		{
			get
			{
				Update source = new Update(Session["ID"].ToString());
				DataSet ds_Decision = source.InspectionDecision();
				return ds_Decision;
			}
		}


	
		private void Page_Load(object sender, System.EventArgs e)
		{
			ItemSearchControl1.Commodity = true; //상품 바인딩
			ItemSearchControl1.Products = true; //제품 바인딩
			ItemSearchControl1.HalfFinishedProducts = true;//반제품 바인딩
			ItemSearchControl1.RawMaterials = true;//원자재 바인딩
			CSC1.UnitCostDistinction="수주거래처";

			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.

			if(!Page.IsPostBack)
			{
				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 영업관리 > 상품/제품출고현황';</script>");

				bt_Delete.Attributes.Add("onClick","return confirm('선택한 항목을 삭제하시겠습니까?');");

				page_Load();

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
			this.bt_Search.Click += new System.EventHandler(this.bt_Search_Click);
			this.UltraWebGrid1.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.UltraWebGrid1_PageIndexChanged);
			this.bt_Excel.Click += new System.EventHandler(this.bt_Excel_Click);
			this.lnk_Update.Click += new System.EventHandler(this.lnk_Update_Click);
			this.bt_Update.Click += new System.EventHandler(this.bt_Update_Click);
			this.bt_Delete.Click += new System.EventHandler(this.bt_Delete_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void page_Load()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			
			

			//	*************************************************
			//	**  진행상태 드롭다운리스트... 데이타바인딩... **
			//	*************************************************
			//코드분류표에서 대분류명이 단위인 소분류명을 가지고 옴.
			string str_Condition = "Select DISTINCT ProgressCondition from OS_HT";
			SqlCommand comm_Condition = new SqlCommand(str_Condition,conn);
			SqlDataAdapter da_Condition = new SqlDataAdapter(comm_Condition) ;
			DataSet ds_Condition = new DataSet() ;
			da_Condition.Fill(ds_Condition);
				
			dlProgressState.DataSource = ds_Condition;
			dlProgressState.DataTextField = ds_Condition.Tables[0].Columns[0].ToString();
			dlProgressState.DataValueField = ds_Condition.Tables[0].Columns[0].ToString();
			dlProgressState.DataBind();
			dlProgressState.Items.Insert(0, "-선택-") ;
			dlProgressState.Items[0].Value = "";


			/////////////////////////////////////////////////////
			/////////////////////////////////////////////////////
			///
			/// Row 템플릿열에 채우는 것
			///
			/////////////////////////////////////////////////////
			/////////////////////////////////////////////////////
		

			//	*************************************************
			//	**  부적합현상 드롭다운리스트... 데이타바인딩..**
			//	*************************************************
			//코드분류표에서 대분류명이 부적합현상인 소분류명을 가지고 옴.
			
			
				
//			dl_UnSuitabilityStatusEdit.DataSource = ds_UnSuitabilityStatus;
//			dl_UnSuitabilityStatusEdit.DataTextField = ds_UnSuitabilityStatus.Tables[0].Columns[1].ToString();
//			dl_UnSuitabilityStatusEdit.DataValueField = ds_UnSuitabilityStatus.Tables[0].Columns[0].ToString();
//			dl_UnSuitabilityStatusEdit.DataBind();
//			dl_UnSuitabilityStatusEdit.Items.Insert(0, "-선택-") ;
//			dl_UnSuitabilityStatusEdit.Items[0].Value = "";


			//	*************************************************
			//	**  부적합원인 드롭다운리스트... 데이타바인딩..**
			//	*************************************************
			//코드분류표에서 대분류명이 부적합원인인 소분류명을 가지고 옴.
			string str_UnSuitabilityCause = "Select SmallClassificationCode,SmallClassificationName from PUC_MT where SmallClassificationName != '' and LargeClassificationCode = '1010'";
			SqlCommand comm_UnSuitabilityCause = new SqlCommand(str_UnSuitabilityCause,conn);
			SqlDataAdapter da_UnSuitabilityCause = new SqlDataAdapter(comm_UnSuitabilityCause) ;
			DataSet ds_UnSuitabilityCause = new DataSet() ;
			da_UnSuitabilityCause.Fill(ds_UnSuitabilityCause);
				
//			dl_UnSuitabilityCauseEdit.DataSource = ds_UnSuitabilityCause;
//			dl_UnSuitabilityCauseEdit.DataTextField = ds_UnSuitabilityCause.Tables[0].Columns[1].ToString();
//			dl_UnSuitabilityCauseEdit.DataValueField = ds_UnSuitabilityCause.Tables[0].Columns[0].ToString();
//			dl_UnSuitabilityCauseEdit.DataBind();
//			dl_UnSuitabilityCauseEdit.Items.Insert(0, "-선택-") ;
//			dl_UnSuitabilityCauseEdit.Items[0].Value = "";



			//	*************************************************
			//	**  검사판정 드롭다운리스트... 데이타바인딩..**
			//	*************************************************
			//코드분류표에서 대분류명이 검사판정인 소분류명을 가지고 옴.
			string str_InspectionDecision = "Select SmallClassificationCode,SmallClassificationName from PUC_MT where SmallClassificationName != '' and LargeClassificationCode = '1310'";
			SqlCommand comm_InspectionDecision = new SqlCommand(str_InspectionDecision,conn);
			SqlDataAdapter da_InspectionDecision = new SqlDataAdapter(comm_InspectionDecision) ;
			DataSet ds_InspectionDecision = new DataSet() ;
			da_InspectionDecision.Fill(ds_InspectionDecision);
				
//			dl_InspectionDecisionEdit.DataSource = ds_InspectionDecision;
//			dl_InspectionDecisionEdit.DataTextField = ds_InspectionDecision.Tables[0].Columns[1].ToString();
//			dl_InspectionDecisionEdit.DataValueField = ds_InspectionDecision.Tables[0].Columns[0].ToString();
//			dl_InspectionDecisionEdit.DataBind();
//			dl_InspectionDecisionEdit.Items.Insert(0, "-선택-") ;
//			dl_InspectionDecisionEdit.Items[0].Value = "";

			conn.Close();
		}


		private void bt_Search_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = 1;
			UltraWebGrid1.DataSource = search();
			UltraWebGrid1.DataBind();			
			
		}



		private DataSet search()
		{
			
			ds.Clear();
			Search aa;
			if(ItemSearchControl1.hdItem.Trim() =="")
				aa = new Search("GoodsManufactureOutStorehousePC",ItemSearchControl1.ItemNum,ItemSearchControl1.ItemDrawNum,ItemSearchControl1.ItemName,wdcFromDate,wdcToDate,CSC1.Company,CSC1.BusinessRegistrationNum,dlProgressState.SelectedItem.Value);
			else
				aa = new Search("GoodsManufactureOutStorehousePC",ItemSearchControl1.ItemNum,"", "",wdcFromDate,wdcToDate,CSC1.Company,CSC1.BusinessRegistrationNum,dlProgressState.SelectedItem.Value);
			
			return aa.DataSet_search();
			
		}


		/// <summary>
		/// 페이지 이동을 위할때
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			UltraWebGrid1.DataSource = search();
			UltraWebGrid1.DataBind();

		}


		/// <summary>
		/// 엑셀저장버튼
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Excel_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid uwg_OS = new UltraWebGrid();
			uwg_OS = UltraWebGrid1;
			uwg_OS.DisplayLayout.Pager.AllowPaging = false;
			uwg_OS.Columns.FromKey("chk").Hidden = true;
			uwg_OS.DataSource = search();
			uwg_OS.DataBind();
			UltraWebGridExcelExporter1.Export(uwg_OS);
		}

		/// <summary>
		/// 삭제버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Delete_Click(object sender, System.EventArgs e)
		{
			if(MonthClosing())
			{
				Delete del = new Delete(UltraWebGrid1,"GoodsManufactureOutStorehousePC");
				del.MainRowDelete();
			}
			else
			{
				RegisterStartupScript("","<script>alert('월마감이 되어 삭제가 불가능 합니다!');</script>");
			}

			
			UltraWebGrid1.DataSource = search();
			UltraWebGrid1.DataBind();
		}

		/// <summary>
		/// 수정
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lnk_Update_Click(object sender, System.EventArgs e)
		{
			ArrayList arr = new ArrayList();
			//변경전 수량들을 넘겨준다(수주원장과 창고테이블에서 사용되어짐
            arr.Add(decimal.Parse(lb_Quantity.Value));//출고수량
			arr.Add(decimal.Parse(lb_UnInspectionQuantity.Value));//미검수량
			if(Convert.ToString(UltraWebGrid1.Rows[int.Parse(lb_RowIndex.Value)].Cells.FromKey("ProgressCondition").Value) == "대기")
			{
				arr.Add("");
				arr.Add("");
			}
			else
			{
				arr.Add(decimal.Parse(lb_SuitabilityQuantity.Value));//합격수량
				arr.Add(decimal.Parse(lb_UnSuitabilityQuantity.Value));//부적합수량
			}
			arr.Add(int.Parse(lb_Store.Value));//창고번호
			arr.Add(hdyear.Value);
			arr.Add(hdmon.Value);
			arr.Add(int.Parse(lb_RowSelectIndex.Value));//그리드 인덱스


			if(MonthClosing())
			{
				Update up = new Update("GoodsManufactureOutStorehousePC",arr,UltraWebGrid1,Session["ID"].ToString());
				up.MainTableUpdate();
			}
			else
			{
				RegisterStartupScript("","<script>alert('월마감이 되어 수정이 불가능 합니다!');</script>");
			}

			
			
			UltraWebGrid1.DataSource = search();
			UltraWebGrid1.DataBind();

		}


		private void bt_Update_Click(object sender, System.EventArgs e)
		{
			Infragistics.WebUI.UltraWebGrid.UltraWebGrid UWG = UltraWebGrid1;

			bool check = true;
			for(int count = UWG.Rows.Count-1 ; count >=0 ; count--)
			{
				if(UWG.Rows[count].Cells.FromKey("chk").Value == null)
					UWG.Rows[count].Delete();
			}

			if(UWG.Rows.Count == 0)
			{
				check = false;
				RegisterStartupScript("","<script>alert('선택한 항목이 없습니다!');</script>");
			}
			else
			{
				foreach(UltraGridRow row in UWG.Rows)
				{
					if(row.Cells.FromKey("BusinessRegistrationNum").Text != UWG.Rows[0].Cells.FromKey("BusinessRegistrationNum").Text)
					{
						RegisterStartupScript("","<script>alert('다른거래처가 존재하여 계산서발행이 불가능합니다!');</script>");
						check = false;
						break;
					}
				}
			}

			if(check)
			{
				Session["Grid"] = UWG;
				RegisterStartupScript("","<script>window.open('./Popup/BusinessDetailedStatement.aspx','','width=720,scrollbars=yes,menubar=yes,status=no,toolbar=yes,center=yes');</script>");
				//Response.Write("<script>window.open('./Popup/BusinessDetailedStatement.aspx','','width=720,scrollbars=yes,menubar=yes,status=no,toolbar=yes,center=yes');</script>");
			}

			//UltraWebGrid1.DataSource = search();
			//UltraWebGrid1.DataBind();
		}


		/// <summary>
		/// 월마감 여부를 확인하는 함수
		/// </summary>
		/// <returns></returns>
		private bool MonthClosing()
		{
			return true;
//			int count = int.Parse(lb_RowSelectIndex.Value);
//			int year = 0;
//			int month = 0;
//			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
//			conn.Open();
//			string str = "Select ClosingYear, ClosingMonth From MCI_MT Where AffairDistinction = @Distinction";
//			SqlCommand comm =  new SqlCommand(str,conn);
//			comm.Parameters.Add("@Distinction",SqlDbType.VarChar).Value = "영업";
//			SqlDataReader dr = comm.ExecuteReader();
//			while(dr.Read())
//			{
//				year = int.Parse(dr[0].ToString());
//				month = int.Parse(dr[1].ToString());
//			}
//			conn.Close();
//
//			if(year < DateTime.Parse(UltraWebGrid1.Rows[count].Cells.FromKey("OutStoreDate").Text).Year)
//				return true;
//			else if(year == DateTime.Parse(UltraWebGrid1.Rows[count].Cells.FromKey("OutStoreDate").Text).Year)
//			{
//				if(month < DateTime.Parse(UltraWebGrid1.Rows[count].Cells.FromKey("OutStoreDate").Text).Month)
//					return true;
//				else
//					return false;
//			}
//			else
//			{
//				return false;
//			}		
		}
	}
}
