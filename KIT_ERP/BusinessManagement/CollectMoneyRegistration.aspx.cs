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

namespace KIT_ERP
{
	/// <summary>
	/// CollectMoneyRegistration에 대한 요약 설명입니다.
	/// </summary>
	public class CollectMoneyRegistration : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label searchTitle;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Label Label17;
		protected System.Web.UI.WebControls.Label Label18;
		protected System.Web.UI.WebControls.Label Label19;
		protected System.Web.UI.WebControls.Label Label20;
		protected System.Web.UI.WebControls.Label Label21;
		protected System.Web.UI.WebControls.Label Label22;
		protected System.Web.UI.WebControls.Label Label23;
		protected System.Web.UI.WebControls.Label Label24;
		protected System.Web.UI.WebControls.Label Label25;
		protected System.Web.UI.WebControls.Label Label26;
		protected System.Web.UI.WebControls.Label Label27;
		protected System.Web.UI.WebControls.Label Label28;
		protected System.Web.UI.WebControls.Label Label29;
		protected System.Web.UI.WebControls.Label Label30;
		protected System.Web.UI.WebControls.DropDownList dl_DecisionMethod;
		protected System.Web.UI.WebControls.TextBox tb_BillNum1;
		protected System.Web.UI.WebControls.DropDownList dl_BankName1;
		protected System.Web.UI.WebControls.TextBox tb_BillNum2;
		protected System.Web.UI.WebControls.DropDownList dl_BankName2;
		protected System.Web.UI.WebControls.TextBox tb_BillNum3;
		protected System.Web.UI.WebControls.DropDownList dl_BankName3;
		protected System.Web.UI.WebControls.Button bt_Clear;
		protected System.Web.UI.WebControls.Button bt_Update;
		protected System.Web.UI.WebControls.Button bt_Register;
		protected System.Web.UI.WebControls.TextBox tb_ItemPaymentCost;
		protected System.Web.UI.WebControls.TextBox tb_SupplementaryValueTaxPaymentCost;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Index;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcCollectMoneyDate;
		protected Infragistics.WebUI.WebCombo.WebCombo wcCompany;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcPaymentDate1;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcPaymentDate2;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcPaymentDate;
		protected System.Web.UI.WebControls.Button bt_Delete;
		protected System.Web.UI.WebControls.TextBox txtUncollectMoney;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Hidden1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_OldCost;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Hidden2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_CollectMoneyDate;
		protected System.Web.UI.WebControls.Label Label2;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}
			if(!Page.IsPostBack)
			{
//				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 영업관리 > 수금등록';</script>");

				//bt_Register.Attributes.Add("onClick", "return OK('입력한 금액을 등록');");
				//bt_Update.Attributes.Add("onClick", "return OK('선택한 항목을 수정');");

				bt_Delete.Attributes.Add("onClick", "return OK('선택한 항목을 삭제');");

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


				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				conn.Open();
				string str1 = @"SELECT CompanyName, PresidentName, CI_MT.BusinessRegistrationNum as BusinessRegistrationNum, CompanyInfoIndex, BSI_MT.UncollectMoney12 as UncollectMoney FROM CI_MT 
					inner JOIN BSI_MT On CI_MT.BusinessRegistrationNum = BSI_MT.BusinessRegistrationNum 
					where BSI_MT.RecodingState = 1 and CI_MT.RecodingState = 1 and ReceiveingOrderCompany = 1 and [Year] = @year order by CompanyName";
				SqlCommand comm1 =  new SqlCommand(str1,conn);
				comm1.Parameters.Add("@year",DateTime.Now.Year);
				SqlDataAdapter da1 = new SqlDataAdapter(comm1) ;
				DataSet ds1 = new DataSet() ;
				da1.Fill(ds1);

				wcCompany.DataSource = ds1;
				wcCompany.DataTextField = ds1.Tables[0].Columns[0].ToString();
				wcCompany.DataValueField = ds1.Tables[0].Columns[2].ToString();
				wcCompany.DataBind();


				// 결재방법 
				string str = "SELECT SmallClassificationCode, SmallClassificationName from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '0910' order by SmallClassificationName";
				SqlCommand comm =  new SqlCommand(str,conn);
				SqlDataAdapter da = new SqlDataAdapter(comm) ;
				DataSet ds = new DataSet() ;
				da.Fill(ds,"Code");

				dl_DecisionMethod.DataSource = ds;
				dl_DecisionMethod.DataMember = "Code";
				dl_DecisionMethod.DataTextField = "SmallClassificationName";
				dl_DecisionMethod.DataValueField = "SmallClassificationCode";
				dl_DecisionMethod.DataBind();
				dl_DecisionMethod.Items.Insert(0, "-선택-");
				dl_DecisionMethod.Items[0].Value = "";


				//	*************************************************
				//	**  결재은행 분류 드롭다운리스트... 데이타바인딩... **   
				//	*************************************************
				//코드분류표에서 대분류명이 결재은행인 소분류명을 가지고 옴.
				string str_BankName1 = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '0920' order by SmallClassificationName";
				SqlCommand comm_BankName1 = new SqlCommand(str_BankName1,conn);
				SqlDataAdapter da_BankName1 = new SqlDataAdapter(comm_BankName1) ;
				DataSet ds_BankName1 = new DataSet() ;
				da_BankName1.Fill(ds_BankName1);
				
				dl_BankName1.DataSource = ds_BankName1;
				dl_BankName1.DataTextField = ds_BankName1.Tables[0].Columns[0].ToString();
				dl_BankName1.DataValueField = ds_BankName1.Tables[0].Columns[1].ToString();
				dl_BankName1.DataBind();
				dl_BankName1.Items.Insert(0, "-선택-") ;
				dl_BankName1.Items[0].Value = "";
				//	*************************************************
				//	**  결재은행 분류 드롭다운리스트... 데이타바인딩... **   
				//	*************************************************
				//코드분류표에서 대분류명이 결재은행인 소분류명을 가지고 옴.
				string str_BankName2 = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '0920' order by SmallClassificationName";
				SqlCommand comm_BankName2 = new SqlCommand(str_BankName2,conn);
				SqlDataAdapter da_BankName2 = new SqlDataAdapter(comm_BankName2) ;
				DataSet ds_BankName2 = new DataSet() ;
				da_BankName2.Fill(ds_BankName2);
				
				dl_BankName2.DataSource = ds_BankName2;
				dl_BankName2.DataTextField = ds_BankName2.Tables[0].Columns[0].ToString();
				dl_BankName2.DataValueField = ds_BankName2.Tables[0].Columns[1].ToString();
				dl_BankName2.DataBind();
				dl_BankName2.Items.Insert(0, "-선택-") ;
				dl_BankName2.Items[0].Value = "";
				//	*************************************************
				//	**  결재은행 분류 드롭다운리스트... 데이타바인딩... **   
				//	*************************************************
				//코드분류표에서 대분류명이 결재은행인 소분류명을 가지고 옴.
				string str_BankName3 = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '0920' order by SmallClassificationName";
				SqlCommand comm_BankName3 = new SqlCommand(str_BankName3,conn);
				SqlDataAdapter da_BankName3 = new SqlDataAdapter(comm_BankName3) ;
				DataSet ds_BankName3 = new DataSet() ;
				da_BankName3.Fill(ds_BankName3);
				
				dl_BankName3.DataSource = ds_BankName3;
				dl_BankName3.DataTextField = ds_BankName3.Tables[0].Columns[0].ToString();
				dl_BankName3.DataValueField = ds_BankName3.Tables[0].Columns[1].ToString();
				dl_BankName3.DataBind();
				dl_BankName3.Items.Insert(0, "-선택-") ;
				dl_BankName3.Items[0].Value = "";
				conn.Close();

				wdcCollectMoneyDate.NullDateLabel = DateTime.Now.ToShortDateString();

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
			this.bt_Delete.Click += new System.EventHandler(this.bt_Delete_Click);
			this.bt_Update.Click += new System.EventHandler(this.bt_Update_Click);
			this.bt_Register.Click += new System.EventHandler(this.bt_Register_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void bt_Register_Click(object sender, System.EventArgs e)
		{

			if(wcCompany.DisplayValue == null)
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('거래처를 선택하세요');");
				Response.Write("</script>");
			}
			
			else
			{
				if(wdcCollectMoneyDate.Text.Trim() == "")
				{
					Response.Write("<script language=javascript>");
					Response.Write("alert('수금일자를 선택하세요');");
					Response.Write("</script>");
				}
				else
				{
					string m_RequirePage = "CollectMoneyRegistration";
					string m_User = Session["ID"].ToString();
					string m_RecodeIdx = lb_Index.Value;

					ArrayList InputList = new ArrayList();

					InputList.Add(wcCompany.DisplayValue);
					InputList.Add(wcCompany.DataValue);
					InputList.Add(wdcCollectMoneyDate.Text);

					InputList.Add(tb_ItemPaymentCost.Text);
					InputList.Add(tb_SupplementaryValueTaxPaymentCost.Text);

					InputList.Add(dl_DecisionMethod.SelectedItem.Value);
					if(dl_DecisionMethod.SelectedItem.Value == "")
						InputList.Add("");
					else
						InputList.Add(dl_DecisionMethod.SelectedItem.Text);
					InputList.Add(tb_BillNum1.Text);
					InputList.Add(wdcPaymentDate1.Text);
					InputList.Add(dl_BankName1.SelectedItem.Value);
					if(dl_BankName1.SelectedItem.Value == "")
						InputList.Add("");
					else
						InputList.Add(dl_BankName1.SelectedItem.Text);

					InputList.Add(tb_BillNum2.Text);
					InputList.Add(wdcPaymentDate2.Text);
					InputList.Add(dl_BankName2.SelectedItem.Value);
					if(dl_BankName2.SelectedItem.Value == "")
						InputList.Add("");
					else
						InputList.Add(dl_BankName2.SelectedItem.Text);

					InputList.Add(tb_BillNum3.Text);
					InputList.Add(wdcPaymentDate2.Text);
					InputList.Add(dl_BankName3.SelectedItem.Value);
					if(dl_BankName3.SelectedItem.Value == "")
						InputList.Add("");
					else
						InputList.Add(dl_BankName3.SelectedItem.Text);

					Registration reg = new Registration(m_RequirePage,m_User,m_RecodeIdx, InputList);
			
					UltraWebGrid1.DataSource = reg.MainTableRegistration();
					UltraWebGrid1.DataBind();

					Bind();
				}
			}
		}

		private void bt_Delete_Click(object sender, System.EventArgs e)
		{
			ArrayList InputList = new ArrayList();

			InputList.Add(wcCompany.DataValue);
			InputList.Add(tb_ItemPaymentCost.Text);
			InputList.Add(wdcCollectMoneyDate.Text);

			Registration del = new Registration("CollectMoneyRegistration",lb_Index.Value,InputList);
			UltraWebGrid1.DataSource = del.MainTableDelete();
			UltraWebGrid1.DataBind();
			Clear();

			Bind();

		}

		private void bt_Update_Click(object sender, System.EventArgs e)
		{
		
			if(wcCompany.DisplayValue == null)
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('거래처를 선택하세요');");
				Response.Write("</script>");
			}
			
			else
			{
				if(wdcCollectMoneyDate.Text.Trim() == "")
				{
					Response.Write("<script language=javascript>");
					Response.Write("alert('수금일자를 선택하세요');");
					Response.Write("</script>");
				}
				else
				{
					string m_RequirePage = "CollectMoneyRegistration";
					string m_User = Session["ID"].ToString();
					string m_RecodeIdx = lb_Index.Value;

					ArrayList InputList = new ArrayList();

					InputList.Add(wcCompany.DisplayValue);
					InputList.Add(wcCompany.DataValue);
					InputList.Add(wdcCollectMoneyDate.Text);
					
					InputList.Add(decimal.Parse(tb_ItemPaymentCost.Text));
					InputList.Add(decimal.Parse(tb_SupplementaryValueTaxPaymentCost.Text));
					InputList.Add(dl_DecisionMethod.SelectedItem.Value);
					if(dl_DecisionMethod.SelectedItem.Value == "")
						InputList.Add("");
					else
						InputList.Add(dl_DecisionMethod.SelectedItem.Text);
					InputList.Add(tb_BillNum1.Text);
					InputList.Add(wdcPaymentDate1.Text);
					InputList.Add(dl_BankName1.SelectedItem.Value);
					if(dl_BankName1.SelectedItem.Value == "")
						InputList.Add("");
					else
						InputList.Add(dl_BankName1.SelectedItem.Text);

					InputList.Add(tb_BillNum2.Text);
					InputList.Add(wdcPaymentDate2.Text);
					InputList.Add(dl_BankName2.SelectedItem.Value);
					if(dl_BankName2.SelectedItem.Value == "")
						InputList.Add("");
					else
						InputList.Add(dl_BankName2.SelectedItem.Text);

					InputList.Add(tb_BillNum3.Text);
					InputList.Add(wdcPaymentDate2.Text);
					InputList.Add(dl_BankName3.SelectedItem.Value);
					if(dl_BankName3.SelectedItem.Value == "")
						InputList.Add("");
					else
						InputList.Add(dl_BankName3.SelectedItem.Text);
					InputList.Add(decimal.Parse(lb_OldCost.Value));
					InputList.Add(lb_CollectMoneyDate.Value);


					

					Registration reg = new Registration(m_RequirePage,m_User,m_RecodeIdx, InputList);
			
					UltraWebGrid1.DataSource = reg.MainTableUpdate();
					UltraWebGrid1.DataBind();

					Bind();
				}
			}
		}

		private void bt_Clear_Click(object sender, System.EventArgs e)
		{
			Clear();
			Bind();
		}

		private void Clear()
		{
			wcCompany.DataValue = "";
			wcCompany.DisplayValue = "";
			wdcCollectMoneyDate.Value = DateTime.Now.ToShortDateString();
			wdcPaymentDate1.Value = "";
			wdcPaymentDate2.Value = "";
			wdcPaymentDate.Value = "";
			lb_Index.Value = "0";
			lb_OldCost.Value = "0";
			lb_CollectMoneyDate.Value ="";
			dl_BankName1.SelectedIndex = 0;
			dl_BankName2.SelectedIndex = 0;
			dl_BankName3.SelectedIndex = 0;
			tb_BillNum1.Text = "";
			tb_BillNum2.Text = "";
			tb_BillNum3.Text = "";
			tb_ItemPaymentCost.Text = "";
			tb_SupplementaryValueTaxPaymentCost.Text = "";
			dl_DecisionMethod.SelectedIndex = 0;
			txtUncollectMoney.Text = "0";
		}

		private void Bind()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);

			string str1 = @"SELECT CompanyName, PresidentName, CI_MT.BusinessRegistrationNum as BusinessRegistrationNum, CompanyInfoIndex, BSI_MT.UncollectMoney12 as UncollectMoney FROM CI_MT 
					inner JOIN BSI_MT On CI_MT.BusinessRegistrationNum = BSI_MT.BusinessRegistrationNum 
					where BSI_MT.RecodingState = 1 and CI_MT.RecodingState = 1 and ReceiveingOrderCompany = 1 order by CompanyName";
			SqlCommand comm1 =  new SqlCommand(str1,conn);
			SqlDataAdapter da1 = new SqlDataAdapter(comm1) ;
			DataSet ds1 = new DataSet() ;
			da1.Fill(ds1);

			wcCompany.DataSource = ds1;
			wcCompany.DataTextField = ds1.Tables[0].Columns[0].ToString();
			wcCompany.DataValueField = ds1.Tables[0].Columns[2].ToString();
			wcCompany.DataBind();

		}
	}
}
