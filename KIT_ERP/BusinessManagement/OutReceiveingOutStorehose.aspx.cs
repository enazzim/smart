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

namespace KIT_ERP.BusinessManagement
{
	/// <summary>
	/// OutReceiveingOutStorehose에 대한 요약 설명입니다.
	/// </summary>
	public class OutReceiveingOutStorehose : System.Web.UI.Page
	{
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcOutStorehouseDate;
		protected System.Web.UI.WebControls.TextBox txtThisTimeOutStorehouseQuantity;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		protected System.Web.UI.WebControls.Button btnClose;
		protected System.Web.UI.WebControls.Button btnInit;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdBusinessRegistrationNum;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdItemNum;
		protected System.Web.UI.WebControls.DropDownList ddlStore;
		protected System.Web.UI.WebControls.TextBox txtStoreQuantity;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		private string param;
		protected System.Web.UI.WebControls.LinkButton LinkButton2;
		private string param1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			ItemSearchControl1.Commodity = true;			//원자재 바인딩
			ItemSearchControl1.Products = true;				//제품 바인딩
			ItemSearchControl1.IsDoPost = true;
			CSC1.IsDoPost1 = true;
			CSC1.UnitCostDistinction = "수주거래처";


			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}						

			if(!Page.IsPostBack)
			{
				param = this.hdBusinessRegistrationNum.Value;	//거래처 웹콤보를 선택했는지 확인하는 변수
				param1 = this.hdItemNum.Value;					//품목 웹콤보를 선택했는지 확인하는 변수

				//품목정보 초기화
				btnInit.Attributes.Add("onClick","ResetTextBox();ResetBox();");

				txtThisTimeOutStorehouseQuantity.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this);");
				txtThisTimeOutStorehouseQuantity.Attributes.Add("OnKeyUp", "Process();OnKeyUp_Currency(this);");
				txtThisTimeOutStorehouseQuantity.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				txtThisTimeOutStorehouseQuantity.Attributes.Add("OnBlur", "OnBlur_Cur(this);");				
			
				wdcOutStorehouseDate.NullDateLabel = DateTime.Now.ToShortDateString();

				Session["param"] = this.CSC1.BusinessRegistrationNum;	//거래처를 선택했는지 확인하는 변수
				Session["param1"] = this.hdItemNum.Value;					//품목을 선택했는지 확인하는 변수

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
			this.ddlStore.SelectedIndexChanged += new System.EventHandler(this.LinkButton1_Click);
			this.LinkButton2.Click += new System.EventHandler(this.LinkButton2_Click);
			this.LinkButton1.Click += new System.EventHandler(this.LinkButton1_Click);
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void LinkButton1_Click(object sender, System.EventArgs e)
		{



			
			if(ItemSearchControl1.ItemNum != null)
			{
				Session["param1"] = ItemSearchControl1.ItemNum;			//품목을 선택했는지 확인하는 변수
				CSC1.ItemNum = ItemSearchControl1.ItemNum;

				decimal Quantity = 0;
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);			
				conn.Open();
				string str = @"select StockQuantity12 From BS_MT where ItemNum = @num and BusinessStorehouseNum = @index and RecodingState = 1 and [Year] = @year";
				SqlCommand comm = new SqlCommand();
				comm.Connection = conn;
				comm.CommandText = str;
				comm.Parameters.Add("@num",ItemSearchControl1.ItemNum.ToString());
				comm.Parameters.Add("@index",ddlStore.SelectedItem.Value);
				comm.Parameters.Add("@year",DateTime.Now.Year);
				SqlDataReader dr = comm.ExecuteReader();
				while(dr.Read())
				{
					Quantity = decimal.Parse(dr["StockQuantity12"].ToString());
				}
				dr.Close();
				conn.Close();

//				if(Quantity <= 0)
//				{
//					Response.Write("<script language=javascript>");
//					Response.Write("alert('재고가 없습니다!');");
//					Response.Write("</script>");
//				}
			
				txtStoreQuantity.Text = Quantity.ToString();
			}

