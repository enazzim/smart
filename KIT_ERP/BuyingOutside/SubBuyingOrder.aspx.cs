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
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Win32;

namespace KIT_ERP.BuyingOutside
{
	/// <summary>
	/// SubBuyingOrder에 대한 요약 설명입니다.
	/// </summary>
	public class SubBuyingOrder : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox txtCost;
		protected System.Web.UI.WebControls.TextBox txtQuantity;
		protected System.Web.UI.WebControls.TextBox txtTotalCost;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wcDeliveryDate;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Button btnReg;
		protected System.Web.UI.WebControls.Button btnDelete;
		protected System.Web.UI.WebControls.Button btnUpdate;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		protected System.Web.UI.WebControls.LinkButton Linkbutton2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdindex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden rowIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdItemNum;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lblItemDrawNum;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdBusinessRegistrationNum;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdHistoryIndex;
		private string param;
		private string param1;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			ItemSearchControl1.SubMaterials = true; //원자재 바인딩
			ItemSearchControl1.Expendable = true;// 소모품
			ItemSearchControl1.IsDoPost = true;
			
			CSC1.UnitCostDistinction = "구매단가";
			CSC1.IsDoPost1= true;


			if(!Page.IsPostBack)
			{
				param = this.hdBusinessRegistrationNum.Value;	//거래처 웹콤보를 선택했는지 확인하는 변수
				param1 = this.hdItemNum.Value;					//품목 웹콤보를 선택했는지 확인하는 변수
				
				// 추가버튼에 대하여 onclick 속성 추가
				//btnOrder.Attributes.Add("onclick", "return Add('선택한 품목을 추가하시겠습니까?');");
			
				// 수정버튼에 대하여 onclick속성을 추가 시킴
				//btnEdit.Attributes.Add("onclick","return(Confirm('선택한 품목들의 대하여 수정를 하시겠습니까?'))");
				// 삭제버튼에 대하여 onclick속성을 추가 시킴
				btnDelete.Attributes.Add("onclick","return(Confirm('선택한 품목들의 대하여 삭제를 하시겠습니까?'))");
						
				
				txtQuantity.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this);");
				// onkeyup 이벤트가 발생하면 수량입력과동시에 금액을 계산하는 자바스크립트 함수 매핑
				txtQuantity.Attributes.Add("OnKeyUp", "Process(); OnKeyUp_Currency(this);");
				// focus를 얻으면 자동으로 select()
				txtQuantity.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				// focus를 잃으면 다시 입력란을 체크
				txtQuantity.Attributes.Add("OnBlur", "OnBlur_Cur(this);");
				//tb_DeliveryRequestQuantity1.Attributes.Add("OnChange", "return checkNumber(this)");

