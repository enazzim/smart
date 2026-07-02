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
	/// EtcClaim에 대한 요약 설명입니다.
	/// </summary>
	public class EtcClaim : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button bt_Delete;
		protected System.Web.UI.WebControls.Button bt_Update;
		protected System.Web.UI.WebControls.Button bt_Register;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcDate;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.TextBox tb_Cost;
		protected System.Web.UI.WebControls.TextBox tbRegion;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdDate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_OldCost;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Index;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;
		
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			CSC1.UnitCostDistinction = "구매외주";

			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(!Page.IsPostBack)
			{
				bt_Delete.Attributes.Add("onClick", "return OK('선택한 항목을 삭제');");
				
				
				tb_Cost.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this);");
				tb_Cost.Attributes.Add("OnKeyUp", "OnKeyUp_Currency(this);");
				tb_Cost.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				tb_Cost.Attributes.Add("OnBlur", "OnBlur_Cur(this);");
				

				wdcDate.NullDateLabel = DateTime.Now.ToShortDateString();

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
			if(tbRegion.Text.Trim() == "")
			{
				RegisterStartupScript("","<script>alert('공제사유를 입력해 주세요!');</script>");
			}
			else if(CSC1.BusinessRegistrationNum.Trim() == "")
				RegisterStartupScript("","<script>alert('거래처를 정확히 선택해 주세요!');</script>");
			else if(!Compare(CSC1.Company, CSC1.BusinessRegistrationNum))
			{
				RegisterStartupScript("","<script> alert('사업자번호가 거래처명과 다릅니다!');</script>");
			}
			else if(!MonthClosing())
			{
				RegisterStartupScript("","<script>alert('월마감이 되어 입고가 불가능 합니다!');</script>");
			}
			else
			{
				ArrayList arr = new ArrayList();
				arr.Add(CSC1.Company);		//거래처명
				arr.Add(CSC1.BusinessRegistrationNum);		//사업자등록번호
				arr.Add(wdcDate.Text);							//접수일자
				arr.Add(tbRegion.Text);						//공제사유
				
				if(tb_Cost.Text.Trim() == "")
					arr.Add(0);							//금액
				else
					arr.Add(decimal.Parse(tb_Cost.Text));							//금액
				


				Registration reg = new Registration("EtcClaim",Session["ID"].ToString(),"",arr);
				UltraWebGrid1.DataSource = reg.MainTableRegistration();
				UltraWebGrid1.DataBind();
			}
		}

		

		private void bt_Update_Click(object sender, System.EventArgs e)
		{
			if(lb_Index.Value == "0")
			{
				RegisterStartupScript("","<script>alert('항목을 선택후 수정하세요!');</script>");
			}
			else
			{
					ArrayList arr = new ArrayList();
					arr.Add(CSC1.Company);		//거래처명
					arr.Add(CSC1.BusinessRegistrationNum);		//사업자등록번호
					arr.Add(wdcDate.Text);							//접수일자
					arr.Add(tbRegion.Text);						//공제사유
					arr.Add(decimal.Parse(tb_Cost.Text));							//금액
					arr.Add(lb_OldCost.Value);							//기존금액
					arr.Add(hdDate.Value);

					Registration reg = new Registration("EtcClaim",Session["ID"].ToString(),lb_Index.Value,arr);
					
					UltraWebGrid1.DataSource = reg.MainTableUpdate();
					UltraWebGrid1.DataBind();
			}
		}

		private void bt_Delete_Click(object sender, System.EventArgs e)
		{
			if(lb_Index.Value != "0")
			{
				ArrayList arr = new ArrayList();
				arr.Add(CSC1.Company);		//거래처명
				arr.Add(CSC1.BusinessRegistrationNum);		//사업자등록번호
				arr.Add(wdcDate.Text);							//접수일자
				arr.Add(tbRegion.Text);						//공제사유
				arr.Add(decimal.Parse(tb_Cost.Text));							//금액
				arr.Add(lb_OldCost.Value);							//기존금액
				arr.Add(hdDate.Value);
				

				Registration reg = new Registration("EtcClaim",lb_Index.Value,arr);
				UltraWebGrid1.DataSource = reg.MainTableDelete();
				UltraWebGrid1.DataBind();
				
				
				CSC1.Company="";
				CSC1.BusinessRegistrationNum = "";
				
				tb_Cost.Text = "0";
				tbRegion.Text = "";
				
				lb_Index.Value = "0";
				lb_OldCost.Value = "0";
				
			}
			else
			{
				RegisterStartupScript("","<script>alert('항목을 선택후 삭제하세요!');</script>");
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

		/// <summary>
		/// 월마감 여부를 확인하는 함수
		/// </summary>
		/// <returns></returns>
		private bool MonthClosing()
		{
			int year = 0;
			int month = 0;
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select ClosingYear, ClosingMonth From MCI_MT Where AffairDistinction = @Distinction";
			SqlCommand comm =  new SqlCommand(str,conn);
			comm.Parameters.Add("@Distinction",SqlDbType.VarChar).Value = "구매";
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				year = int.Parse(dr[0].ToString());
				month = int.Parse(dr[1].ToString());
			}
			conn.Close();

			if(year < DateTime.Parse(wdcDate.Text).Year)
				return true;
			else if(year == DateTime.Parse(wdcDate.Text).Year)
			{
				if(month < DateTime.Parse(wdcDate.Text).Month)
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
