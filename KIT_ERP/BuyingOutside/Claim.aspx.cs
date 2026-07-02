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
using System.Configuration;
using System.Data.SqlClient;

namespace KIT_ERP.BuyingOutside
{
	/// <summary>
	/// Claim에 대한 요약 설명입니다.
	/// </summary>
	public class Claim : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox tbQuantity;
		protected System.Web.UI.WebControls.TextBox tb_Cost;
		protected System.Web.UI.WebControls.DropDownList ddlClaimStatus;
		protected System.Web.UI.WebControls.DropDownList ddlClaimCause;
		protected System.Web.UI.WebControls.Button bt_Delete;
		protected System.Web.UI.WebControls.Button bt_Update;
		protected System.Web.UI.WebControls.Button bt_Register;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lbItemNum;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lbItemDrawNum;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_OldCost;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcDate;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Index;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdDate;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;
		
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			ItemSearchControl1.Commodity = true; //상품 바인딩				
			ItemSearchControl1.Products = true; //제품 바인딩
			ItemSearchControl1.HalfFinishedProducts = true;//반제품 바인딩
			ItemSearchControl1.RawMaterials = true;//원자재 바인딩
			CSC1.UnitCostDistinction = "구매외주";


			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(!Page.IsPostBack)
			{
				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 영업관리 > 클레임 등록';</script>");
				
				//bt_Register.Attributes.Add("onClick", "return OK('입력한 항목을 등록');");
				//bt_Update.Attributes.Add("onClick", "return OK('선택한 항목을 수정');");
				bt_Delete.Attributes.Add("onClick", "return OK('선택한 항목을 삭제');");
				//Button1.Attributes.Add("onClick", "ResetTextBox();;");


				tbQuantity.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this);");
				tbQuantity.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				tbQuantity.Attributes.Add("OnBlur", "OnBlur_Cur(this);");
				tbQuantity.Attributes.Add("OnKeyUp", "OnKeyUp_Currency(this);");

