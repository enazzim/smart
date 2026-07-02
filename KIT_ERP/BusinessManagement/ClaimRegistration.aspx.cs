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

namespace KIT_ERP.BusinessManagement
{
	/// <summary>
	/// Claim에 대한 요약 설명입니다.
	/// </summary>
	public class ClaimRegistration : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label searchTitle;
		protected System.Web.UI.WebControls.Label Label19;
		protected System.Web.UI.WebControls.Label Label26;
		protected System.Web.UI.WebControls.Label Label27;
		protected System.Web.UI.WebControls.Label Label17;
		protected System.Web.UI.WebControls.Label Label2;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.TextBox tb_Cost;
		protected Infragistics.WebUI.WebCombo.WebCombo wcCompany;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcDate;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox tbQuantity;
		protected System.Web.UI.WebControls.DropDownList ddlClaimStatus;
		protected System.Web.UI.WebControls.DropDownList ddlClaimCause;
		protected System.Web.UI.WebControls.Button bt_Delete;
		protected System.Web.UI.WebControls.Button bt_Update;
		protected System.Web.UI.WebControls.Button bt_Register;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lbItemNum;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lbItemDrawNum;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_OldCost;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Index;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_ReceiptDate;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			ItemSearchControl1.Commodity = true; //상품 바인딩				
			ItemSearchControl1.Products = true; //제품 바인딩
			ItemSearchControl1.Phantom = true;//펜텀 바인딩

			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(!Page.IsPostBack)
			{
				tbQuantity.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this);");
				// onkeyup 이벤트가 발생하면 콤마를 찍고 불량수량을 자동으로 계산하는 자바스크립트 함수 매핑
				tbQuantity.Attributes.Add("OnKeyUp", "OnKeyUp_Currency(this);");

				// focus를 얻으면 자동으로 select()
				tbQuantity.Attributes.Add("OnFocus", "OnFocus_Obj(this);");

				// focus를 잃으면 다시 입력란을 체크
				tbQuantity.Attributes.Add("OnBlur", "OnBlur_Cur(this);");
				tb_Cost.Attributes.Add("OnKeyDown","OnKeyDown_Float(this);");
				tb_Cost.Attributes.Add("OnKeyUp", "OnKeyUp_Currency(this)");
				tb_Cost.Attributes.Add("onblur","OnBlur_Cur(this)");
				tb_Cost.Attributes.Add("onfocus","OnFocus_Obj(this)");
				

				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				conn.Open();


				// 품목바인딩
			


				// 거래처바인딩
				string strSQL = "SELECT CompanyName, PresidentName, BusinessRegistrationNum, CompanyInfoIndex FROM CI_MT where RecodingState = 1 and ReceiveingOrderCompany = 1 order by CompanyName";
				SqlCommand commCom = new SqlCommand(strSQL,conn);
				SqlDataAdapter daCom = new SqlDataAdapter(commCom) ;
				DataSet dsCom = new DataSet() ;
				daCom.Fill(dsCom);

				wcCompany.DataSource = dsCom;
				wcCompany.DataTextField = dsCom.Tables[0].Columns[0].ToString();
				wcCompany.DataValueField = dsCom.Tables[0].Columns[2].ToString();
				wcCompany.DataBind();


				/////////////////////////////////////////
				/// 클레임 현상 코드 삽입
				/////////////////////////////////////////
				string str = "SELECT SmallClassificationCode, SmallClassificationName from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '1000' order by SmallClassificationName";
				SqlCommand comm =  new SqlCommand(str,conn);
				SqlDataAdapter da = new SqlDataAdapter(comm) ;
				DataSet ds = new DataSet() ;
				da.Fill(ds,"Code");

				ddlClaimStatus.DataSource = ds;
				ddlClaimStatus.DataMember = "Code";
				ddlClaimStatus.DataTextField = "SmallClassificationName";
				ddlClaimStatus.DataValueField = "SmallClassificationCode";
				ddlClaimStatus.DataBind();
				ddlClaimStatus.Items.Insert(0, "---선택---");
				ddlClaimStatus.Items[0].Value = "";

