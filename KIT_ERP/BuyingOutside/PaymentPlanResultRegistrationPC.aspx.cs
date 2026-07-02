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

namespace KIT_ERP.BuyingOutside
{
	/// <summary>
	/// PaymentPlanResultPC에 대한 요약 설명입니다.
	/// </summary>
	public class PaymentPlanResultPC : System.Web.UI.Page
	{
		protected Infragistics.WebUI.WebCombo.WebCombo wcbCompany;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcStartDate;
		protected System.Web.UI.WebControls.Button btnStop;
		protected System.Web.UI.WebControls.Button btnInit;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.WebControls.Button btnExcel;
		protected System.Web.UI.WebControls.DropDownList SettleDropDown;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwgPPR_HT;
		protected System.Web.UI.WebControls.Button btnDelete;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hd_ItemPaymentCost;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdRowIndex;
		protected System.Web.UI.WebControls.LinkButton linkUpdate;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter uwgExcel;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdYear;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdMon;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcEndDate;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}
			if(!Page.IsPostBack)
			{

				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 구매/외주관리 > 지급계획/실적등록현황';</script>");

				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				SqlDataAdapter PPRAdapter = new SqlDataAdapter("procPaymentPlanResultRegistration",conn);
				DataSet dsPPR = new DataSet();
				PPRAdapter.Fill(dsPPR);
				//품목과 거래처에 관한 정보를 가지고 와서 WebCombo에 뿌러줌(프로시져)
				this.wcbCompany.DataSource = dsPPR.Tables[2];
				this.wcbCompany.DataValue = dsPPR.Tables[2].Columns[1];
				this.wcbCompany.DataTextField = "거래처명";
				this.wcbCompany.DataValue = "사업자등록번호";
				this.wcbCompany.DataBind();

				this.wcbCompany.Columns[0].Width = 250;
				this.wcbCompany.Columns[1].Width = 300;

				wcbCompany.DisplayValue = "";

				//결제방법 드롭다운리스트에 바인딩				
				this.SettleDropDown.DataSource = dsPPR.Tables[0];
				this.SettleDropDown.DataTextField = dsPPR.Tables[0].Columns[1].ToString();
				this.SettleDropDown.DataValueField = dsPPR.Tables[0].Columns[1].ToString();
				this.SettleDropDown.DataBind();
				this.SettleDropDown.Items.Insert(0,"---선택---");
				this.SettleDropDown.Items[0].Value = "";


				// 삭제버튼에 대하여 onclick속성을 추가 시킴
				btnDelete.Attributes.Add("onClick","return Confirm('선택한 항목에 대하여 삭제을 하시겠습니까?');");

			}
		}

		protected DataSet dsBank
		{
			get
			{
				DataSet ds = new DataSet();
				Update up = new Update();
				ds = up.dsBank();
				
				return ds;
			}
		}

		protected DataSet dsDecision
		{
			get
			{
				DataSet ds = new DataSet();
				Update up = new Update();
				ds = up.dsDecision();
				return ds;
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
			this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.uwgPPR_HT.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.uwgPPR_HT_PageIndexChanged);
			this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
			this.linkUpdate.Click += new System.EventHandler(this.LinkButton1_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click_1);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		//검색조건에 행당하는 품목들을 원장에서 검색
		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			KIT_ERP.Search search = new KIT_ERP.Search("PaymentPlanResultRegistrationPC",wdcStartDate,wdcEndDate,wcbCompany,SettleDropDown.SelectedItem.Value);
			this.uwgPPR_HT.DataSource = search.DataSet_search();;
			this.uwgPPR_HT.DataBind();
		}

		//검색조건 초기화 버튼
		private void btnInit_Click(object sender, System.EventArgs e)
		{
			this.wcbCompany.DataValue = "";
			this.SettleDropDown.SelectedIndex = 0;
			this.wdcStartDate.Value = null;
			this.wdcEndDate.Value = null;
		}

		//현제 나타난 그리드에서 삭제하고자 하는 품목을 선택하고 삭제
		private void btnDelete_Click_1(object sender, System.EventArgs e)
		{
			if(this.uwgPPR_HT.Rows.Count ==0)
			{				
				Response.Write("<script language=javascript>");
				Response.Write("alert('삭제할 항목이 없습니다.')");
				Response.Write("</script>");
			}
			else
			{
				KIT_ERP.Delete delete = new KIT_ERP.Delete(uwgPPR_HT,"PaymentPlanResultRegistrationPC");
				delete.MainRowDelete();
				
				// 검색객체를 생성(이전 검색조건과 같은 조건으로 검색해서 내용을 그리드에 바인딩)
				KIT_ERP.Search search = new KIT_ERP.Search("PaymentPlanResultRegistrationPC",wdcStartDate,wdcEndDate,wcbCompany,SettleDropDown.SelectedItem.Value);
				this.uwgPPR_HT.DataSource = search.DataSet_search();;
				this.uwgPPR_HT.DataBind();					
			}			
		}

		private void LinkButton1_Click(object sender, System.EventArgs e)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();	
			SqlTransaction tr = conn.BeginTransaction();
			try
			{
				PaymentMoneyRegistrationUpdate(conn,tr);
				BSI_MTTableUpdate(conn,tr);
				tr.Commit();
				RegisterStartupScript("","<script>alert('수정되었습니다!');</script>");
			}
			catch(Exception ee)
			{
				RegisterStartupScript("","<script>alert('"+ee.Message+"');</script>");
				tr.Rollback();
			}
			finally
			{
				conn.Close();
			}
			
		}

		//매입매출테이블 업데이트
		private void BSI_MTTableUpdate(SqlConnection con, SqlTransaction trans)
		{
			int year = int.Parse(hdYear.Value);
			int mon = int.Parse(hdMon.Value);
			//기존테이블에서 이전물품액을 마이너스 증가
			int rowcount = int.Parse(hdRowIndex.Value);
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;
			comm.Parameters.Add("@comnum", SqlDbType.VarChar).Value = uwgPPR_HT.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Value.ToString();
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(hd_ItemPaymentCost.Value);
			comm.Parameters.Add("@year",year);
			
			Table table = new Table(year,mon);
			comm.CommandText = table.UNCollectMoneyDiminutionPaymentMoneyIncreaseTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			//새로변경되는 물품액 증가
			year = DateTime.Parse(uwgPPR_HT.Rows[rowcount].Cells.FromKey("PaymentDate").Text).Year;
			mon = DateTime.Parse(uwgPPR_HT.Rows[rowcount].Cells.FromKey("PaymentDate").Text).Month;

			Table table1 = new Table(year,mon);			
			comm.Parameters.Add("@comnum", SqlDbType.VarChar).Value = uwgPPR_HT.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Value.ToString();
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(uwgPPR_HT.Rows[rowcount].Cells.FromKey("ItemPaymentCost").Value.ToString());// 물품금액
			comm.Parameters.Add("@year", year);

			comm.CommandText = table1.UNCollectMoneyIncreasePaymentMoneyDiminutionTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
		}

		//지급원장 업데이트
		private void PaymentMoneyRegistrationUpdate(SqlConnection con, SqlTransaction trans)
		{
			int rowcount = int.Parse(hdRowIndex.Value); //Row 인덱스...

			string str = " UPDATE P_HT SET PaymentDate = @PaymentDate, ItemPaymentCost = @ItemPaymentCost, " +
						" SupplementaryValueTaxPaymentCost = @SupplementaryValueTaxPaymentCost,DecisionMethodCode = @DecisionMethodCode,DecisionMethod = @DecisionMethod,ProofData=@ProofData, " +
						" BillNum1 = @BillNum1,BillPaymentDate1=@BillPaymentDate1,BankCode1=@BankCode1,BankName1=@BankName1, " +
						" BillNum2 = @BillNum2,BillPaymentDate2=@BillPaymentDate2,BankCode2=@BankCode1,BankName2=@BankName2, " +
						" BillNum3 = @BillNum3,BillPaymentDate3=@BillPaymentDate3,BankCode3=@BankCode1,BankName3=@BankName3, " +
						" UpdatingPerson=@UpdatingPerson,UpdatingPersonID=@UpdatingPersonID,UpdatingDate = @UpdatingDate" +
						" WHERE PaymentHistoryIndex= @INDEX";
			
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			
			comm.Parameters.Add("@PaymentDate",SqlDbType.SmallDateTime).Value = DateTime.Parse(uwgPPR_HT.Rows[rowcount].Cells.FromKey("PaymentDate").Value.ToString());// 지급일자
			comm.Parameters.Add("@ItemPaymentCost",SqlDbType.Decimal).Value = decimal.Parse(uwgPPR_HT.Rows[rowcount].Cells.FromKey("ItemPaymentCost").Value.ToString());// 수금액(물품대)
			comm.Parameters.Add("@SupplementaryValueTaxPaymentCost",SqlDbType.Decimal).Value = decimal.Parse(uwgPPR_HT.Rows[rowcount].Cells.FromKey("SupplementaryValueTaxPaymentCost").Value.ToString());// 지급액(부가세)
			comm.Parameters.Add("@DecisionMethodCode",SqlDbType.VarChar).Value = uwgPPR_HT.Rows[rowcount].Cells.FromKey("DecisionMethodCode").Value.ToString();//결재방법코드
			comm.Parameters.Add("@DecisionMethod",SqlDbType.VarChar).Value = uwgPPR_HT.Rows[rowcount].Cells.FromKey("DecisionMethod").Value.ToString();//결재방법
			comm.Parameters.Add("@ProofData",SqlDbType.VarChar).Value = uwgPPR_HT.Rows[rowcount].Cells.FromKey("ProofData").Value.ToString();//증거자료
			
			if(uwgPPR_HT.Rows[rowcount].Cells.FromKey("BillNum1").Value == null || uwgPPR_HT.Rows[rowcount].Cells.FromKey("BillNum1").Text.Trim() == "")
				comm.Parameters.Add("@BillNum1",SqlDbType.VarChar).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@BillNum1",SqlDbType.VarChar).Value = uwgPPR_HT.Rows[rowcount].Cells.FromKey("BillNum1").Value.ToString();//어음번호
			
			if(uwgPPR_HT.Rows[rowcount].Cells.FromKey("BillPaymentDate1").Value == null || uwgPPR_HT.Rows[rowcount].Cells.FromKey("BillPaymentDate1").Text.Trim() == "")
				comm.Parameters.Add("@BillPaymentDate1",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@BillPaymentDate1",SqlDbType.SmallDateTime).Value = DateTime.Parse(uwgPPR_HT.Rows[rowcount].Cells.FromKey("BillPaymentDate1").Value.ToString());//결재일자
			
			if(uwgPPR_HT.Rows[rowcount].Cells.FromKey("BankCode1").Value == null || uwgPPR_HT.Rows[rowcount].Cells.FromKey("BankCode1").Text.Trim() == "")
			{

				comm.Parameters.Add("@BankCode1",SqlDbType.VarChar).Value = Convert.DBNull;//은행코드
				comm.Parameters.Add("@BankName1",SqlDbType.VarChar).Value = Convert.DBNull;//은행명
			}
			else
			{
				comm.Parameters.Add("@BankCode1",SqlDbType.VarChar).Value = uwgPPR_HT.Rows[rowcount].Cells.FromKey("BankCode1").Value.ToString();//은행코드
				comm.Parameters.Add("@BankName1",SqlDbType.VarChar).Value = uwgPPR_HT.Rows[rowcount].Cells.FromKey("BankName1").Value.ToString();//은행명
			}
			
			if(uwgPPR_HT.Rows[rowcount].Cells.FromKey("BillNum2").Value == null || uwgPPR_HT.Rows[rowcount].Cells.FromKey("BillNum2").Text.Trim() == "")
				comm.Parameters.Add("@BillNum2",SqlDbType.VarChar).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@BillNum2",SqlDbType.VarChar).Value = uwgPPR_HT.Rows[rowcount].Cells.FromKey("BillNum2").Value.ToString();//어음번호
			
			if(uwgPPR_HT.Rows[rowcount].Cells.FromKey("BillPaymentDate2").Value == null || uwgPPR_HT.Rows[rowcount].Cells.FromKey("BillPaymentDate2").Text.Trim() == "" )
				comm.Parameters.Add("@BillPaymentDate2",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@BillPaymentDate2",SqlDbType.SmallDateTime).Value = DateTime.Parse(uwgPPR_HT.Rows[rowcount].Cells.FromKey("BillPaymentDate2").Value.ToString());//결재일자
			
			if(uwgPPR_HT.Rows[rowcount].Cells.FromKey("BankCode2").Value == null || uwgPPR_HT.Rows[rowcount].Cells.FromKey("BankCode2").Text.Trim() == "")
			{

				comm.Parameters.Add("@BankCode2",SqlDbType.VarChar).Value = Convert.DBNull;//은행코드
				comm.Parameters.Add("@BankName2",SqlDbType.VarChar).Value = Convert.DBNull;//은행코드
			}
			else
			{
				comm.Parameters.Add("@BankCode2",SqlDbType.VarChar).Value = uwgPPR_HT.Rows[rowcount].Cells.FromKey("BankCode2").Value.ToString();
				comm.Parameters.Add("@BankName2",SqlDbType.VarChar).Value = uwgPPR_HT.Rows[rowcount].Cells.FromKey("BankName2").Value.ToString();
			}
			
			
			if(uwgPPR_HT.Rows[rowcount].Cells.FromKey("BillNum3").Value == null || uwgPPR_HT.Rows[rowcount].Cells.FromKey("BillNum3").Text.Trim() == "")
				comm.Parameters.Add("@BillNum3",SqlDbType.VarChar).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@BillNum3",SqlDbType.VarChar).Value = uwgPPR_HT.Rows[rowcount].Cells.FromKey("BillNum3").Value.ToString();//어음번호
			
			if(uwgPPR_HT.Rows[rowcount].Cells.FromKey("BillPaymentDate3").Value == null || uwgPPR_HT.Rows[rowcount].Cells.FromKey("BillPaymentDate2").Text.Trim() == "" )
					comm.Parameters.Add("@BillPaymentDate3",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@BillPaymentDate3",SqlDbType.SmallDateTime).Value = DateTime.Parse(uwgPPR_HT.Rows[rowcount].Cells.FromKey("BillPaymentDate3").Value.ToString());//결재일자
			
			if(uwgPPR_HT.Rows[rowcount].Cells.FromKey("BankCode3").Value == null || uwgPPR_HT.Rows[rowcount].Cells.FromKey("BankCode3").Text.Trim() == "")
			{

				comm.Parameters.Add("@BankCode3",SqlDbType.VarChar).Value = Convert.DBNull;//은행코드
				comm.Parameters.Add("@BankName3",SqlDbType.VarChar).Value = Convert.DBNull;//은행코드
			}
			else
			{
				comm.Parameters.Add("@BankCode3",SqlDbType.VarChar).Value = uwgPPR_HT.Rows[rowcount].Cells.FromKey("BankCode3").Value.ToString();
				comm.Parameters.Add("@BankName3",SqlDbType.VarChar).Value = uwgPPR_HT.Rows[rowcount].Cells.FromKey("BankName3").Value.ToString();
			}
			
			comm.Parameters.Add("@UpdatingPerson",SqlDbType.VarChar).Value = Session["UserName"].ToString();
			comm.Parameters.Add("@UpdatingPersonID",SqlDbType.VarChar).Value = Session["ID"].ToString();
			comm.Parameters.Add("@UpdatingDate",SqlDbType.SmallDateTime).Value = DateTime.Now.ToString();
			
			comm.Parameters.Add("@INDEX",SqlDbType.Int).Value = int.Parse(uwgPPR_HT.Rows[rowcount].Cells.FromKey("PaymentHistoryIndex").Value.ToString());
			comm.ExecuteNonQuery();
		}

		private void btnExcel_Click(object sender, System.EventArgs e)
		{
			// 현재 Grid의 내용을 Excel로 Export
			Infragistics.WebUI.UltraWebGrid.UltraWebGrid  uwgExcelImport = new Infragistics.WebUI.UltraWebGrid.UltraWebGrid();
			uwgExcelImport = uwgPPR_HT;
			uwgExcelImport.Columns.FromKey("chk").Hidden = true;
			uwgExcel.Export(uwgPPR_HT);
		}

		private void uwgPPR_HT_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			uwgPPR_HT.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;

			KIT_ERP.Search search = new KIT_ERP.Search("PaymentPlanResultRegistrationPC",wdcStartDate,wdcEndDate,wcbCompany,SettleDropDown.SelectedItem.Value);
			this.uwgPPR_HT.DataSource = search.DataSet_search();;
			this.uwgPPR_HT.DataBind();
		}
	}
}
