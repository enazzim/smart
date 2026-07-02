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
using Microsoft.Win32;




namespace KIT_ERP.BuyingOutside
{
	/// <summary>
	/// BuyingOrderAdd에 대한 요약 설명입니다.
	/// </summary>
	public class BuyingOrderAdd : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected System.Web.UI.WebControls.Button btnInit;
		protected System.Web.UI.WebControls.Button btnEdit;
		protected System.Web.UI.WebControls.Button btnDelete;
		protected System.Web.UI.WebControls.Label Label1;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwgBuyingOrderAdd;
		protected System.Web.UI.WebControls.TextBox txtFirstQuantity;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser txtFirstDate;
		protected System.Web.UI.WebControls.TextBox txtApplyUnitCost;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lblItemDrawNum;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdItemNum;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdBusinessRegistrationNum;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdindex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden rowIndex;
		protected System.Web.UI.WebControls.Button btnOrder;
		private string param;
		private string param1;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		
		protected System.Web.UI.WebControls.LinkButton Linkbutton2;
		protected System.Web.UI.WebControls.TextBox txtUnit;
		protected System.Web.UI.WebControls.TextBox txtOrderIntervalQuantity;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		
	
	
		private void Page_Load(object sender, System.EventArgs e)
		{	
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			ItemSearchControl1.RawMaterials = true; //원자재 바인딩
			ItemSearchControl1.Commodity = true;	//상품 바인딩
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
						
				
				txtFirstQuantity.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this);");
				// focus를 얻으면 자동으로 select()
				txtFirstQuantity.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				// focus를 잃으면 다시 입력란을 체크
				txtFirstQuantity.Attributes.Add("OnBlur", "OnBlur_Float(this);");

				
				btnInit.Attributes.Add("onClick","ResettxtBox()");
				btnDelete.Attributes.Add("onClick","ResettxtBox()");

				
				Session["param"] = this.CSC1.BusinessRegistrationNum;	//거래처를 선택했는지 확인하는 변수
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
			this.LinkButton1.Click += new System.EventHandler(this.LinkButton1_Click);
			this.Linkbutton2.Click += new System.EventHandler(this.Linkbutton2_Click);
			this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnOrder.Click += new System.EventHandler(this.btnOrder_Click);
			this.Load += new System.EventHandler(this.Page_Load);
			this.PreRender += new System.EventHandler(this.BuyingOrderAdd_PreRender);

		}
		#endregion
		
		/// <summary>
		/// 추가발주를 하게될 경우!!
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOrder_Click(object sender, System.EventArgs e)
		{
			
			ArrayList OrderArrayList = new ArrayList();

			OrderArrayList.Add(ItemSearchControl1.ItemNum);			//품목번호
			OrderArrayList.Add(ItemSearchControl1.ItemDrawNum);		//도면번호
			OrderArrayList.Add(ItemSearchControl1.ItemName);		//품목명 
			OrderArrayList.Add(CSC1.Company);			//거래처명 
			OrderArrayList.Add(CSC1.BusinessRegistrationNum);				//사업자등록번호 
			OrderArrayList.Add(txtFirstQuantity.Text.ToString());	//1차납품요구량
			OrderArrayList.Add(txtFirstDate.Text);					//1차납품 요구일
			OrderArrayList.Add(0);	//2차납품요구량
			OrderArrayList.Add("");					//2차납품요구일
			OrderArrayList.Add(0);	//3차납품요구량
			OrderArrayList.Add("");					//3차납품요구일
			OrderArrayList.Add(0);	//4차납품요구량
			OrderArrayList.Add("");					//4차납품요구일
			OrderArrayList.Add(0);	//5차납품요구량
			OrderArrayList.Add("");					//5차납품요구일
			OrderArrayList.Add(txtFirstQuantity.Text.ToString());	//총발주량			
			OrderArrayList.Add(txtApplyUnitCost.Text);				//적용단가
			OrderArrayList.Add(decimal.Parse(txtApplyUnitCost.Text)*decimal.Parse(txtFirstQuantity.Text.ToString()));//총금액
			OrderArrayList.Add("100");								//발주비율(추가발주는 무조건 수량이 다나간다.
			OrderArrayList.Add(txtFirstQuantity.Text.ToString());	//잔량
			OrderArrayList.Add(Session["UserName"].ToString());								//등록자
			OrderArrayList.Add(Session["ID"].ToString());								//등록자ID
			OrderArrayList.Add(DateTime.Now.ToShortDateString());	//등록일	
		

		
		
			//구매발주추가 버튼을 클릭
			KIT_ERP.Registration addregisterration = new Registration("BuyingOrderAdd",Session["ID"].ToString(),hdindex.Value,OrderArrayList);
				
			this.uwgBuyingOrderAdd.DataSource = addregisterration.MainTableRegistration();
			this.uwgBuyingOrderAdd.DataBind();


			Session["param"] = this.CSC1.BusinessRegistrationNum;	//거래처 웹콤보를 선택했는지 확인하는 변수
			Session["param1"] = this.hdItemNum.Value;					//품목 웹콤보를 선택했는지 확인하는 변수

							
		}

	
		


		private void btnInit_Click(object sender, System.EventArgs e)
		{
			ItemInit();		
		}

		private void ItemInit()
		{
			//검색조건 초기화
			
			CSC1.BusinessRegistrationNum = "";
			CSC1.Company="";
			txtFirstQuantity.Text = "0";
			txtApplyUnitCost.Text = "0";
			txtFirstDate.Value = null;
			hdBusinessRegistrationNum.Value ="";
			hdItemNum.Value = "";
			txtUnit.Text = "";
			txtOrderIntervalQuantity.Text = "0";
			Session["param"] = "";
			Session["param1"] = "";
			ItemSearchControl1.BusinessRegistrationNum = CSC1.BusinessRegistrationNum;
			ItemSearchControl1.UnitCostDistinction = "구매단가";
			CSC1.ItemNum = hdItemNum.Value;
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			//선택된 로우 인덱스를 저장하는 변수
			int rowcount = int.Parse(rowIndex.Value);

			////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			//삭제를 하려고 하는 품목이 진행상태가 대기이면서 검사품인경우 품질검사원장에 발주번호로 검색해서 있는지 확인한다.//
			//만약 품목이 있다면 삭제를 할 수 가 없다.                                                                                                          //
			////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);				
			string str = "select count(*) from qi_ht where HistoryIndex2 = @hdindex";
			SqlCommand cmd = new SqlCommand(str,conn);
			conn.Open();
			cmd.Parameters.Add("@hdindex",SqlDbType.Int).Value = int.Parse(uwgBuyingOrderAdd.Rows[rowcount].Cells.FromKey("BuyingOrderHistoryIndex").Value.ToString());
			int num = int.Parse(cmd.ExecuteScalar().ToString());
			conn.Close();

			
			//진행상태가 대기인지 아닌지 판단하고 대기가 아니라면 삭제를 할수 없다.
			if(uwgBuyingOrderAdd.Rows[rowcount].Cells.FromKey("ProgressCondition").Value.ToString() != "대기")
			{
				Response.Write("<script language='javascript'>");
				Response.Write("alert('삭제 할 수 없는 품목 입니다.');");
				Response.Write("</script>");
			}
			else if(num != 0)
			{
				Response.Write("<script language='javascript'>");
				Response.Write("alert('삭제 할 수 없는 품목 입니다.');");
				Response.Write("</script>");
			}
			else
			{
				//진행상태가 대기인것들은 삭제
				KIT_ERP.Registration delete = new KIT_ERP.Registration("BuyingOrderAdd",uwgBuyingOrderAdd.Rows[rowcount].Cells.FromKey("BuyingOrderHistoryIndex").Text);
				this.uwgBuyingOrderAdd.DataSource = delete.MainTableDelete();
				this.uwgBuyingOrderAdd.DataBind();

				//삭제되고 난뒤 초기화
				ItemInit();
			}		
				
		}

		/// <summary>
		/// 수정버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnEdit_Click(object sender, System.EventArgs e)
		{
			ArrayList OrderArrayList = new ArrayList();

			OrderArrayList.Add(ItemSearchControl1.ItemNum);			//품목번호 
			OrderArrayList.Add(ItemSearchControl1.ItemDrawNum);		//도면번호 
			OrderArrayList.Add(ItemSearchControl1.ItemName);		//품목명 
			OrderArrayList.Add(CSC1.Company);						//거래처명 
			OrderArrayList.Add(CSC1.BusinessRegistrationNum);		//사업자등록번호 
			OrderArrayList.Add(txtFirstQuantity.Text.ToString());	//1차납품요구량
			OrderArrayList.Add(txtFirstDate.Text);					//1차납품 요구일
			OrderArrayList.Add(0);	//2차납품요구량
			OrderArrayList.Add("");					//2차납품요구일
			OrderArrayList.Add(0);	//3차납품요구량
			OrderArrayList.Add("");					//3차납품요구일
			OrderArrayList.Add(0);	//4차납품요구량
			OrderArrayList.Add("");					//4차납품요구일
			OrderArrayList.Add(0);	//5차납품요구량
			OrderArrayList.Add("");					//5차납품요구일
			OrderArrayList.Add(txtFirstQuantity.Text.ToString());	//총발주량			
			OrderArrayList.Add(txtApplyUnitCost.Text);				//적용단가
			OrderArrayList.Add(decimal.Parse(txtApplyUnitCost.Text)*decimal.Parse(txtFirstQuantity.Text.ToString()));//총금액
			OrderArrayList.Add("100");								//발주비율(추가발주는 무조건 수량이 다나간다.
			OrderArrayList.Add(txtFirstQuantity.Text.ToString());	//잔량
			OrderArrayList.Add(Session["UserName"].ToString());								//등록자
			OrderArrayList.Add(Session["ID"].ToString());								//등록자ID
			OrderArrayList.Add(DateTime.Now.ToShortDateString());	//등록일
			OrderArrayList.Add(this.hdindex.Value);					//원장번호
			

			//구매발주추가페이지 수정 버튼을 클릭
			KIT_ERP.Registration Editregisterration = new Registration("BuyingOrderAdd",Session["ID"].ToString(),this.hdindex.Value,OrderArrayList);
								
			this.uwgBuyingOrderAdd.DataSource = Editregisterration.MainTableUpdate();
			this.uwgBuyingOrderAdd.DataBind();	

			Session["param"] = this.CSC1.BusinessRegistrationNum;	//거래처 웹콤보를 선택했는지 확인하는 변수
			Session["param1"] = this.hdItemNum.Value;					//품목 웹콤보를 선택했는지 확인하는 변수

		
		}

	
		/// <summary>
		/// 품목을 선택했을시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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
			
			//선택한 품목과 거래처에 해당하는 적용단가를 가지고오는 함추호출
			
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

			
			sqlApply = @"Select SmallClassificationName, OrderIntervalQuantity From II_MT inner join PUC_MT on Unit = SmallClassificationCode Where II_MT.RecodingState = 1 and ItemNum = @ItemNum";
			Applycom.CommandText = sqlApply;
			Applycom.Parameters.Add("@ItemNum",ItemSearchControl1.ItemNum);
			SqlDataReader reader = Applycom.ExecuteReader();
			while(reader.Read())
			{
				txtUnit.Text = reader["SmallClassificationName"].ToString();
				txtOrderIntervalQuantity.Text = reader["OrderIntervalQuantity"].ToString();
			}
			reader.Close();

			if(CSC1.BusinessRegistrationNum.Trim() != "" && ItemSearchControl1.ItemNum.Trim() != "" && ApplyUnitCost == 0)
				throw new Exception("품목 "+ItemSearchControl1.ItemNum+"의 단가정보가 존재하지 않습니다!");
			else
			{
				txtApplyUnitCost.Text = Convert.ToString(ApplyUnitCost);
			}
		}

		/// <summary>
		/// 거래처를 선택했을때
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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

			//선택한 품목과 거래처에 해당하는 적용단가를 가지고오는 함추호출
           	
		}

		//품목을 선택해서 해당 거래처에 해당하는 적용단가를 가지고 오는 함수
		public void AppCost(SqlConnection con, SqlTransaction trans, string BusinessRegistrationNum)
		{
			//품목을 선택했을때 품목의 적용단가를 가져오는 구문
			decimal ApplyUnitCost=-1;				
			
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

			sqlApply = @"Select SmallClassificationName, OrderIntervalQuantity From II_MT inner join PUC_MT on Unit = SmallClassificationCode Where II_MT.RecodingState = 1 and ItemNum = @ItemNum";
			Applycom.CommandText = sqlApply;
			Applycom.Parameters.Add("@ItemNum",ItemSearchControl1.ItemNum);
			SqlDataReader reader = Applycom.ExecuteReader();
			while(reader.Read())
			{
				txtUnit.Text = reader["SmallClassificationName"].ToString();
				txtOrderIntervalQuantity.Text = reader["OrderIntervalQuantity"].ToString();
			}
			reader.Close();

			if(CSC1.BusinessRegistrationNum.Trim() != "" && ItemSearchControl1.ItemNum.Trim() != "" && ApplyUnitCost == -1)
				throw new Exception("품목 "+ItemSearchControl1.ItemNum+"의 단가정보가 존재하지 않습니다!");
			else
			{
				txtApplyUnitCost.Text = ApplyUnitCost.ToString();
			}

			
			
		}


		private void BuyingOrderAdd_PreRender(object sender, System.EventArgs e)
		{
		}		
	}
}
