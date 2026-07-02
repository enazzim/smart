using System;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;

namespace KIT_ERP.BasisInformation
{
	/// <summary>
	/// CompanyInfo에 대한 요약 설명입니다.
	/// </summary>
	public class CompanyInfo : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label CI_CompanyDivision;
		protected System.Web.UI.WebControls.CheckBox cb_ReceiveingOrderCompany;
		protected System.Web.UI.WebControls.CheckBox cb_BuyingCompany;
		protected System.Web.UI.WebControls.CheckBox cb_OutSideOrderCompany;
		protected System.Web.UI.WebControls.CheckBox cb_CostCompany;
		protected System.Web.UI.WebControls.Label lb_CompanyName;
		protected System.Web.UI.WebControls.TextBox tb_CompanyName;
		protected System.Web.UI.WebControls.Label lb_PresidentName;
		protected System.Web.UI.WebControls.TextBox tb_PresidentName;
		protected System.Web.UI.WebControls.Label lb_BusinessCompanyNum;
		protected System.Web.UI.WebControls.TextBox tb_BusinessCompanyNum;
		protected System.Web.UI.WebControls.Label lb_CorporationRegistrationNum;
		protected System.Web.UI.WebControls.TextBox tb_CorporationRegistrationNum;
		protected System.Web.UI.WebControls.Label lb_BusinessCompanyAddress;
		protected System.Web.UI.WebControls.Button bt_Post1;
		protected System.Web.UI.WebControls.TextBox tb_BusinessCompanyAddress;
		protected System.Web.UI.WebControls.Label lb_TaxBillAddress;
		protected System.Web.UI.WebControls.Button bt_Post2;
		protected System.Web.UI.WebControls.TextBox tb_TaxBillAddress;
		protected System.Web.UI.WebControls.Label lb_HomepageAddress;
		protected System.Web.UI.WebControls.TextBox tb_HomepageAddress;
		protected System.Web.UI.WebControls.Label lb_BusinessClassification;
		protected System.Web.UI.WebControls.TextBox tb_BusinessClassification;
		protected System.Web.UI.WebControls.Label lb_BusinessItem;
		protected System.Web.UI.WebControls.TextBox tb_BusinessItem;
		protected System.Web.UI.WebControls.Label lb_CurrentTradeState;
		protected System.Web.UI.WebControls.DropDownList dl_CurrentTradeState;
		protected System.Web.UI.WebControls.Label lb_SupplementaryValueTax;
		protected System.Web.UI.WebControls.DropDownList dl_SupplementaryValueTax;
		protected System.Web.UI.WebControls.Label lb_SupplementaryValueTaxRate;
		protected System.Web.UI.WebControls.Label lb_TelephoneNum;
		protected System.Web.UI.WebControls.TextBox tb_TelephoneNum;
		protected System.Web.UI.WebControls.Label lb_FaxNum;
		protected System.Web.UI.WebControls.TextBox tb_FaxNum;
		protected System.Web.UI.WebControls.Label lb_TradeClassification1;
		protected System.Web.UI.WebControls.Label lb_TradeClassification2;
		protected System.Web.UI.WebControls.Label lb_TradeClassification3;
		protected System.Web.UI.WebControls.DropDownList dl_TradeClassification3;
		protected System.Web.UI.WebControls.Label lb_SaleStandardDate;
		protected System.Web.UI.WebControls.TextBox tb_SaleStandardDate;
		protected System.Web.UI.WebControls.Label lb_SaleStandardBillDate;
		protected System.Web.UI.WebControls.TextBox tb_SaleStandardBillDate;
		protected System.Web.UI.WebControls.Label lb_BillApprovalStandard;
		protected System.Web.UI.WebControls.DropDownList dl_BillApprovalStandard;
		protected System.Web.UI.WebControls.Label lb_FixPeriodCollectMoneyDate1;
		protected System.Web.UI.WebControls.TextBox tb_FixPeriodCollectMoneyDate1;
		protected System.Web.UI.WebControls.Label lb_FixPeriodCollectMoneyDate2;
		protected System.Web.UI.WebControls.TextBox tb_FixPeriodCollectMoneyDate2;
		protected System.Web.UI.WebControls.Label lb_CompanyPersonInCharge;
		protected System.Web.UI.WebControls.TextBox tb_CompanyPersonInCharge;
		protected System.Web.UI.WebControls.TextBox tb_Email;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Button bt_Reference;
		protected System.Web.UI.WebControls.Label lb_Index;
		protected System.Web.UI.WebControls.Button bt_Registration;
		protected System.Web.UI.WebControls.Button bt_Delete;
		protected System.Web.UI.WebControls.Button bt_Update;
		protected System.Web.UI.WebControls.Label lb_CompanyChoice;
		protected System.Web.UI.WebControls.Label lb_CompanyIndex;
		protected System.Web.UI.WebControls.DropDownList dl_TradeClassification1;
		protected System.Web.UI.WebControls.DropDownList dl_TradeClassification2;
		protected System.Web.UI.WebControls.Button bt_Clear;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_ReferenceIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_ReferenceTable;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid Ultrawebgrid1;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		protected System.Web.UI.WebControls.TextBox tb_SupplementaryValueTaxRate;
		protected System.Web.UI.WebControls.LinkButton Linkbutton2;
		protected KIT_ERP.BasisInformation.CompanyInfo dsCompany;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			CSC1.UnitCostDistinction= "";
			CSC1.IsDoPost1= true;

			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			if(!Page.IsPostBack)
			{

				////Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 기준정보 > 거래처정보';</script>");

				bt_Registration.Attributes.Add("onClick", "return OK('입력한 항목을 등록');");
				bt_Update.Attributes.Add("onClick", "return OK('선택한 항목을 수정');");
				bt_Delete.Attributes.Add("onClick", "return OK('선택한 항목을 삭제');");

				tb_SupplementaryValueTaxRate.Attributes.Add("OnKeyDown", "OnKeyDown_Currency(this);");
				tb_SupplementaryValueTaxRate.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				bt_Clear.Attributes.Add("onClick", "ResetBox()");
				page_load();
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
			this.Linkbutton2.Click += new System.EventHandler(this.Linkbutton2_Click);
			this.LinkButton1.Click += new System.EventHandler(this.LinkButton1_Click);
			this.bt_Post1.Click += new System.EventHandler(this.bt_Post1_Click);
			this.bt_Post2.Click += new System.EventHandler(this.bt_Post2_Click);
			this.bt_Reference.Click += new System.EventHandler(this.bt_Reference_Click);
			this.bt_Clear.Click += new System.EventHandler(this.bt_Clear_Click);
			this.bt_Update.Click += new System.EventHandler(this.bt_Update_Click);
			this.bt_Delete.Click += new System.EventHandler(this.bt_Delete_Click);
			this.bt_Registration.Click += new System.EventHandler(this.bt_Registration_Click);
			this.UltraWebGrid1.DblClick += new Infragistics.WebUI.UltraWebGrid.ClickEventHandler(this.UltraWebGrid1_DblClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void page_load()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			
			//	*************************************************
			//	**  거래처분류1 드롭다운리스트... 데이타바인딩... **
			//	*************************************************
			//코드분류표에서 대분류명이 거래처분류1인 소분류명을 가지고 옴.
			string str1 = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationName = '거래처분류1' " ;
			SqlCommand comm1 = new SqlCommand(str1,conn);
			SqlDataAdapter da1 = new SqlDataAdapter(comm1) ;
			DataSet ds1 = new DataSet() ;
			da1.Fill(ds1);
				
			dl_TradeClassification1.DataSource = ds1;
			dl_TradeClassification1.DataTextField = ds1.Tables[0].Columns[0].ToString();
			dl_TradeClassification1.DataValueField = ds1.Tables[0].Columns[1].ToString();
			dl_TradeClassification1.DataBind();
			dl_TradeClassification1.Items.Insert(0, "-선택-") ;
			dl_TradeClassification1.Items[0].Value = "";

			//	*************************************************
			//	**  거래처분류2 드롭다운리스트... 데이타바인딩... **
			//	*************************************************
			//코드분류표에서 대분류명이 거래처분류2인 소분류명을 가지고 옴.
			string str2 = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationName = '거래처분류2'" ;
			SqlCommand comm2 = new SqlCommand(str2,conn);
			SqlDataAdapter da2 = new SqlDataAdapter(comm2) ;
			DataSet ds2 = new DataSet() ;
			da2.Fill(ds2);
				
			dl_TradeClassification2.DataSource = ds2;
			dl_TradeClassification2.DataTextField = ds2.Tables[0].Columns[0].ToString();
			dl_TradeClassification2.DataValueField = ds2.Tables[0].Columns[1].ToString();
			dl_TradeClassification2.DataBind();
			dl_TradeClassification2.Items.Insert(0, "-선택-") ;
			dl_TradeClassification2.Items[0].Value = "";

			//	*************************************************
			//	**  거래처분류3 드롭다운리스트... 데이타바인딩... **
			//	*************************************************
			//코드분류표에서 대분류명이 거래처분류2인 소분류명을 가지고 옴.
			string str3 = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationName = '거래처분류3'" ;
			SqlCommand comm3 = new SqlCommand(str3,conn);
			SqlDataAdapter da3 = new SqlDataAdapter(comm3) ;
			DataSet ds3 = new DataSet() ;
			da3.Fill(ds3);
				
			dl_TradeClassification3.DataSource = ds3;
			dl_TradeClassification3.DataTextField = ds3.Tables[0].Columns[0].ToString();
			dl_TradeClassification3.DataValueField = ds3.Tables[0].Columns[1].ToString();
			dl_TradeClassification3.DataBind();
			dl_TradeClassification3.Items.Insert(0, "-선택-") ;
			dl_TradeClassification3.Items[0].Value = "";

			
		
		}

	

		// 등록버튼 클릭
		private void bt_Registration_Click(object sender, System.EventArgs e)
		{
			

			if(Page.IsValid == true)
			{
				string m_RequirePage = "CompanyInfo";
				string m_RequireAction = "Registration";
				string m_TableName = "CI_MT";
				int m_Idx = (int)PageName.CompanyInfo;
				string m_User = Session["ID"].ToString();
			
				ArrayList InputList = new ArrayList();



				int ROC=0,OOC=0,BC=0,CC=0;
			
				// 먼저 거래처분류를 확인한다
				if(cb_ReceiveingOrderCompany.Checked)
					ROC = 1;
				if(cb_OutSideOrderCompany.Checked)
					OOC = 1;
				if(cb_BuyingCompany.Checked)
					BC = 1;
				if(cb_CostCompany.Checked)
					CC = 1;

				
				
				
				InputList.Add(ROC);
				InputList.Add(OOC);
				InputList.Add(BC);
				InputList.Add(CC);
				InputList.Add(tb_CompanyName.Text);
				InputList.Add(tb_PresidentName.Text);
				InputList.Add(tb_BusinessCompanyNum.Text);
				InputList.Add(tb_CorporationRegistrationNum.Text);
				InputList.Add(tb_BusinessCompanyAddress.Text);
				InputList.Add(tb_TaxBillAddress.Text);
				InputList.Add(tb_HomepageAddress.Text);
				InputList.Add(tb_BusinessClassification.Text);
				InputList.Add(tb_BusinessItem.Text);
				InputList.Add(dl_CurrentTradeState.SelectedItem.Value);
				InputList.Add(dl_SupplementaryValueTax.SelectedItem.Value);
				InputList.Add(tb_SupplementaryValueTaxRate.Text);
				InputList.Add(tb_TelephoneNum.Text);
				InputList.Add(tb_FaxNum.Text);
				InputList.Add(dl_TradeClassification1.SelectedItem.Value);
				InputList.Add(dl_TradeClassification2.SelectedItem.Value);
				InputList.Add(dl_TradeClassification3.SelectedItem.Value);
				InputList.Add(tb_SaleStandardDate.Text);
				InputList.Add(tb_SaleStandardBillDate.Text);
				InputList.Add(dl_BillApprovalStandard.SelectedItem.Value);
				InputList.Add(tb_FixPeriodCollectMoneyDate1.Text);
				InputList.Add(tb_FixPeriodCollectMoneyDate2.Text);
				InputList.Add(tb_CompanyPersonInCharge.Text);
				InputList.Add(tb_Email.Text);



				MasterInfoRecordRUD rud = new MasterInfoRecordRUD(m_RequirePage, m_RequireAction, m_TableName, m_Idx, m_User, InputList,"낱개입력");
				rud.ActivateRUD();
				Label1.Text = "";
				Label2.Text = "";

				bt_Delete.Enabled = false;
				bt_Update.Enabled = false;
				bt_Registration.Enabled = true;
				
			}
			//FindIndex();
			
			
		}

		//수정버튼 클릭
		private void bt_Update_Click(object sender, System.EventArgs e)
		{
			if(Page.IsValid == true)
			{
				if(lb_CompanyIndex.Text == "")
				{
					RegisterStartupScript("","<script>alert('등록후 바로 수정은 불가능 합니다!');</script>");
				}
				else
				{
					string m_RequirePage = "CompanyInfo";
					string m_RequireAction = "Update";
					string m_TableName = "CI_MT";
					int m_Idx = (int)PageName.CompanyInfo;
					string m_User = Session["ID"].ToString();
			
					ArrayList InputList = new ArrayList();

					int ROC=0,OOC=0,BC=0,CC=0;
			
					// 먼저 거래처분류를 확인한다
					if(cb_ReceiveingOrderCompany.Checked)
						ROC = 1;
					if(cb_OutSideOrderCompany.Checked)
						OOC = 1;
					if(cb_BuyingCompany.Checked)
						BC = 1;
					if(cb_CostCompany.Checked)
						CC = 1;
			

					InputList.Add(ROC);
					InputList.Add(OOC);
					InputList.Add(BC);
					InputList.Add(CC);
					InputList.Add(tb_CompanyName.Text);
					InputList.Add(tb_PresidentName.Text);
					InputList.Add(tb_BusinessCompanyNum.Text);
					InputList.Add(tb_CorporationRegistrationNum.Text);
					InputList.Add(tb_BusinessCompanyAddress.Text);
					InputList.Add(tb_TaxBillAddress.Text);
					InputList.Add(tb_HomepageAddress.Text);
					InputList.Add(tb_BusinessClassification.Text);
					InputList.Add(tb_BusinessItem.Text);
					InputList.Add(dl_CurrentTradeState.SelectedItem.Value);
					InputList.Add(dl_SupplementaryValueTax.SelectedItem.Value);
					InputList.Add(tb_SupplementaryValueTaxRate.Text);
					InputList.Add(tb_TelephoneNum.Text);
					InputList.Add(tb_FaxNum.Text);
					InputList.Add(dl_TradeClassification1.SelectedItem.Value);
					InputList.Add(dl_TradeClassification2.SelectedItem.Value);
					InputList.Add(dl_TradeClassification3.SelectedItem.Value);
					InputList.Add(tb_SaleStandardDate.Text);
					InputList.Add(tb_SaleStandardBillDate.Text);
					InputList.Add(dl_BillApprovalStandard.SelectedItem.Value);
					InputList.Add(tb_FixPeriodCollectMoneyDate1.Text);
					InputList.Add(tb_FixPeriodCollectMoneyDate2.Text);
					InputList.Add(tb_CompanyPersonInCharge.Text);
					InputList.Add(tb_Email.Text);
					InputList.Add(lb_CompanyIndex.Text);

					MasterInfoRecordRUD rud = new MasterInfoRecordRUD(m_RequirePage, m_RequireAction, m_TableName, m_Idx, m_User, InputList,"낱개입력");
					rud.ActivateRUD();

					bt_Delete.Enabled = false;
					bt_Update.Enabled = false;
					bt_Registration.Enabled = true;
				}
			}
		}


        // 삭제버튼 클릭
		private void bt_Delete_Click(object sender, System.EventArgs e)
		{
			
			if(Page.IsValid == true)
			{
				if(lb_CompanyIndex.Text == "")
				{
					RegisterStartupScript("","<script>alert('등록후 바로 삭제는 불가능 합니다!');</script>");
				}
				else
				{
					string m_RequirePage = "CompanyInfo";
					string m_RequireAction = "Delete";
					string m_TableName = "CI_MT";
					int m_Idx = (int)PageName.CompanyInfo;
					string m_User = Session["ID"].ToString();
			
					ArrayList InputList = new ArrayList();

					int ROC=0,OOC=0,BC=0,CC=0;
			
					// 먼저 거래처분류를 확인한다
					if(cb_ReceiveingOrderCompany.Checked)
						ROC = 1;
					if(cb_OutSideOrderCompany.Checked)
						OOC = 1;
					if(cb_BuyingCompany.Checked)
						BC = 1;
					if(cb_CostCompany.Checked)
						CC = 1;
			

					InputList.Add(ROC);
					InputList.Add(OOC);
					InputList.Add(BC);
					InputList.Add(CC);
					InputList.Add(tb_CompanyName.Text);
					InputList.Add(tb_PresidentName.Text);
					InputList.Add(tb_BusinessCompanyNum.Text);
					InputList.Add(tb_CorporationRegistrationNum.Text);
					InputList.Add(tb_BusinessCompanyAddress.Text);
					InputList.Add(tb_TaxBillAddress.Text);
					InputList.Add(tb_HomepageAddress.Text);
					InputList.Add(tb_BusinessClassification.Text);
					InputList.Add(tb_BusinessItem.Text);
					InputList.Add(dl_CurrentTradeState.SelectedItem.Value);
					InputList.Add(dl_SupplementaryValueTax.SelectedItem.Value);
					InputList.Add(tb_SupplementaryValueTaxRate.Text);
					InputList.Add(tb_TelephoneNum.Text);
					InputList.Add(tb_FaxNum.Text);
					InputList.Add(dl_TradeClassification1.SelectedItem.Value);
					InputList.Add(dl_TradeClassification2.SelectedItem.Value);
					InputList.Add(dl_TradeClassification3.SelectedItem.Value);
					InputList.Add(tb_SaleStandardDate.Text);
					InputList.Add(tb_SaleStandardBillDate.Text);
					InputList.Add(dl_BillApprovalStandard.SelectedItem.Value);
					InputList.Add(tb_FixPeriodCollectMoneyDate1.Text);
					InputList.Add(tb_FixPeriodCollectMoneyDate2.Text);
					InputList.Add(tb_CompanyPersonInCharge.Text);
					InputList.Add(tb_Email.Text);
					InputList.Add(lb_CompanyIndex.Text);

				
					MasterInfoRecordRUD rud = new MasterInfoRecordRUD(m_RequirePage, m_RequireAction, m_TableName, m_Idx, m_User, InputList,"낱개입력");
					rud.ActivateRUD();	


					cb_ReceiveingOrderCompany.Checked = false;
					cb_OutSideOrderCompany.Checked = false;
					cb_BuyingCompany.Checked = false;
					cb_CostCompany.Checked = false;
					tb_CompanyName.Text = "";
					tb_PresidentName.Text = "";
					tb_BusinessCompanyNum.Text = "";
					tb_CorporationRegistrationNum.Text = "";
					tb_BusinessCompanyAddress.Text = "";
					tb_TaxBillAddress.Text = "";
					tb_HomepageAddress.Text = "";
					tb_BusinessClassification.Text = "";
					tb_BusinessItem.Text = "";
					dl_CurrentTradeState.SelectedIndex = 0;
					dl_SupplementaryValueTax.SelectedIndex = 0;
					tb_SupplementaryValueTaxRate.Text = "10";
					tb_TelephoneNum.Text = "";
					tb_FaxNum.Text = "";
					dl_TradeClassification1.SelectedIndex = 0;
					dl_TradeClassification2.SelectedIndex = 0;
					dl_TradeClassification3.SelectedIndex = 0;
					tb_SaleStandardDate.Text = "";
					tb_SaleStandardBillDate.Text = "";
					dl_BillApprovalStandard.SelectedIndex = 0;
					tb_FixPeriodCollectMoneyDate1.Text = "";
					tb_FixPeriodCollectMoneyDate2.Text = "";
					tb_CompanyPersonInCharge.Text = "";
					tb_Email.Text = "";
					lb_Index.Text = "";
					lb_CompanyIndex.Text = "";
					bt_Delete.Enabled = false;
					bt_Update.Enabled = false;
					bt_Registration.Enabled = true;		
				}

			}
		}


		/// <summary>
		/// 초기화버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Clear_Click(object sender, System.EventArgs e)
		{
			cb_ReceiveingOrderCompany.Checked = false;
			cb_OutSideOrderCompany.Checked = false;
			cb_BuyingCompany.Checked = false;
			cb_CostCompany.Checked = false;
			tb_CompanyName.Text = "";
			tb_PresidentName.Text = "";
			tb_BusinessCompanyNum.Text = "";
			tb_CorporationRegistrationNum.Text = "";
			tb_BusinessCompanyAddress.Text = "";
			tb_TaxBillAddress.Text = "";
			tb_HomepageAddress.Text = "";
			tb_BusinessClassification.Text = "";
			tb_BusinessItem.Text = "";
			dl_CurrentTradeState.SelectedIndex = 0;
			dl_SupplementaryValueTax.SelectedIndex = 0;
			tb_SupplementaryValueTaxRate.Text = "10";
			tb_TelephoneNum.Text = "";
			tb_FaxNum.Text = "";
			dl_TradeClassification1.SelectedIndex = 0;
			dl_TradeClassification2.SelectedIndex = 0;
			dl_TradeClassification3.SelectedIndex = 0;
			tb_SaleStandardDate.Text = "";
			tb_SaleStandardBillDate.Text = "";
			dl_BillApprovalStandard.SelectedIndex = 0;
			tb_FixPeriodCollectMoneyDate1.Text = "";
			tb_FixPeriodCollectMoneyDate2.Text = "";
			tb_CompanyPersonInCharge.Text = "";
			tb_Email.Text = "";
			lb_Index.Text = "";
			lb_CompanyIndex.Text = "";

			bt_Delete.Enabled = false;
			bt_Update.Enabled = false;
			bt_Registration.Enabled = true;
		}

		/// <summary>
		/// 참고보기버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Reference_Click(object sender, System.EventArgs e)
		{
			if(lb_Index.Text == "")
			{
				RegisterStartupScript("","<script>alert('항목을 선택후 버튼을 눌러주세요');</script>");
			}
			else
			{

				string m_TableName = "거래처정보";
				int m_RecodeIdx = int.Parse(lb_Index.Text);
			

				Reference rf = new Reference(m_TableName, m_RecodeIdx);
				UltraWebGrid1.DataSource = rf.reference();
				UltraWebGrid1.DataBind();
			}

			
		}


		/// <summary>
		/// 사업장주소 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Post1_Click(object sender, System.EventArgs e)
		{
			string text = "tb_BusinessCompanyAddress";
			RegisterStartupScript("","<script language=javascript>window.open('./Popup/PostSearch.aspx?formID=CompanyInfo&text="+text+"','PostSearch'," + "'width=300" + "," + "height=300px');</script>");
		}

		/// <summary>
		/// 세금계산서 주소 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Post2_Click(object sender, System.EventArgs e)
		{
			string text = "tb_TaxBillAddress";
			RegisterStartupScript("","<script language=javascript>window.open('./Popup/PostSearch.aspx?formID=CompanyInfo&text="+text+"','PostSearch'," + "'width=300" + "," + "height=300px');</script>");
		}		
		
		/// <summary>
		/// 등록후 다시 바인딩 되면서 제일큰 인덱스 번호를 lb_CompanyIndex에 저장하는 함수
		/// </summary>
		private void FindIndex()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "SELECT Max(CompanyInfoIndex) From CI_MT where RecodingState = 1 ";
			SqlCommand comm =  new SqlCommand(str,conn);
			lb_CompanyIndex.Text = comm.ExecuteScalar().ToString();
			conn.Close();
		}