			ItemSearchControl1.BusinessRegistrationNum = CSC1.BusinessRegistrationNum;
			ItemSearchControl1.UnitCostDistinction = "판매단가";
			hdItemNum.Value = ItemSearchControl1.ItemNum;




			
			

			
		}



		private void Button1_Click(object sender, System.EventArgs e)
		{
			if(ExistItem(ItemSearchControl1.ItemNum, ItemSearchControl1.ItemDrawNum, ItemSearchControl1.ItemName) == 0)
			{
				RegisterStartupScript("","<script language=javascript>alert('선출고를 보낼 품목을 정확히 선택해 주세요')</script>");
			}
			else if(CSC1.BusinessRegistrationNum.Trim() == "")
			{
				RegisterStartupScript("","<script language=javascript>alert('거래처를 정확히 선택해 주세요')</script>");
			}
			else if(txtThisTimeOutStorehouseQuantity.Text.Trim() == "" || txtThisTimeOutStorehouseQuantity.Text.Trim() == "0" )
			{
				RegisterStartupScript("","<script language=javascript>alert('출고수량을 입력해 주세요')</script>");
			}
			else
			{
				ArrayList preArray = new ArrayList();
				preArray.Add(ItemSearchControl1.ItemNum.ToString());		//품목번호0
				preArray.Add(ItemSearchControl1.ItemDrawNum.ToString());	//도면번호1
				preArray.Add(ItemSearchControl1.ItemName.ToString());		//품목명2
				preArray.Add(CSC1.Company);		//거래처명3
				preArray.Add(CSC1.BusinessRegistrationNum);			//사업자등록번호4
				preArray.Add(decimal.Parse(txtThisTimeOutStorehouseQuantity.Text));//금번출고량5
				preArray.Add(ddlStore.SelectedItem.Value);					//창고종류6
				if(this.wdcOutStorehouseDate.Value == null)
					preArray.Add(DateTime.Now.ToShortDateString());			//출고일자7
				else
					preArray.Add(this.wdcOutStorehouseDate.Value);			//출고일자7
				
				KIT_ERP.Register register = new KIT_ERP.Register(preArray,"OutReceiveingOutStorehose",Session["ID"].ToString());
				register.Registration();
			}
			
		}

		private int ExistItem(string ItemNum, string ItemDrawNum, string ItemName)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);			
			conn.Open();
			//품목번호와 공정코드로 공정순서정보테이블에서 진척비율과 공정순서번호를 가지고 온다.
			string str = "select Count(*) from II_MT where ItemNum = @ItemNum and ItemDrawNum = @ItemDrawNum and ItemName = @ItemName and RecodingState = 1";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@ItemNum",ItemNum);
			comm.Parameters.Add("@ItemDrawNum",ItemDrawNum);
			comm.Parameters.Add("@ItemName",ItemName);
			int count = int.Parse(comm.ExecuteScalar().ToString());
			conn.Close();

			return count;
			
		}

		/// <summary>
		/// 거래처를 선택시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LinkButton2_Click(object sender, System.EventArgs e)
		{
			Session["param"] = CSC1.BusinessRegistrationNum;
			ItemSearchControl1.BusinessRegistrationNum = CSC1.BusinessRegistrationNum;
			ItemSearchControl1.UnitCostDistinction = "판매단가";
			CSC1.ItemNum = hdItemNum.Value;
		}

		private void btnInit_Click(object sender, System.EventArgs e)
		{
			txtThisTimeOutStorehouseQuantity.Text = "0";
			txtStoreQuantity.Text = "0";
			ddlStore.SelectedIndex = 0;
			wdcOutStorehouseDate.Value = DateTime.Now.ToShortDateString();
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script language='javascript'>");
			Response.Write("window.close()");
			Response.Write("</script>");  
		}

		
	}
}
