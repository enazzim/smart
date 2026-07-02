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
	/// SaleHistoryRegistration에 대한 요약 설명입니다.
	/// </summary>
	public class SaleHistoryRegistration : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label searchTitle;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label21;
		protected System.Web.UI.WebControls.Button Button5;
		protected System.Web.UI.WebControls.Label Label17;
		protected System.Web.UI.WebControls.Label Label18;
		protected System.Web.UI.WebControls.Label Label19;
		protected System.Web.UI.WebControls.Label Label20;
		protected System.Web.UI.WebControls.Label Label22;
		protected System.Web.UI.WebControls.Label Label23;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label16;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Button bt_Search;
		protected System.Web.UI.WebControls.TextBox tb_ItemNum;
		protected System.Web.UI.WebControls.TextBox tb_OutStorehouseQuantity;
		protected System.Web.UI.WebControls.TextBox tb_SuitabilityQuantity;
		protected System.Web.UI.WebControls.TextBox tb_UnSuitabilityQuantity;
		protected System.Web.UI.WebControls.DropDownList dl_UnSuitabilityStatus;
		protected System.Web.UI.WebControls.DropDownList dl_UnSuitabilityCause;
		protected System.Web.UI.WebControls.TextBox tb_UnSuitabilityDetailMeaning;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.TextBox tb_SupplementaryValueTaxRate;
		protected System.Web.UI.WebControls.TextBox tb_UnSuitabilityCost;
		protected System.Web.UI.WebControls.DropDownList dl_InspectionDecision;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Com;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Num;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid2;
		protected System.Web.UI.WebControls.Label Label24;
		protected System.Web.UI.WebControls.TextBox tb_ApplyUnitCost;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_RowSelectIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_RowIndex;
		protected System.Web.UI.WebControls.Button bt_Register;
		protected System.Web.UI.WebControls.TextBox tb_ItemName;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcSaleDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcFromDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcToDate;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			ItemSearchControl1.Commodity = true; //상품 바인딩				
			ItemSearchControl1.Products = true; //제품 바인딩
			ItemSearchControl1.HalfFinishedProducts = true;//반제품 바인딩
			ItemSearchControl1.RawMaterials = true;//원자재 바인딩
			CSC1.UnitCostDistinction= "수주거래처";

			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(!Page.IsPostBack)
			{
				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 영업관리 > 매출등록';</script>");

				//bt_Register.Attributes.Add("onclick","return OK('등록');");

				tb_SuitabilityQuantity.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this);");
				// onkeyup 이벤트가 발생하면 불량수량을 자동으로 계산하는 자바스크립트 함수 매핑
				tb_SuitabilityQuantity.Attributes.Add("OnKeyUp", "return Process()");
				// focus를 얻으면 자동으로 select()
				tb_SuitabilityQuantity.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				// focus를 잃으면 다시 입력란을 체크
				tb_SuitabilityQuantity.Attributes.Add("OnBlur", "OnBlur_Float(this);");
	
				//불량금액
				tb_UnSuitabilityCost.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this);");
				tb_UnSuitabilityCost.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				tb_UnSuitabilityCost.Attributes.Add("OnBlur", "OnBlur_Float(this);");

				//적용단가
				tb_ApplyUnitCost.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this);");
				tb_ApplyUnitCost.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				tb_ApplyUnitCost.Attributes.Add("OnBlur", "OnBlur_Float(this);");

				wdcSaleDate.NullDateLabel = DateTime.Now.ToShortDateString();
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
			this.bt_Register.Click += new System.EventHandler(this.bt_Register_Click);
			this.Button5.Click += new System.EventHandler(this.Button5_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void page_Load()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			//	*************************************************
			//	**  부적합현상 드롭다운리스트... 데이타바인딩... **
			//	*************************************************
			//코드분류표에서 대분류명이 부적합현상인 소분류명을 가지고 옴.
			string str_Status = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '1000'" ;
			SqlCommand comm_Status = new SqlCommand(str_Status,conn);
			SqlDataAdapter da_Status = new SqlDataAdapter(comm_Status) ;
			DataSet ds_Status = new DataSet() ;
			da_Status.Fill(ds_Status);
				
			dl_UnSuitabilityStatus.DataSource = ds_Status;
			dl_UnSuitabilityStatus.DataTextField = ds_Status.Tables[0].Columns[0].ToString();
			dl_UnSuitabilityStatus.DataValueField = ds_Status.Tables[0].Columns[1].ToString();
			dl_UnSuitabilityStatus.DataBind();
			dl_UnSuitabilityStatus.Items.Insert(0, "-선택-") ;
			dl_UnSuitabilityStatus.Items[0].Value = "";

			//	*************************************************
			//	**  부적합원인 드롭다운리스트... 데이타바인딩... **
			//	*************************************************
			//코드분류표에서 대분류명이 부적합현상인 소분류명을 가지고 옴.
			string str_Cause = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '1010'" ;
			SqlCommand comm_Cause = new SqlCommand(str_Cause,conn);
			SqlDataAdapter da_Cause = new SqlDataAdapter(comm_Cause) ;
			DataSet ds_Cause = new DataSet() ;
			da_Cause.Fill(ds_Cause);
				
			dl_UnSuitabilityCause.DataSource = ds_Cause;
			dl_UnSuitabilityCause.DataTextField = ds_Cause.Tables[0].Columns[0].ToString();
			dl_UnSuitabilityCause.DataValueField = ds_Cause.Tables[0].Columns[1].ToString();
			dl_UnSuitabilityCause.DataBind();
			dl_UnSuitabilityCause.Items.Insert(0, "-선택-") ;
			dl_UnSuitabilityCause.Items[0].Value = "";


			//	*************************************************
			//	**  검사판정 드롭다운리스트... 데이타바인딩... **
			//	*************************************************
			//코드분류표에서 대분류명이 검사판정인 소분류명을 가지고 옴.
			string str_Decision = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '1310'" ;
			SqlCommand comm_Decision = new SqlCommand(str_Decision,conn);
			SqlDataAdapter da_Decision = new SqlDataAdapter(comm_Decision) ;
			DataSet ds_Decision = new DataSet() ;
			da_Decision.Fill(ds_Decision);
				
			dl_InspectionDecision.DataSource = ds_Decision;
			dl_InspectionDecision.DataTextField = ds_Decision.Tables[0].Columns[0].ToString();
			dl_InspectionDecision.DataValueField = ds_Decision.Tables[0].Columns[1].ToString();
			dl_InspectionDecision.DataBind();
			dl_InspectionDecision.Items.Insert(0, "-선택-") ;
			dl_InspectionDecision.Items[0].Value = "";
		}

		/// <summary>
		/// 검색버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Search_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = 1;
			Search();
		}

		/// <summary>
		/// 검색함수
		/// </summary>
		private void Search()
		{
			Search search = new Search("SaleHistoryRegistration",ItemSearchControl1.ItemNum,ItemSearchControl1.ItemDrawNum,ItemSearchControl1.ItemName,wdcFromDate,wdcToDate,CSC1.Company,CSC1.BusinessRegistrationNum);
			UltraWebGrid1.DataSource = search.DataSet_search();
			UltraWebGrid1.DataBind();
		}

		/// <summary>
		/// 매출등록버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Register_Click(object sender, System.EventArgs e)
		{
			if(lb_RowSelectIndex.Value == "")
			{
				RegisterStartupScript("","<script>alert('품목을 선택하세요!');</script>");
			}
			else
			{
				ArrayList arr = new ArrayList();

				arr.Add(decimal.Parse(tb_SuitabilityQuantity.Text));//함격수량
				arr.Add(decimal.Parse(tb_ApplyUnitCost.Text)*decimal.Parse(tb_SuitabilityQuantity.Text));//총금액			
				arr.Add(decimal.Parse(tb_UnSuitabilityQuantity.Text));//부적합수량
				arr.Add(decimal.Parse(tb_UnSuitabilityCost.Text));//부적합 금액
				if(dl_InspectionDecision.SelectedItem.Value.ToString().Trim() == "")
				{
					arr.Add("");
					arr.Add("");//검사판정내용
				}
				else
				{
					arr.Add(dl_InspectionDecision.SelectedItem.Value);//검사판정코드
					arr.Add(dl_InspectionDecision.SelectedItem.Text);//검사판정내용
				}
				if(dl_UnSuitabilityCause.SelectedItem.Value.ToString().Trim() == "")
				{
					arr.Add("");
					arr.Add("");
				}
				else
				{
					arr.Add(dl_UnSuitabilityCause.SelectedItem.Value);//부적합원인코드
					arr.Add(dl_UnSuitabilityCause.SelectedItem.Text);//부적합원인내용
				}
				if(dl_UnSuitabilityStatus.SelectedItem.Value.ToString().Trim() == "")
				{
					arr.Add("");
					arr.Add("");
				}
				else
				{
					arr.Add(dl_UnSuitabilityStatus.SelectedItem.Value);//부적합현상코드
					arr.Add(dl_UnSuitabilityStatus.SelectedItem.Text);//부적합현상내용
				}
				arr.Add(tb_UnSuitabilityDetailMeaning.Text);//부적합세부내용
			
				if(tb_SupplementaryValueTaxRate.Text.Trim() == "")
					arr.Add("0");
				else
					arr.Add(decimal.Parse(tb_SupplementaryValueTaxRate.Text));//부가세율
			
				if(tb_ApplyUnitCost.Text.Trim() == "")
					arr.Add("0");
				else
					arr.Add(decimal.Parse(tb_ApplyUnitCost.Text));		//적용단가.

				arr.Add(wdcSaleDate.Text);								//매출일
				arr.Add(int.Parse(lb_RowSelectIndex.Value));			//선택된 그리드

				Register reg = new Register(UltraWebGrid1,UltraWebGrid2,arr,"SaleHistoryRegistration",Session["ID"].ToString());
				reg.RowRegistration();
				Search();
				View();//아래 매출원장을 보여주는 함수
				ClearView();

			}
		}

		private void ClearView()
		{
			lb_RowSelectIndex.Value = "";
			tb_OutStorehouseQuantity.Text = "0";
			tb_SuitabilityQuantity.Text = "0";
			tb_ApplyUnitCost.Text = "0";
			tb_UnSuitabilityDetailMeaning.Text = "";
			tb_SupplementaryValueTaxRate.Text = "10";
			tb_ItemNum.Text = "";
			tb_ItemName.Text = "";
			tb_UnSuitabilityQuantity.Text = "0";
			tb_UnSuitabilityCost.Text = "0";
			dl_UnSuitabilityStatus.SelectedIndex = 0;
			dl_UnSuitabilityStatus.SelectedIndex = 0;
			dl_InspectionDecision.SelectedIndex = 0;
			wdcSaleDate.Value = DateTime.Now.ToShortDateString();
			lb_RowSelectIndex.Value = "";
			lb_Com.Value = "";
			lb_Num.Value = "";

		}
		private void View()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string str = "Select * From S_HT Where RegistrationDate = @date and BusinessRegistrationNum = @num order by SaleHistoryIndex desc";
			SqlCommand comm = new SqlCommand(str, conn);
			comm.Parameters.Add("@date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = lb_Com.Value.ToString();
			SqlDataReader dr = comm.ExecuteReader();
			UltraWebGrid2.DataSource = dr;
			UltraWebGrid2.DataBind();
			conn.Close();
		}


        /// <summary>
        /// 발행버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void Button5_Click(object sender, System.EventArgs e)
		{
			if(UltraWebGrid2.Rows.Count == 0)
			{
				RegisterStartupScript("","<script>alert('발행할 품목이 없습니다!');</script>");
			}
			else
			{
				lb_Com.Value = UltraWebGrid2.Rows[0].Cells.FromKey("BusinessRegistrationNum").Text;

				bool check = true;
				foreach(Infragistics.WebUI.UltraWebGrid.UltraGridRow row in UltraWebGrid2.Rows)
				{
					if(lb_Com != row.Cells.FromKey("BusinessRegistrationNum").Value)
					{
						check = false;
						break;
					}
				}
				if(!check)
				{
					
					Session["UWG"] = UltraWebGrid2;
					RegisterStartupScript("","<script>window.open('./Popup/Tex.aspx','','width=720,scrollbars=yes,menubar=yes,status=no,toolbar=yes,center=yes');</script>");
				}
				else
				{
					RegisterStartupScript("","<script>alert('다른거래처가 존재하여 계산서발행이 불가능합니다!');</script>");
				}
			}
		}

		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;

			Search search = new Search("SaleHistoryRegistration",ItemSearchControl1.ItemNum,ItemSearchControl1.ItemDrawNum,ItemSearchControl1.ItemName,wdcFromDate,wdcToDate,CSC1.Company,CSC1.BusinessRegistrationNum);
			UltraWebGrid1.DataSource = search.DataSet_search();
			UltraWebGrid1.DataBind();
		}
	}
}