		/// <summary>
		/// 어깨창 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LinkButton1_Click(object sender, System.EventArgs e)
		{
			RegisterStartupScript("","<script>window.open('./Popup/CompanyAll.aspx','CompanyAll','width=830,height=500px');</script>");
		}

		/// <summary>
		/// 참고보고 그리드 더블클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UltraWebGrid1_DblClick(object sender, Infragistics.WebUI.UltraWebGrid.ClickEventArgs e)
		{
			string table = lb_ReferenceTable.Value;
			string index = lb_ReferenceIndex.Value;

			if(table == "생산품질")
				table = "T_Product";
			else if(table =="개발실")
				table = "T_Development";
			else
				table = "T_Materials";
			
			//RegisterStartupScript("","<script>window.open('./Popup/View.aspx?table=" + table + "&index=" + index + "','Reference'," + "'width=510" + "," + "height=435px');</script>");
			RegisterStartupScript("","<script>window.open('./Popup/View1.aspx?table=" + table + "&index=" + index + "','Reference'," + "'width=818" + "," + "height=610px');</script>");
		}

		private void Linkbutton2_Click(object sender, System.EventArgs e)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select * From CI_MT Where RecodingState = 1 and BusinessRegistrationNum = @BusinessRegistrationNum";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@BusinessRegistrationNum",CSC1.BusinessRegistrationNum);
			SqlDataReader dr = comm.ExecuteReader();

			while(dr.Read())
			{
				cb_ReceiveingOrderCompany.Checked = Boolean.Parse(dr["ReceiveingOrderCompany"].ToString());
				cb_OutSideOrderCompany.Checked = Boolean.Parse(dr["OutSideOrderCompany"].ToString());
				cb_BuyingCompany.Checked = Boolean.Parse(dr["BuyingCompany"].ToString());
				cb_CostCompany.Checked = Boolean.Parse(dr["CostCompany"].ToString());
				tb_CompanyName.Text = dr["CompanyName"].ToString();
				tb_PresidentName.Text = dr["PresidentName"].ToString();
				tb_BusinessCompanyNum.Text = dr["BusinessRegistrationNum"].ToString();
				tb_CorporationRegistrationNum.Text = dr["CorporationRegistrationNum"].ToString();
				tb_BusinessCompanyAddress.Text = dr["BusinessCompanyAddress"].ToString();
				tb_TaxBillAddress.Text = dr["TaxBillAddress"].ToString();
				tb_HomepageAddress.Text = dr["HomepageAddress"].ToString();
				tb_BusinessClassification.Text = dr["BusinessClassification"].ToString();
				tb_BusinessItem.Text = dr["BusinessItem"].ToString();

				for (int i=0 ; i < dl_CurrentTradeState.Items.Count ; i++)
				{
					if( dl_CurrentTradeState.Items[i].Value == dr["CurrentTradeState"].ToString())
					{
						dl_CurrentTradeState.SelectedIndex = i;				
					}
				}
				for (int i=0 ; i < dl_SupplementaryValueTax.Items.Count ; i++)
				{
					if( dl_SupplementaryValueTax.Items[i].Value == dr["SupplementaryValueTax"].ToString())
					{
						dl_SupplementaryValueTax.SelectedIndex = i;				
					}
				}

				tb_SupplementaryValueTaxRate.Text = dr["SupplementaryValueTaxRate"].ToString();
				tb_TelephoneNum.Text = dr["TelephoneNum"].ToString();
				tb_FaxNum.Text = dr["FaxNum"].ToString();

				for (int i=0 ; i < dl_TradeClassification1.Items.Count ; i++)
				{
					if( dl_TradeClassification1.Items[i].Value == dr["TradeClassification1"].ToString())
					{
						dl_TradeClassification1.SelectedIndex = i;				
					}
				}
				for (int i=0 ; i < dl_TradeClassification2.Items.Count ; i++)
				{
					if( dl_TradeClassification2.Items[i].Value == dr["TradeClassification2"].ToString())
					{
						dl_TradeClassification2.SelectedIndex = i;				
					}
				}
				for (int i=0 ; i < dl_TradeClassification3.Items.Count ; i++)
				{
					if( dl_TradeClassification3.Items[i].Value == dr["TradeClassification3"].ToString())
					{
						dl_TradeClassification3.SelectedIndex = i;				
					}
				}
				tb_SaleStandardDate.Text = dr["SaleStandardDate"].ToString();
				tb_SaleStandardBillDate.Text = dr["SaleStandardBillDate"].ToString();
				
				for (int i=0 ; i < dl_BillApprovalStandard.Items.Count ; i++)
				{
					if( dl_BillApprovalStandard.Items[i].Value == dr["BillApprovalStandard"].ToString())
					{
						dl_BillApprovalStandard.SelectedIndex = i;				
					}
				}				
				tb_FixPeriodCollectMoneyDate1.Text = dr["FixPeriodCollectMoneyDate1"].ToString();
				tb_FixPeriodCollectMoneyDate1.Text = dr["FixPeriodCollectMoneyDate2"].ToString();
				tb_CompanyPersonInCharge.Text = dr["CompanyPersonInCharge"].ToString();
				tb_Email.Text = dr["CompanyPersonInChargeEmail"].ToString();
				lb_CompanyIndex.Text = dr["CompanyInfoIndex"].ToString();
				lb_Index.Text = lb_CompanyIndex.Text;
			}
			
			bt_Delete.Enabled = true;
			bt_Update.Enabled = true;
			bt_Registration.Enabled = false;
		}
	}
}

