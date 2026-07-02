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
	/// PaymentPlanResultRegistraton에 대한 요약 설명입니다.
	/// </summary>
	public class PaymentPlanResultRegistraton : System.Web.UI.Page
	{
		protected Infragistics.WebUI.WebSchedule.WebDateChooser PaymentDate;
		protected System.Web.UI.WebControls.TextBox txtEvidence;
		protected System.Web.UI.WebControls.TextBox txtDraftNum1;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser SettleDate1;
		protected System.Web.UI.WebControls.DropDownList BankingDropdown1;
		protected System.Web.UI.WebControls.TextBox txtDraftNum2;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser SettleDate2;
		protected System.Web.UI.WebControls.DropDownList BankingDropdown2;
		protected System.Web.UI.WebControls.TextBox txtDraftNum3;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser SettleDate3;
		protected System.Web.UI.WebControls.DropDownList BankingDropdown3;
		protected System.Web.UI.WebControls.Button btnInit;
		protected System.Web.UI.WebControls.Button btnEdit;
		protected System.Web.UI.WebControls.Button btnDelete;
		protected System.Web.UI.WebControls.Button btnInput;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwgP_HT;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdBusinessRegistrationNum;
		protected System.Web.UI.WebControls.DropDownList SettleDropDown;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdindex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lbOldCost;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdYear;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdMon;
		protected System.Web.UI.WebControls.TextBox tb_SupplementaryValueTaxPaymentCost;
		protected System.Web.UI.WebControls.TextBox tb_ItemPaymentCost;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;

	
		private void Page_Load(object sender, System.EventArgs e)
		{
			CSC1.UnitCostDistinction = "구매외주";
			CSC1.IsDoPost1 = true;

			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Homepage/default.htm';</script>");
			}
			if(!Page.IsPostBack)
			{

				//품목과 거래처에 관한 정보를 가지고 와서 WebCombo에 뿌러줌(프로시져)
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				SqlDataAdapter sa = new SqlDataAdapter("procPaymentPlanResultRegistration",conn);
				DataSet ds = new DataSet();
				sa.Fill(ds);
				

					
				//결제방법 드롭다운리스트에 바인딩
				SqlDataAdapter PPRAdapter = new SqlDataAdapter("procPaymentPlanResultRegistration",conn);
				DataSet dsPPR = new DataSet();
				PPRAdapter.Fill(dsPPR);
				this.SettleDropDown.DataSource = dsPPR.Tables[0];
				this.SettleDropDown.DataTextField = dsPPR.Tables[0].Columns[1].ToString();
				this.SettleDropDown.DataValueField = dsPPR.Tables[0].Columns[0].ToString();
				this.SettleDropDown.DataBind();
				this.SettleDropDown.Items.Insert(0,"---선택---");
				this.SettleDropDown.Items[0].Value = "";
				
				//결제은행1 드롭다운리스트에 바인딩
				this.BankingDropdown1.DataSource = dsPPR.Tables[1];
				this.BankingDropdown1.DataTextField = dsPPR.Tables[1].Columns[1].ToString();
				this.BankingDropdown1.DataValueField = dsPPR.Tables[1].Columns[0].ToString();
				this.BankingDropdown1.DataBind();
				this.BankingDropdown1.Items.Insert(0,"---선택---");
				this.BankingDropdown1.Items[0].Value = "";

				//결제은행2 드롭다운리스트에 바인딩
				this.BankingDropdown2.DataSource = dsPPR.Tables[1];
				this.BankingDropdown2.DataTextField = dsPPR.Tables[1].Columns[1].ToString();
				this.BankingDropdown2.DataValueField = dsPPR.Tables[1].Columns[0].ToString();
				this.BankingDropdown2.DataBind();
				this.BankingDropdown2.Items.Insert(0,"---선택---");
				this.BankingDropdown2.Items[0].Value = "";

				//결제은행3 드롭다운리스트에 바인딩
				this.BankingDropdown3.DataSource = dsPPR.Tables[1];
				this.BankingDropdown3.DataTextField = dsPPR.Tables[1].Columns[1].ToString();
				this.BankingDropdown3.DataValueField = dsPPR.Tables[1].Columns[0].ToString();
				this.BankingDropdown3.DataBind();
				this.BankingDropdown3.Items.Insert(0,"---선택---");
				this.BankingDropdown3.Items[0].Value = "";
				
			
				// 수정버튼에 대하여 onclick속성을 추가 시킴
				btnEdit.Attributes.Add("onClick","return Confirm('수정을 하시겠습니까?');");
				btnInit.Attributes.Add("onClick","return ResetBox()");
				btnDelete.Attributes.Add("onClick", "return OK('선택한 항목을 삭제');");

				tb_ItemPaymentCost.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this);");
				// onkeyup 이벤트가 발생하면 콤마를 찍고
				tb_ItemPaymentCost.Attributes.Add("OnKeyUp", "Process(); OnKeyUp_Currency(this);");

				// focus를 얻으면 자동으로 select()
				tb_ItemPaymentCost.Attributes.Add("OnFocus", "OnFocus_Obj(this);");

				// focus를 잃으면 다시 입력란을 체크
				tb_ItemPaymentCost.Attributes.Add("OnBlur", "OnBlur_Cur(this);");

				tb_SupplementaryValueTaxPaymentCost.Attributes.Add("OnKeyDown","OnKeyDown_Float(this);");
				tb_SupplementaryValueTaxPaymentCost.Attributes.Add("OnKeyUp", "OnKeyUp_Currency(this)");
				tb_SupplementaryValueTaxPaymentCost.Attributes.Add("onblur","OnBlur_Cur(this)");
				tb_SupplementaryValueTaxPaymentCost.Attributes.Add("onfocus","OnFocus_Obj(this)");

				PaymentDate.NullDateLabel = DateTime.Now.ToShortDateString();

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
			this.LinkButton1.Click += new System.EventHandler(this.LinkButton1_Click);
			this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnInput.Click += new System.EventHandler(this.btnInput_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		//검색항목들 초기화
		private void btnInit_Click(object sender, System.EventArgs e)
		{
			this.PaymentDate.Value = DateTime.Now.ToShortDateString();//지급일 초기화
			this.tb_ItemPaymentCost.Text = Convert.ToString(0); //수금액초기화
			this.tb_SupplementaryValueTaxPaymentCost.Text = Convert.ToString(0);//지급액 초기화
			this.txtEvidence.Text = null;//증거자료 초기화
			this.txtDraftNum1.Text = null;//어음번호1 초기화
			this.txtDraftNum2.Text = null;//어음번호2 초기화
			this.txtDraftNum3.Text = null;//어음번호3 초기화
			this.SettleDate1.Value = null;//결제일자1 초기화
			this.SettleDate2.Value = null;//결제일자2 초기화
			this.SettleDate3.Value = null;//결제일자3 초기화
			this.SettleDropDown.SelectedIndex = 0;//결제방법 초기화
			this.BankingDropdown1.SelectedIndex = 0;//결제은행1 초기화
			this.BankingDropdown2.SelectedIndex = 0;//결제은행2 초기화
			this.BankingDropdown3.SelectedIndex = 0;//결제은행3 초기화
			this.hdindex.Value = null;//지급계획원장 인덱스 Hidden필드 초기화
		}

		//지급계획/실적등록 
		private void btnInput_Click(object sender, System.EventArgs e)
		{			
			ArrayList ArrayPayment = new ArrayList();
			
			ArrayPayment.Add(CSC1.Company);//거래처명
			ArrayPayment.Add(CSC1.BusinessRegistrationNum);//사업자등록번호
			ArrayPayment.Add(this.PaymentDate.Text);//지급일
			ArrayPayment.Add(this.tb_ItemPaymentCost.Text);//수금액(물품대)
			ArrayPayment.Add(this.tb_SupplementaryValueTaxPaymentCost.Text);//지금액(부가세)
			ArrayPayment.Add(this.SettleDropDown.SelectedItem.Value);//결제방법코드
			ArrayPayment.Add(this.SettleDropDown.SelectedItem.Text);//결제방법			
			ArrayPayment.Add("");//증거자료
			ArrayPayment.Add(this.txtDraftNum1.Text);//어음번호1
			ArrayPayment.Add(this.SettleDate1.Text);//결제일자1
			ArrayPayment.Add(this.BankingDropdown1.SelectedItem.Value);//결제은행1코드
			if(BankingDropdown1.SelectedItem.Value == "")
				ArrayPayment.Add("");//결제은행1
			else
				ArrayPayment.Add(this.BankingDropdown1.SelectedItem.Text);//결제은행1
			ArrayPayment.Add(this.txtDraftNum2.Text);//어음번호2
			ArrayPayment.Add(this.SettleDate2.Value);//결제일자2
			
			ArrayPayment.Add(this.BankingDropdown2.SelectedItem.Value);//결제은행2코드
			if(BankingDropdown2.SelectedItem.Value == "")
				ArrayPayment.Add("");//결제은행2
			else
			ArrayPayment.Add(this.BankingDropdown2.SelectedItem.Text);//결제은행2
			ArrayPayment.Add(this.txtDraftNum3.Text);//어음번호3
			ArrayPayment.Add(this.SettleDate3.Value);//결제일자3
			ArrayPayment.Add(this.BankingDropdown3.SelectedItem.Value);//결제은행3코드
			if(BankingDropdown3.SelectedItem.Value == "")
				ArrayPayment.Add("");//결제은행3
			else
			ArrayPayment.Add(this.BankingDropdown3.SelectedItem.Text);//결제은행3
			

			KIT_ERP.Registration registration = new Registration("PaymentPlanResultRegistration",Session["ID"].ToString(),"",ArrayPayment);
			uwgP_HT.DataSource= registration.MainTableRegistration();
			uwgP_HT.DataBind();
		}

		//그리드의 항목중에서 삭제를 하고자 하는 품목을 선택하고 삭제버튼을 누름
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			//거래처 WebCombo에 데이타가 없을경우 삭제를 할 수가 없다.
			if(this.hdindex.Value == null)
			{
				Response.Write("<script language='javascript'>") ;
				Response.Write("alert('삭제할 품목이 선택되지 않았습니다.그리드에서 삭제 할 품목을 선택해 주세요.')") ;
				Response.Write("</script>") ;
			}
			else
			{
				ArrayList ArrayPayment = new ArrayList();

				ArrayPayment.Add(CSC1.BusinessRegistrationNum);//사업자등록번호
				ArrayPayment.Add(this.tb_ItemPaymentCost.Text);//수금액(물품대)
				ArrayPayment.Add(hdYear.Value);//지급일
				ArrayPayment.Add(hdMon.Value);//지급일

				KIT_ERP.Registration delete = new Registration("PaymentPlanResultRegistration",hdindex.Value,ArrayPayment);
				uwgP_HT.DataSource=delete.MainTableDelete();
				uwgP_HT.DataBind();
			}

		}

		//그리드의 내용을 선택하고 내용을 수정하고 수정버튼을 누름
		private void btnEdit_Click(object sender, System.EventArgs e)
		{
													
			ArrayList EditArray = new ArrayList();
			EditArray.Add(CSC1.Company);//거래처명
			EditArray.Add(CSC1.BusinessRegistrationNum);//사업자등록번호
			EditArray.Add(this.PaymentDate.Text);//지급일
			EditArray.Add(this.tb_ItemPaymentCost.Text);//수금액
			EditArray.Add(this.tb_SupplementaryValueTaxPaymentCost.Text);//지급액
			EditArray.Add(this.SettleDropDown.SelectedItem.Value);//결제방법코드
			EditArray.Add(this.SettleDropDown.SelectedItem.Text);//결제방법
			EditArray.Add(this.txtEvidence.Text);//증거자료
			////////////      어음번호1,결제일자1,결제은행1,결제은행1   //////////////
			EditArray.Add(this.txtDraftNum1.Text);
			EditArray.Add(this.SettleDate1.Value);
			EditArray.Add(this.BankingDropdown1.SelectedItem.Value);
			if(BankingDropdown1.SelectedItem.Value == "")
				EditArray.Add("");
			else
				EditArray.Add(this.BankingDropdown1.SelectedItem.Text);

			////////////      어음번호2,결제일자2,결제은행2,결제은행2   //////////////
			EditArray.Add(this.txtDraftNum2.Text);
			EditArray.Add(this.SettleDate2.Value);
			
			EditArray.Add(this.BankingDropdown2.SelectedItem.Value);
			if(BankingDropdown2.SelectedItem.Value == "")
				EditArray.Add("");
			else
				EditArray.Add(this.BankingDropdown2.SelectedItem.Text);

			////////////      어음번호3,결제일자3,결제은행3,결제은행3   //////////////
			EditArray.Add(this.txtDraftNum3.Text);
			EditArray.Add(this.SettleDate3.Value);
			EditArray.Add(this.BankingDropdown3.SelectedItem.Value);
			if(BankingDropdown3.SelectedItem.Value == "")
				EditArray.Add("");
			else
				EditArray.Add(this.BankingDropdown3.SelectedItem.Text);

			EditArray.Add(decimal.Parse(lbOldCost.Value));//기존의 금액
			EditArray.Add(hdYear.Value);//기존의 년도
			EditArray.Add(hdMon.Value);//기존의 월

			


			KIT_ERP.Registration edit = new KIT_ERP.Registration("PaymentPlanResultRegistration",Session["ID"].ToString(),hdindex.Value.ToString(),EditArray);
			this.uwgP_HT.DataSource = edit.MainTableUpdate();
			this.uwgP_HT.DataBind();
		}

		private void LinkButton1_Click(object sender, System.EventArgs e)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = "Select UnPaymentMoney12 From BSI_MT where BusinessRegistrationNum = @num and [Year] = @year";
			comm.Parameters.Add("@num",CSC1.BusinessRegistrationNum);
			comm.Parameters.Add("@year",DateTime.Now.Year);
			conn.Open();
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				txtEvidence.Text = dr["UnPaymentMoney12"].ToString();
			}
			conn.Close();
		}

		
	}
}
