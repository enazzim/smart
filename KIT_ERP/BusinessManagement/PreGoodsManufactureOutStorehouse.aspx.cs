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

namespace mlim_ERP.BusinessManagement
{
	/// <summary>
	/// PreGoodsManufactureOutStorehouse에 대한 요약 설명입니다.
	/// </summary>
	public class PreGoodsManufactureOutStorehouse : System.Web.UI.Page
	{
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcOutStorehouseDate;
		protected System.Web.UI.WebControls.TextBox txtThisTimeOutStorehouseQuantity;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		protected System.Web.UI.WebControls.Button btnClose;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdQuantity;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdBusinessRegistrationNum;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdItemNum;
		protected System.Web.UI.WebControls.TextBox tbStoreQuantity;
		protected System.Web.UI.WebControls.TextBox tbApplyUnitCost;
		protected System.Web.UI.WebControls.TextBox tbTax;
		protected System.Web.UI.WebControls.LinkButton LinkButton2;
		//private mlim_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;
		//private mlim_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected mlim_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected mlim_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;

		private string param;
		protected System.Web.UI.WebControls.Button btnInit;
		private string param1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			ItemSearchControl1.Commodity = true;
			ItemSearchControl1.Products = true;
			ItemSearchControl1.IsDoPost = true;
			CSC1.UnitCostDistinction = "수주거래처";
			CSC1.IsDoPost1= true;

			//젤 처음 페이지가 로드될때....
			if(!Page.IsPostBack)
			{
				param = this.hdBusinessRegistrationNum.Value;	//거래처 웹콤보를 선택했는지 확인하는 변수
				param1 = this.hdItemNum.Value;					//품목 웹콤보를 선택했는지 확인하는 변수	

				ItemSearchControl1.Commodity = true;
				ItemSearchControl1.Products = true;
				ItemSearchControl1.IsDoPost = true;
			
				CSC1.UnitCostDistinction = "수주거래처";
				CSC1.IsDoPost1= true;

				Session["param"] = this.CSC1.BusinessRegistrationNum;	//거래처 웹콤보를 선택했는지 확인하는 변수
				Session["param1"] = this.hdItemNum.Value;					//품목 웹콤보를 선택했는지 확인하는 변수
			}
			else
			{
				if(Session["param"].ToString().Length != 0)
				{
					ItemSearchControl1.BusinessRegistrationNum = CSC1.BusinessRegistrationNum;
					ItemSearchControl1.UnitCostDistinction = "판매단가";
				}
				
				if(Session["param1"].ToString().Length != 0)
				{
					CSC1.ItemNum = hdItemNum.Value;
				}
				param = Session["param"].ToString();	//거래처 웹콤보를 선택했는지 확인하는 변수
				param1 = Session["param1"].ToString();					//품목 웹콤보를 선택했는지 확인하는 변수
			}
			// 등록버튼에 대하여 onclick속성을 추가 시킴
			txtThisTimeOutStorehouseQuantity.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this);");
			// onkeyup 이벤트가 발생하면 수량입력과동시에 금액을 계산하는 자바스크립트 함수 매핑
			txtThisTimeOutStorehouseQuantity.Attributes.Add("OnKeyUp", "Process(); OnKeyUp_Currency(this);");
			// focus를 얻으면 자동으로 select()
			txtThisTimeOutStorehouseQuantity.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
			// focus를 잃으면 다시 입력란을 체크
			txtThisTimeOutStorehouseQuantity.Attributes.Add("OnBlur", "OnBlur_Cur(this);");
			//tb_DeliveryRequestQuantity1.Attributes.Add("OnChange", "return checkNumber(this)");

