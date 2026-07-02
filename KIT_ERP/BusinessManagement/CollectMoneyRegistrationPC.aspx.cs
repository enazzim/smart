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
using Infragistics.WebUI.UltraWebGrid;
using Infragistics.WebUI.WebCombo;


namespace KIT_ERP.BusinessManagement
{
	/// <summary>
	/// CollectMoneyRegistrationPC에 대한 요약 설명입니다.
	/// </summary>
	public class CollectMoneyRegistrationPC : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label searchTitle;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdc_FromDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdc_ToDate;
		protected System.Web.UI.WebControls.Button bt_Clear;
		protected System.Web.UI.WebControls.Button bt_Search;
		protected System.Web.UI.WebControls.DropDownList dl_DecisionMethod;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Button bt_Excel;
		protected System.Web.UI.WebControls.Button bt_Delete;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_RowIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_RowSelectIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Cost;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Tax;
		protected System.Web.UI.WebControls.LinkButton lnk_Update;
		protected Infragistics.WebUI.WebCombo.WebCombo wc_Company;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hd_year;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hd_month;
		protected DataSet ds = new DataSet();
	
		protected DataSet Bank
		{
			get
			{
				Update source = new Update(Session["ID"].ToString());
				DataSet ds_Bank = source.Bank();			
				return ds_Bank;
			}
		}