				txtCost.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this);");
				txtCost.Attributes.Add("OnKeyUp", "Process(); OnKeyUp_Currency(this);");
				txtCost.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				txtCost.Attributes.Add("OnBlur", "OnBlur_Cur(this);");
				
								
				Session["param"] = this.CSC1.BusinessRegistrationNum;		//거래처를 선택했는지 확인하는 변수
				Session["param1"] = this.hdItemNum.Value;					//품목을 선택했는지 확인하는 변수
			}
			else
			{
				if(Session["param"].ToString().Length != 0)
				{
					ItemSearchControl1.BusinessRegistrationNum = CSC1.BusinessRegistrationNum;
					ItemSearchControl1.UnitCostDistinction = "구매단가";
				}
				
				if(Session["param1"].ToString().Length != 0)
				{
					CSC1.ItemNum = hdItemNum.Value;
				}
				param = Session["param"].ToString();	//거래처를 선택했는지 확인하는 변수
				param1 = Session["param1"].ToString();					//품목을 선택했는지 확인하는 변수
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
			this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnReg.Click += new System.EventHandler(this.btnReg_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnReg_Click(object sender, System.EventArgs e)
		{
			if(wcDeliveryDate.Text.Trim() == "")
			{
				Response.Write("<script>alert('납기일을 입력하세요!');</script>");
			}
			else if(txtQuantity.Text.Trim() == "0" || txtQuantity.Text.Trim() =="")
			{
				Response.Write("<script>alert('발주량을 입력하세요!');</script>");
			}
			else if(txtCost.Text.Trim() == "0" || txtCost.Text.Trim() =="")
			{
				Response.Write("<script>alert('단가를 입력하세요!');</script>");
			}
			else if(!Compare(CSC1.Company, CSC1.BusinessRegistrationNum))
			{
				RegisterStartupScript("","<script> alert('사업자번호가 거래처명과 다릅니다!');</script>");
			}
			else
			{

				ArrayList OrderArrayList = new ArrayList();

				OrderArrayList.Add(ItemSearchControl1.ItemNum);			//품목번호
				OrderArrayList.Add(ItemSearchControl1.ItemDrawNum);		//도면번호
				OrderArrayList.Add(ItemSearchControl1.ItemName);		//품목명 
				OrderArrayList.Add(CSC1.Company);			//거래처명 
				OrderArrayList.Add(CSC1.BusinessRegistrationNum);				//사업자등록번호 
				OrderArrayList.Add(txtQuantity.Text.ToString());	//총발주량			
				OrderArrayList.Add(txtCost.Text);				//적용단가
				OrderArrayList.Add(decimal.Parse(txtTotalCost.Text));//총금액
				OrderArrayList.Add(txtQuantity.Text.ToString());	//잔량
				OrderArrayList.Add(Session["UserName"].ToString());								//등록자
				OrderArrayList.Add(Session["ID"].ToString());								//등록자ID
				OrderArrayList.Add(DateTime.Parse(wcDeliveryDate.Text.Trim()).ToShortDateString());			//납기일
				OrderArrayList.Add(DateTime.Now.ToShortDateString());	//등록일	
		

		
		
				//부자재발주 버튼을 클릭
				KIT_ERP.Registration addregisterration = new Registration("SubBuyingOrder",Session["ID"].ToString(),hdindex.Value,OrderArrayList);
				
				this.UltraWebGrid1.DataSource = addregisterration.MainTableRegistration();
				this.UltraWebGrid1.DataBind();
			}

			Session["param"] = this.CSC1.BusinessRegistrationNum;	//거래처 웹콤보를 선택했는지 확인하는 변수
			Session["param1"] = this.hdItemNum.Value;					//품목 웹콤보를 선택했는지 확인하는 변수
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

		private void LinkButton1_Click(object sender, System.EventArgs e)
		{
			if(ItemSearchControl1.ItemNum != null)
			{
				Session["param1"] = ItemSearchControl1.ItemNum;			//품목을 선택했는지 확인하는 변수
				CSC1.ItemNum = ItemSearchControl1.ItemNum;

				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				conn.Open();
				SqlTransaction tr = conn.BeginTransaction();
				try
				{
					
					AppCost(conn,tr);

					tr.Commit();
				}
				catch(Exception ee)
				{
					Response.Write("<script>alert('"+ee.Message + "');</script>");
					tr.Rollback();
				}
				finally
				{
					conn.Close();
				}
			}
			
			ItemSearchControl1.BusinessRegistrationNum = CSC1.BusinessRegistrationNum;
			ItemSearchControl1.UnitCostDistinction = "구매단가";
			hdItemNum.Value = ItemSearchControl1.ItemNum;
		}

		private void Linkbutton2_Click(object sender, System.EventArgs e)
		{
			Session["param"] = CSC1.BusinessRegistrationNum;
			
			ItemSearchControl1.BusinessRegistrationNum = CSC1.BusinessRegistrationNum;
			ItemSearchControl1.UnitCostDistinction = "구매단가";
			CSC1.ItemNum = hdItemNum.Value;

			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlTransaction tr = conn.BeginTransaction();
			try
			{
				AppCost(conn,tr,CSC1.BusinessRegistrationNum);
				tr.Commit();
			}
			catch(Exception ee)
			{
				Response.Write("<script>alert('"+ee.Message + "');</script>");
				tr.Rollback();
			}
			finally
			{
				conn.Close();
			}
		}

		//품목을 선택해서 해당 거래처에 해당하는 적용단가를 가지고 오는 함수
		public void AppCost(SqlConnection con, SqlTransaction trans)
		{
			//품목을 선택했을때 품목의 적용단가를 가져오는 구문
			decimal ApplyUnitCost=0;				
			
			//단가정보테이블에서 내가 선택한 품목과 거래처에 해당하는 품목의 단가구하는 쿼리
			string sqlApply = @"SELECT UCI_MT.StandardUnitCost FROM UCI_MT where UCI_MT.RecodingState =1 and UCI_MT.ItemNum = @ItemNum AND UCI_MT.BusinessRegistrationNum like @Num and UnitCostDistinction = '구매단가'";

			SqlCommand Applycom = new SqlCommand();
			Applycom.Connection = con;
			Applycom.Transaction = trans;
			Applycom.CommandText = sqlApply;
			Applycom.Parameters.Add("@ItemNum",ItemSearchControl1.ItemNum);
			Applycom.Parameters.Add("@Num",CSC1.BusinessRegistrationNum);
			SqlDataReader dr = Applycom.ExecuteReader();
			while(dr.Read())
			{
				ApplyUnitCost = Decimal.Parse(dr["StandardUnitCost"].ToString());
			}
			dr.Close();
			Applycom.Parameters.Clear();

			
			if(CSC1.BusinessRegistrationNum.Trim() != "" && ItemSearchControl1.ItemNum.Trim() != "" && ApplyUnitCost == 0)
				throw new Exception("품목 "+ItemSearchControl1.ItemNum+"의 단가정보가 존재하지 않습니다!");
			else
			{
				txtCost.Text = Convert.ToString(ApplyUnitCost);
			}
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			//선택된 로우 인덱스를 저장하는 변수
			int rowcount = int.Parse(rowIndex.Value);

			//진행상태가 대기인지 아닌지 판단하고 대기가 아니라면 삭제를 할수 없다.
			if(UltraWebGrid1.Rows[rowcount].Cells.FromKey("ProgressCondition").Value.ToString() != "대기")
			{
				Response.Write("<script language='javascript'>");
				Response.Write("alert('삭제 할 수 없는 품목 입니다.');");
				Response.Write("</script>");
			}
			else if(hdindex.Value.Trim() == "")
			{
				Response.Write("<script language='javascript'>");
				Response.Write("alert('삭제 할 품목을 선택하세요.');");
				Response.Write("</script>");
			}
			else
			{
				//진행상태가 대기인것들은 삭제
				KIT_ERP.Registration delete = new KIT_ERP.Registration("SubBuyingOrder",hdindex.Value);
				this.UltraWebGrid1.DataSource = delete.MainTableDelete();
				this.UltraWebGrid1.DataBind();

				//삭제되고 난뒤 초기화
				ItemInit();
			}	
		}

		private void ItemInit()
		{
			//검색조건 초기화
			
			CSC1.BusinessRegistrationNum = "";
			CSC1.Company="";
			ItemSearchControl1.ItemNum = "";
			ItemSearchControl1.ItemDrawNum = "";
			ItemSearchControl1.ItemName = "";
			txtQuantity.Text = "0";
			txtCost.Text = "0";
			wcDeliveryDate.Value = null;
			hdBusinessRegistrationNum.Value ="";
			hdItemNum.Value = "0";
			txtTotalCost.Text = "0";
			Session["param"] = "";
			Session["param1"] = "";
			ItemSearchControl1.BusinessRegistrationNum = CSC1.BusinessRegistrationNum;
			ItemSearchControl1.UnitCostDistinction = "구매단가";
			CSC1.ItemNum = hdItemNum.Value;
			hdindex.Value = "";
		}

		private void btnUpdate_Click(object sender, System.EventArgs e)
		{
			ArrayList OrderArrayList = new ArrayList();

			OrderArrayList.Add(ItemSearchControl1.ItemNum);			//품목번호 
			OrderArrayList.Add(ItemSearchControl1.ItemDrawNum);		//도면번호 
			OrderArrayList.Add(ItemSearchControl1.ItemName);		//품목명 
			OrderArrayList.Add(CSC1.Company);						//거래처명 
			OrderArrayList.Add(CSC1.BusinessRegistrationNum);		//사업자등록번호 
			OrderArrayList.Add(txtQuantity.Text.ToString());	//수량
			OrderArrayList.Add(wcDeliveryDate.Text.Trim());					//납기일
			OrderArrayList.Add(txtCost.Text);				//적용단가
			OrderArrayList.Add(decimal.Parse(txtCost.Text)*decimal.Parse(txtQuantity.Text.ToString()));//총금액
			OrderArrayList.Add(txtQuantity.Text.ToString());	//잔량
			OrderArrayList.Add(Session["UserName"].ToString());								//등록자
			OrderArrayList.Add(Session["ID"].ToString());								//등록자ID
			OrderArrayList.Add(DateTime.Now.ToShortDateString());	//등록일
			OrderArrayList.Add(this.hdindex.Value);					//원장번호
			

			//구매발주추가페이지 수정 버튼을 클릭
			KIT_ERP.Registration Editregisterration = new Registration("SubBuyingOrder",Session["ID"].ToString(),this.hdindex.Value,OrderArrayList);
								
			this.UltraWebGrid1.DataSource = Editregisterration.MainTableUpdate();
			this.UltraWebGrid1.DataBind();	

			Session["param"] = this.CSC1.BusinessRegistrationNum;	//거래처 웹콤보를 선택했는지 확인하는 변수
			Session["param1"] = this.hdItemNum.Value;					//품목 웹콤보를 선택했는지 확인하는 변수
		}

		//품목을 선택해서 해당 거래처에 해당하는 적용단가를 가지고 오는 함수
		public void AppCost(SqlConnection con, SqlTransaction trans, string BusinessRegistrationNum)
		{
			//품목을 선택했을때 품목의 적용단가를 가져오는 구문
			decimal ApplyUnitCost=0;				
			
			//단가정보테이블에서 내가 선택한 품목과 거래처에 해당하는 품목의 단가구하는 쿼리
			string sqlApply = @"SELECT UCI_MT.StandardUnitCost FROM UCI_MT where UCI_MT.RecodingState =1 and UCI_MT.ItemNum = @ItemNum AND UCI_MT.BusinessRegistrationNum like @Num and UnitCostDistinction = '구매단가'";

			SqlCommand Applycom = new SqlCommand();
			Applycom.Connection = con;
			Applycom.Transaction = trans;
			Applycom.CommandText = sqlApply;
			Applycom.Parameters.Add("@ItemNum",ItemSearchControl1.ItemNum);
			Applycom.Parameters.Add("@Num",CSC1.BusinessRegistrationNum);
			SqlDataReader dr = Applycom.ExecuteReader();
			while(dr.Read())
			{
				ApplyUnitCost = Decimal.Parse(dr["StandardUnitCost"].ToString());
			}
			dr.Close();

			
			

			if(CSC1.BusinessRegistrationNum.Trim() != "" && ItemSearchControl1.ItemNum.Trim() != "" && ApplyUnitCost == 0)
				throw new Exception("품목 "+ItemSearchControl1.ItemNum+"의 단가정보가 존재하지 않습니다!");
			else
			{
				txtCost.Text = ApplyUnitCost.ToString();
			}

			
			
		}
	}
}