			tbApplyUnitCost.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this);");
			tbApplyUnitCost.Attributes.Add("OnKeyUp", "Process(); OnKeyUp_Currency(this);");
			tbApplyUnitCost.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
			tbApplyUnitCost.Attributes.Add("OnBlur", "OnBlur_Cur(this);");
			btnInit.Attributes.Add("onClick", "ResetTextBox();ResetBox();");
			
			wdcOutStorehouseDate.NullDateLabel = DateTime.Now.ToShortDateString();
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
			this.LinkButton2.Click += new System.EventHandler(this.LinkButton2_Click);
			this.LinkButton1.Click += new System.EventHandler(this.LinkButton1_Click);
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
			this.Load += new System.EventHandler(this.Page_Load);
			this.PreRender += new System.EventHandler(this.PreGoodsManufactureOutStorehouse_PreRender);

		}
		#endregion

		/// <summary>
		/// 품목선택시 창고에가서 수량 가져올것
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LinkButton1_Click(object sender, System.EventArgs e)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "";
			str = "Select StockQuantity12 From BS_MT WHERE RecodingState = 1 and  ItemNum = @num and BusinessStorehouseNum = '1' and [Year] = @year";

			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@num", ItemSearchControl1.ItemNum);
			comm.Parameters.Add("@year",DateTime.Now.Year);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				
				tbStoreQuantity.Text = dr["StockQuantity12"].ToString();
				
			}
			dr.Read();
			if(tbStoreQuantity.Text == "0")
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('재고가 없습니다!');");
				Response.Write("</script>");
			}
			conn.Close();

			//선택한 품목과 거래처에 해당하는 적용단가를 가지고오는 함추호출
			AppCost();		
		}

		/// <summary>
		/// 등록버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button1_Click(object sender, System.EventArgs e)
		{
			if(ItemSearchControl1.ItemName == null || ItemSearchControl1.ItemName == "")
			{
				RegisterStartupScript("","<script language=javascript>alert('선출고를 보낼 품목이 없습니다.품목을 선택해 주세요')</script>");
			}
			else
			{
				//등록을 위한 ArrayList 객체 생성
				ArrayList preArray = new ArrayList();
				preArray.Add(ItemSearchControl1.ItemNum.ToString());		//0품목번호
				preArray.Add(ItemSearchControl1.ItemDrawNum.ToString());	//1도면번호
				preArray.Add(ItemSearchControl1.ItemName.ToString());		//2품목명
				preArray.Add(CSC1.Company);									//3거래처명
				preArray.Add(CSC1.BusinessRegistrationNum);					//4사업자등록번호
				preArray.Add(this.txtThisTimeOutStorehouseQuantity.Text);	//5금번출고량
				if(this.wdcOutStorehouseDate.Value == null)
					preArray.Add(DateTime.Now.ToShortDateString());			//6출고일자
				else
					preArray.Add(this.wdcOutStorehouseDate.Value);			//6출고일자
				preArray.Add(tbApplyUnitCost.Text);							//7적용단가
				preArray.Add(tbTax.Text);									//8부가세
				
				mlim_ERP.Register register = new mlim_ERP.Register(preArray,"PreGoodsManufactureOutStorehouse",Session["ID"].ToString());
				register.Registration();

			}

			Session["param"] = this.CSC1.BusinessRegistrationNum;	//거래처 웹콤보를 선택했는지 확인하는 변수
			Session["param1"] = this.hdItemNum.Value;					//품목 웹콤보를 선택했는지 확인하는 변수
		}

		/// <summary>
		/// 거래처 선택시 품목과 더불어 판매단가를 가져오는 함수
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LinkButton2_Click(object sender, System.EventArgs e)
		{
			Session["param"] = CSC1.BusinessRegistrationNum;
			ItemSearchControl1.BusinessRegistrationNum = CSC1.BusinessRegistrationNum;
			ItemSearchControl1.UnitCostDistinction = "판매단가";
			CSC1.ItemNum = hdItemNum.Value;
			//선택한 품목과 거래처에 해당하는 적용단가를 가지고오는 함추호출
			AppCost();
		}

		//품목을 선택해서 해당 거래처에 해당하는 적용단가를 가지고 오는 함수
		public string AppCost()
		{
			//품목을 선택했을때 품목의 적용단가를 가져오는 구문
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();	
			Decimal ApplyUnitCost=0;				
			
			//단가정보테이블에서 내가 선택한 품목과 거래처에 해당하는 품목의 단가구하는 쿼리
			string sqlApply = @"SELECT UCI_MT.StandardUnitCost FROM UCI_MT where UCI_MT.ItemNum = @ItemNum AND UCI_MT.BusinessRegistrationNum like @Num";

			SqlCommand Applycom = new SqlCommand(sqlApply,conn);
			Applycom.Parameters.Add("@ItemNum",ItemSearchControl1.ItemNum);
			Applycom.Parameters.Add("@Num",CSC1.BusinessRegistrationNum);
			SqlDataReader dr = Applycom.ExecuteReader();
			Applycom.Parameters.Clear();
			while(dr.Read())
			{
				ApplyUnitCost = Decimal.Parse(dr["StandardUnitCost"].ToString());
			}			
			tbApplyUnitCost.Text = Convert.ToString(ApplyUnitCost);
			dr.Close();
			conn.Close();

			return tbApplyUnitCost.Text;
		}

		
		private void ItemInit()
		{
			//검색조건 초기화
						
			CSC1.BusinessRegistrationNum = "";
			CSC1.Company="";
			hdBusinessRegistrationNum.Value ="";
			hdItemNum.Value = "";
			Session["param"] = "";
			Session["param1"] = "";
			ItemSearchControl1.BusinessRegistrationNum = CSC1.BusinessRegistrationNum;
			ItemSearchControl1.UnitCostDistinction = "판매단가";
			CSC1.ItemNum = hdItemNum.Value;
			tbStoreQuantity.Text = "0";
			txtThisTimeOutStorehouseQuantity.Text = "0";
			tbApplyUnitCost.Text = "0";
			tbTax.Text = "10";
			wdcOutStorehouseDate.Value = DateTime.Now.ToShortDateString();
		}
		
		private void PreGoodsManufactureOutStorehouse_PreRender(object sender, System.EventArgs e)
		{
			if(Page.IsPostBack)
			{
				
				param = (string)Session["param"];
				param1 = (string)Session["param1"];

				ItemSearchControl1.Products = true; //원자재 바인딩
				ItemSearchControl1.Commodity = true;	//상품 바인딩
				ItemSearchControl1.IsDoPost = true;
				ItemSearchControl1.BusinessRegistrationNum = CSC1.BusinessRegistrationNum;
							
				CSC1.UnitCostDistinction = "판매단가";
				CSC1.ItemNum = Session["param1"].ToString();
				CSC1.IsDoPost1= true;
			}

			//품목정보 초기화
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script language='javascript'>");
			Response.Write("window.close()");
			Response.Write("</script>");  	
		}

		private void btnInit_Click(object sender, System.EventArgs e)
		{
			ItemInit();		
		}		
	
		
	}
}