		protected DataSet Method
		{
			get
			{
				Update source = new Update(Session["ID"].ToString());
				DataSet ds_Method = source.Method();
				return ds_Method;
			}
		}




		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}
			if(!Page.IsPostBack)
			{
				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 영업관리 > 수금등록현황';</script>");

				bt_Delete.Attributes.Add("onClick","return confirm('선택한 항목을 삭제하시겠습니까?');");

				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				conn.Open();

				// 거래처 웹콤보 데이터바인딩
				string str1 = "SELECT CompanyName, PresidentName, BusinessRegistrationNum, CompanyInfoIndex FROM CI_MT where RecodingState = 1 and ReceiveingOrderCompany = 1 order by CompanyName";
				SqlCommand comm1 =  new SqlCommand(str1,conn);
				SqlDataAdapter da1 = new SqlDataAdapter(comm1) ;
				DataSet ds1 = new DataSet() ;
				da1.Fill(ds1);

				wc_Company.DataSource = ds1;
				wc_Company.DataTextField = ds1.Tables[0].Columns[0].ToString();
				wc_Company.DataValueField = ds1.Tables[0].Columns[0].ToString();
				wc_Company.DataBind();

				//	*************************************************
				//	**  결재방법 드롭다운리스트... 데이타바인딩... **
				//	*************************************************
				//코드분류표에서 대분류명이 결재방법인 소분류명을 가지고 옴.
				string str = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationName = '결재방법'" ;
				SqlCommand comm = new SqlCommand(str,conn);
				SqlDataAdapter da = new SqlDataAdapter(comm) ;
				DataSet ds = new DataSet() ;
				da.Fill(ds);
				
				dl_DecisionMethod.DataSource = ds;
				dl_DecisionMethod.DataTextField = ds.Tables[0].Columns[0].ToString();
				dl_DecisionMethod.DataValueField = ds.Tables[0].Columns[0].ToString();
				dl_DecisionMethod.DataBind();
				dl_DecisionMethod.Items.Insert(0, "-선택-") ;
				dl_DecisionMethod.Items[0].Value = "";

				

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
			this.bt_Clear.Click += new System.EventHandler(this.bt_Clear_Click);
			this.bt_Search.Click += new System.EventHandler(this.bt_Search_Click);
			this.UltraWebGrid1.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.UltraWebGrid1_PageIndexChanged);
			this.bt_Excel.Click += new System.EventHandler(this.bt_Excel_Click);
			this.lnk_Update.Click += new System.EventHandler(this.lnk_Update_Click);
			this.bt_Delete.Click += new System.EventHandler(this.bt_Delete_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		/// <summary>
		/// 검색버튼
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Search_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = 1;
			UltraWebGrid1.DataSource = Search();
			UltraWebGrid1.DataBind();
		}

		/// <summary>
		/// 초기화버튼
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Clear_Click(object sender, System.EventArgs e)
		{
			wc_Company.DataValue = "";
			dl_DecisionMethod.SelectedIndex = 0;
			wdc_FromDate.Value = "";
			wdc_ToDate.Value = "";
		}

		/// <summary>
		/// 검색함수
		/// </summary>
		private DataSet Search()
		{
			
			Search aa = new Search("CollectMoneyRegistrationPC",wdc_FromDate,wdc_ToDate,wc_Company, dl_DecisionMethod.SelectedItem.Value);
			return aa.DataSet_search();
			
		}

		/// <summary>
		/// 삭제버튼
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Delete_Click(object sender, System.EventArgs e)
		{
			if(!MonthClosing())
			{
				Delete del = new Delete(UltraWebGrid1,"CollectMoneyRegistrationPC");
				del.MainRowDelete();
			}
			else
			{
				RegisterStartupScript("","<script>alert('월마감이 되어 삭제가 불가능 합니다!');</script>");
			}
			
			UltraWebGrid1.DataSource = Search();
			UltraWebGrid1.DataBind();
		}

		/// <summary>
		/// 엑셀저장
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Excel_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid uwg_Excel = new UltraWebGrid();
			uwg_Excel = UltraWebGrid1;
			uwg_Excel.DisplayLayout.Pager.AllowPaging = false;
			uwg_Excel.Columns.FromKey("chk").Hidden = true;
			uwg_Excel.DataSource = Search();
			uwg_Excel.DataBind();
			UltraWebGridExcelExporter1.Export(uwg_Excel);
		}

		/// <summary>
		/// 페이지 이동
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			UltraWebGrid1.DataSource = Search();
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
			arr.Add(decimal.Parse(lb_Cost.Value));//기존의 수금액
			arr.Add(decimal.Parse(lb_Tax.Value));//기존의 부가세
			//결재일자1
			if(UltraWebGrid1.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("BillPaymentDate1").Value == null || UltraWebGrid1.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("BillPaymentDate1").Text.Trim() == "")
				arr.Add("");
			else
				arr.Add(UltraWebGrid1.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("BillPaymentDate1").Text.Trim());
			//결재일자2
			if(UltraWebGrid1.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("BillPaymentDate2").Value == null || UltraWebGrid1.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("BillPaymentDate2").Text.Trim() == "")
				arr.Add("");
			else
				arr.Add(UltraWebGrid1.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("BillPaymentDate2").Text.Trim());
			//결재일자3
			if(UltraWebGrid1.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("BillPaymentDate3").Value == null || UltraWebGrid1.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("BillPaymentDate3").Text.Trim() == "")
				arr.Add("");
			else
				arr.Add(UltraWebGrid1.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("BillPaymentDate3").Text.Trim());

			arr.Add(hd_year.Value);
			arr.Add(hd_month.Value);
			
			arr.Add(int.Parse(lb_RowSelectIndex.Value));//선택 그리드 Row인덱스

			if(!MonthClosing())
			{
				Update up = new Update("CollectMoneyRegistrationPC",arr,UltraWebGrid1, Session["ID"].ToString());
				up.MainTableUpdate();
			}
			else
			{
				RegisterStartupScript("","<script>alert('월마감이 되어 수정이 불가능 합니다!');</script>");
			}
			
			
			UltraWebGrid1.DataSource = Search();
			UltraWebGrid1.DataBind();

		}

		/// <summary>
		/// 월마감 여부를 확인하는 함수
		/// </summary>
		/// <returns></returns>
		private bool MonthClosing()
		{
			int count = int.Parse(lb_RowSelectIndex.Value);
			int year = 0;
			int month = 0;
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select ClosingYear, ClosingMonth From MCI_MT Where AffairDistinction = @Distinction";
			SqlCommand comm =  new SqlCommand(str,conn);
			comm.Parameters.Add("@Distinction",SqlDbType.VarChar).Value = "영업";
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				year = int.Parse(dr[0].ToString());
				month = int.Parse(dr[1].ToString());
			}
			conn.Close();

			
			if(year < DateTime.Parse(UltraWebGrid1.Rows[count].Cells.FromKey("CollectMoneyDate").Text).Year)
				return true;
			else if(year == DateTime.Parse(UltraWebGrid1.Rows[count].Cells.FromKey("CollectMoneyDate").Text).Year)
			{
				if(month < DateTime.Parse(UltraWebGrid1.Rows[count].Cells.FromKey("CollectMoneyDate").Text).Month)
					return true;
				else
					return false;
			}
			else
			{
				return false;
			}		
		}


		
	}
}