				tb_Cost.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this);");
				tb_Cost.Attributes.Add("OnKeyUp", "OnKeyUp_Currency(this);");
				tb_Cost.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				tb_Cost.Attributes.Add("OnBlur", "OnBlur_Cur(this);");
				

				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				conn.Open();


				// 품목바인딩
			
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
			this.bt_Delete.Click += new System.EventHandler(this.bt_Delete_Click);
			this.bt_Update.Click += new System.EventHandler(this.bt_Update_Click);
			this.bt_Register.Click += new System.EventHandler(this.bt_Register_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void bt_Register_Click(object sender, System.EventArgs e)
		{
			if(Validity())
			{
				ArrayList arr = new ArrayList();
				arr.Add(ItemSearchControl1.ItemNum);						//품목번호
				arr.Add(ItemSearchControl1.ItemDrawNum);					//도면번호			
				arr.Add(ItemSearchControl1.ItemName);			//품목명
				arr.Add(CSC1.Company);		//거래처명
				arr.Add(CSC1.BusinessRegistrationNum);		//사업자등록번호
				arr.Add(wdcDate.Text);							//접수일자
				arr.Add(decimal.Parse(tbQuantity.Text));						//수량
				arr.Add(decimal.Parse(tb_Cost.Text));							//금액
				arr.Add(ddlClaimStatus.SelectedItem.Text);		//현상
				arr.Add(ddlClaimStatus.SelectedItem.Value);		//현상코드
				arr.Add(ddlClaimCause.SelectedItem.Text);		//원인
				arr.Add(ddlClaimCause.SelectedItem.Value);		//원인코드
			


				Registration reg = new Registration("Claim",Session["ID"].ToString(),"",arr);
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
				comm.Parameters.Add("@num",CSC1.BusinessRegistrationNum);
				comm.CommandText = str;
				int b = int.Parse(comm.ExecuteScalar().ToString());
				comm.Parameters.Clear();
				if(b == 0)
				{
					throw new Exception("거래처를 정확히 입력해주세요!");
				}
				

				if(ddlClaimStatus.SelectedIndex == 0)
					throw new Exception("불량현상을 입력해주세요!");
				if(ddlClaimCause.SelectedIndex == 0)
					throw new Exception("불량원인을 입력해주세요!");
				else if(!Compare(CSC1.Company, CSC1.BusinessRegistrationNum))
					throw new Exception("사업자번호가 거래처명과 다릅니다!");

				tr.Commit();
				conn.Close();
				return true;
				
			}
			catch(Exception ee)
			{
				tr.Rollback();
				RegisterStartupScript("","<script>alert('"+ee.Message+"');</script>");
				conn.Close();
				return false;
			}
		}

		private bool Compare(string Company, string BusinessRegistrationNum)
		{
			SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			SqlCommand com = new SqlCommand();
			com.Connection = con;
			con.Open();
			string str = "Select Count(BusinessRegistrationNum) From CI_MT where RecodingState =1 and CompanyName = @Com and BusinessRegistrationNum = @Num";
			com.CommandText = str;
			com.Parameters.Add("@Com",Company);
			com.Parameters.Add("@Num",BusinessRegistrationNum);
			int a = int.Parse(com.ExecuteScalar().ToString());
			com.Parameters.Clear();
			con.Close();

			if(a == 0)
				return false;
			else
				return true;
		}

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
					arr.Add(CSC1.Company);		//거래처명
					arr.Add(CSC1.BusinessRegistrationNum);		//사업자등록번호
					arr.Add(wdcDate.Text);							//접수일자
					arr.Add(decimal.Parse(tbQuantity.Text));						//수량
					arr.Add(decimal.Parse(tb_Cost.Text));							//금액
					arr.Add(ddlClaimStatus.SelectedItem.Text);		//현상
					arr.Add(ddlClaimStatus.SelectedItem.Value);		//현상코드
					arr.Add(ddlClaimCause.SelectedItem.Text);		//원인
					arr.Add(ddlClaimCause.SelectedItem.Value);		//원인코드
					arr.Add(lb_OldCost.Value);							//기존금액
					arr.Add(hdDate.Value);

					Registration reg = new Registration("Claim",Session["ID"].ToString(),lb_Index.Value,arr);
					//Registration reg = new Registration("Claim","admin",lb_Index.Value,arr);
					UltraWebGrid1.DataSource = reg.MainTableUpdate();
					UltraWebGrid1.DataBind();
				}
			}
		}

		private void bt_Delete_Click(object sender, System.EventArgs e)
		{
			if(lb_Index.Value != "0")
			{
				ArrayList arr = new ArrayList();
				arr.Add(ItemSearchControl1.ItemNum);						//품목번호
				arr.Add(ItemSearchControl1.ItemDrawNum);					//도면번호			
				arr.Add(ItemSearchControl1.ItemName);		//품목명
				arr.Add(CSC1.Company);		//거래처명
				arr.Add(CSC1.BusinessRegistrationNum);		//사업자등록번호
				arr.Add(wdcDate.Text);							//접수일자
				arr.Add(decimal.Parse(tbQuantity.Text));						//수량
				arr.Add(decimal.Parse(tb_Cost.Text));							//금액
				arr.Add(ddlClaimStatus.SelectedItem.Text);		//현상
				arr.Add(ddlClaimStatus.SelectedItem.Value);		//현상코드
				arr.Add(ddlClaimCause.SelectedItem.Text);		//원인
				arr.Add(ddlClaimCause.SelectedItem.Value);		//원인코드
				arr.Add(lb_OldCost.Value);							//기존금액
				arr.Add(hdDate.Value);
				

				Registration reg = new Registration("Claim",lb_Index.Value,arr);
				UltraWebGrid1.DataSource = reg.MainTableDelete();
				UltraWebGrid1.DataBind();
				
				
				CSC1.Company="";
				CSC1.BusinessRegistrationNum = "";
				ItemSearchControl1.ItemDrawNum = "";
				ItemSearchControl1.ItemName = "";
				ItemSearchControl1.ItemNum = "";
				tb_Cost.Text = "0";
				tbQuantity.Text = "0";
				ddlClaimCause.SelectedIndex = 0;
				ddlClaimStatus.SelectedIndex = 0;
				lb_Index.Value = "0";
				lb_OldCost.Value = "0";
				lbItemDrawNum.Value  ="";
				lbItemNum.Value = "";
			}
			else
			{
				RegisterStartupScript("","<script>alert('항목을 선택후 삭제하세요!');</script>");
			}
		}

		
	}
}