				/////////////////////////////////////////
				/// 클레임 원인 코드 삽입
				/////////////////////////////////////////
				string str1 = "SELECT SmallClassificationCode, SmallClassificationName from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '1010' order by SmallClassificationName";
				SqlCommand comm1 =  new SqlCommand(str1,conn);
				SqlDataAdapter da1 = new SqlDataAdapter(comm1) ;
				DataSet ds1 = new DataSet() ;
				da1.Fill(ds1,"Code");

				ddlClaimCause.DataSource = ds1;
				ddlClaimCause.DataMember = "Code";
				ddlClaimCause.DataTextField = "SmallClassificationName";
				ddlClaimCause.DataValueField = "SmallClassificationCode";
				ddlClaimCause.DataBind();
				ddlClaimCause.Items.Insert(0, "---선택---");
				ddlClaimCause.Items[0].Value = "";


				wdcDate.NullDateLabel = DateTime.Now.ToShortDateString();


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
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.bt_Delete.Click += new System.EventHandler(this.bt_Delete_Click);
			this.bt_Update.Click += new System.EventHandler(this.bt_Update_Click);
			this.bt_Register.Click += new System.EventHandler(this.bt_Register_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		/// <summary>
		/// 등록버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Register_Click(object sender, System.EventArgs e)
		{
			if(Validity())
			{
				ArrayList arr = new ArrayList();
				arr.Add(ItemSearchControl1.ItemNum);						//품목번호
				arr.Add(ItemSearchControl1.ItemDrawNum);					//도면번호			
				arr.Add(ItemSearchControl1.ItemName);		//품목명
				arr.Add(wcCompany.DisplayValue.ToString());		//거래처명
				arr.Add(wcCompany.DataValue.ToString());		//사업자등록번호
				arr.Add(wdcDate.Text);							//접수일자
				arr.Add(tbQuantity.Text);						//수량
				arr.Add(tb_Cost.Text);							//금액
				arr.Add(ddlClaimStatus.SelectedItem.Text);		//현상
				arr.Add(ddlClaimStatus.SelectedItem.Value);		//현상코드
				arr.Add(ddlClaimCause.SelectedItem.Text);		//원인
				arr.Add(ddlClaimCause.SelectedItem.Value);		//원인코드
			


				Registration reg = new Registration("ClaimRegistration",Session["ID"].ToString(),"",arr);
				UltraWebGrid1.DataSource = reg.MainTableRegistration();
				UltraWebGrid1.DataBind();
			}
		}

		private bool Validity()
		{
			string str = "";
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlTransaction tr = conn.BeginTransaction();
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			try
			{
				str = "Select count(*) From II_MT Where RecodingState = 1 and ItemNum = @num";
				comm.Parameters.Add("@num",ItemSearchControl1.ItemNum);
				comm.CommandText = str;
				int a = int.Parse(comm.ExecuteScalar().ToString());
				comm.Parameters.Clear();
				if(a == 0)
				{
					throw new Exception("품목을 정확히 입력해주세요!");
				}
				str = "Select count(*) From CI_MT Where RecodingState = 1 and BusinessRegistrationNum = @num";
				comm.Parameters.Add("@num",wcCompany.DataValue.ToString());
				comm.CommandText = str;
				int b = int.Parse(comm.ExecuteScalar().ToString());
				comm.Parameters.Clear();
				if(b == 0)
				{
					throw new Exception("거래처를 정확히 입력해주세요!");
				}
				tr.Commit();
				conn.Close();
				return true;
				
			}
			catch(Exception ee)
			{
				RegisterStartupScript("","<script>alert('"+ee.Message+"');</script>");
				tr.Rollback();
				conn.Close();
				return false;
			}
			
		}

		/// <summary>
		/// 수정버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Update_Click(object sender, System.EventArgs e)
		{
			if(lb_Index.Value == "0")
			{
				RegisterStartupScript("","<script>alert('항목을 선택후 수정하세요!');</script>");
			}
			else
			{
				if(Validity())
				{
					ArrayList arr = new ArrayList();
					arr.Add(ItemSearchControl1.ItemNum);						//품목번호
					arr.Add(ItemSearchControl1.ItemDrawNum);					//도면번호			
					arr.Add(ItemSearchControl1.ItemName);		//품목명
					arr.Add(wcCompany.DisplayValue.ToString());		//거래처명
					arr.Add(wcCompany.DataValue.ToString());		//사업자등록번호
					arr.Add(wdcDate.Text);							//접수일자
					arr.Add(tbQuantity.Text);						//수량
					arr.Add(tb_Cost.Text);							//금액
					arr.Add(ddlClaimStatus.SelectedItem.Text);		//현상
					arr.Add(ddlClaimStatus.SelectedItem.Value);		//현상코드
					arr.Add(ddlClaimCause.SelectedItem.Text);		//원인
					arr.Add(ddlClaimCause.SelectedItem.Value);		//원인코드
					arr.Add(lb_OldCost.Value);							//기존금액
					arr.Add(lb_ReceiptDate.Value);							//기존접수일자


					Registration reg = new Registration("ClaimRegistration",Session["ID"].ToString(),lb_Index.Value,arr);
					UltraWebGrid1.DataSource = reg.MainTableUpdate();
					UltraWebGrid1.DataBind();
				}
			}
		}

		/// <summary>
		/// 삭제버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Delete_Click(object sender, System.EventArgs e)
		{
			if(lb_Index.Value != "0")
			{
				ArrayList arr = new ArrayList();
				arr.Add(ItemSearchControl1.ItemNum);						//품목번호
				arr.Add(ItemSearchControl1.ItemDrawNum);					//도면번호			
				arr.Add(ItemSearchControl1.ItemName);		//품목명
				arr.Add(wcCompany.DisplayValue.ToString());		//거래처명
				arr.Add(wcCompany.DataValue.ToString());		//사업자등록번호
				arr.Add(wdcDate.Text);							//접수일자
				arr.Add(tbQuantity.Text);						//수량
				arr.Add(tb_Cost.Text);							//금액
				arr.Add(ddlClaimStatus.SelectedItem.Text);		//현상
				arr.Add(ddlClaimStatus.SelectedItem.Value);		//현상코드
				arr.Add(ddlClaimCause.SelectedItem.Text);		//원인
				arr.Add(ddlClaimCause.SelectedItem.Value);		//원인코드
				arr.Add(lb_OldCost.Value);							//기존금액
				arr.Add(lb_ReceiptDate.Value);							//기존접수일자

				Registration reg = new Registration("ClaimRegistration",lb_Index.Value,arr);
				UltraWebGrid1.DataSource = reg.MainTableDelete();
				UltraWebGrid1.DataBind();
				
				
				wcCompany.DisplayValue = "";
				wdcDate.Value = DateTime.Now.ToShortDateString();
				tb_Cost.Text = "0";
				tbQuantity.Text = "0";
				ddlClaimCause.SelectedIndex = 0;
				ddlClaimStatus.SelectedIndex = 0;
				lb_Index.Value = "0";
				lb_OldCost.Value = "0";
				lb_ReceiptDate.Value = "";
				lbItemDrawNum.Value  ="";
				lbItemNum.Value = "";


			}
			else
			{
				RegisterStartupScript("","<script>alert('항목을 선택후 삭제하세요!');</script>");
			}
		}

		/// <summary>
		/// 초기화버튼
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button1_Click(object sender, System.EventArgs e)
		{ 
			wcCompany.DataValue = "";
			wcCompany.DisplayValue  = "";

			//wdcDate.NullDateLabel = DateTime.Now.ToShortDateString();
			wdcDate.Value = DateTime.Now.ToShortDateString();

			lbItemNum.Value = "";
			lbItemDrawNum.Value = "";
			lb_OldCost.Value = "0";
			lb_ReceiptDate.Value = "";
			lb_Index.Value = "0";
			ddlClaimCause.SelectedIndex = 0;
			ddlClaimStatus.SelectedIndex = 0;
		}
	}
}
