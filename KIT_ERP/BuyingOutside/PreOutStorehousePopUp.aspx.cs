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
	/// PreOutStorehousePopUp에 대한 요약 설명입니다.
	/// </summary>
	public class PreOutStorehousePopUp : System.Web.UI.Page
	{
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcOutStorehouseDate;
		protected System.Web.UI.WebControls.DropDownList ddlProcessName;
		protected System.Web.UI.WebControls.Button btnRegister;
		protected System.Web.UI.WebControls.Button btnInit;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Button btnClose;
		protected System.Web.UI.WebControls.Button btLot;
		protected System.Web.UI.WebControls.TextBox Textbox3;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdLotNum;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdItemNum;//유저컨트롤 선언
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;

		private string param;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdBusinessRegistrationNum;
		protected System.Web.UI.WebControls.TextBox txtThisTimeOutStorehouseQuantity;
		private string param1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			ItemSearchControl1.RawMaterials = true;			//원자재 바인딩
			ItemSearchControl1.HalfFinishedProducts = true; //반제품 바인딩
			ItemSearchControl1.Products = true;				//제품 바인딩
			ItemSearchControl1.IsDoPost = true;			
			CSC1.UnitCostDistinction = "외주거래처";


			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}						

			if(!Page.IsPostBack)
			{
//				Session["UserName"] = "자재부장";
//				Session["ID"] = "admin";

				param = this.hdBusinessRegistrationNum.Value;	//거래처 웹콤보를 선택했는지 확인하는 변수
				param1 = this.hdItemNum.Value;					//품목 웹콤보를 선택했는지 확인하는 변수

				//품목정보 초기화
				btnInit.Attributes.Add("onClick","ResetTextBox();ResetBox();");

				txtThisTimeOutStorehouseQuantity.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this);");
				txtThisTimeOutStorehouseQuantity.Attributes.Add("OnKeyUp", "Comma(); OnKeyUp_Currency(this);");
				txtThisTimeOutStorehouseQuantity.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				txtThisTimeOutStorehouseQuantity.Attributes.Add("OnBlur", "OnBlur_Cur(this);");				
			

				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);			
				SqlDataAdapter PreAdapter = new SqlDataAdapter("procPreOutSotoreHousePopup",conn);
				DataSet dsPre = new DataSet();
				PreAdapter.Fill(dsPre);

				this.ddlProcessName.DataSource = dsPre.Tables[2];
				this.ddlProcessName.DataTextField = dsPre.Tables[2].Columns[1].ToString();
				this.ddlProcessName.DataValueField = dsPre.Tables[2].Columns[0].ToString();
				this.ddlProcessName.DataBind();
				this.ddlProcessName.Items.Insert(0,"-----선택-----");
				this.ddlProcessName.Items[0].Value = "";
				

				wdcOutStorehouseDate.NullDateLabel = DateTime.Now.ToShortDateString();

				ItemSearchControl1.RawMaterials = true;			//원자재 바인딩
				ItemSearchControl1.HalfFinishedProducts = true; //반제품 바인딩
				ItemSearchControl1.Products = true;				//제품 바인딩
				ItemSearchControl1.IsDoPost = true;			
				CSC1.UnitCostDistinction = "외주거래처";

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
			this.btLot.Click += new System.EventHandler(this.btLot_Click);
			this.LinkButton1.Click += new System.EventHandler(this.LinkButton1_Click);
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		//선출고 버튼을 누르면 현제 입력한 값을 출고원장에 기록을 한다. 
		//원자재 일경우는 원자재 창고에서 수량을 조정하고 반제품또는 제품일 경우는 생산창고의 수량을 조정한다.
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
			else if(Exist(ItemSearchControl1.ItemNum) && ddlProcessName.SelectedIndex == 0)
			{
				RegisterStartupScript("","<script language=javascript>alert('공정을 정확히 선택해 주세요')</script>");
			}
			else if(Exist(ItemSearchControl1.ItemNum, ddlProcessName.SelectedItem.Value,CSC1.BusinessRegistrationNum) == 0)
			{
				RegisterStartupScript("","<script language=javascript>alert('해당거래처에 출고품목 창고가 생성되어 있지 않습니다!')</script>");
			}
			else
			{
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);			
				conn.Open();
				//품목번호와 공정코드로 공정순서정보테이블에서 진척비율과 공정순서번호를 가지고 온다.
				string str = "select ProcessSequenceNum,ProgressRate from psi_mt where ItemNum = @ItemNum and processCode = @processCode";
				SqlCommand comm = new SqlCommand(str,conn);
				comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = ItemSearchControl1.ItemNum.ToString();
				comm.Parameters.Add("@processCode",SqlDbType.VarChar).Value = this.ddlProcessName.SelectedItem.Value.ToString();
				SqlDataReader PreReader = comm.ExecuteReader();
			
				
				//진척비율과 공정순서 저장하는 변수 선언
				int Sequence=0;
				decimal Progress=0;


				while(PreReader.Read())
				{
					Sequence = int.Parse(PreReader["ProcessSequenceNum"].ToString());
					Progress = decimal.Parse(PreReader["ProgressRate"].ToString());
				}

				PreReader.Close();
				conn.Close();
           											  
				//등록을 위한 ArrayList 객체 생성
				Textbox3.Text = hdLotNum.Value;
				ArrayList preArray = new ArrayList();
				preArray.Add(ItemSearchControl1.ItemNum.ToString());		//품목번호
				preArray.Add(ItemSearchControl1.ItemDrawNum.ToString());	//도면번호
				preArray.Add(ItemSearchControl1.ItemName.ToString());		//품목명
				preArray.Add(Progress);										//진척비율
				preArray.Add(Sequence);										//공정순서
				if(ddlProcessName.SelectedIndex == 0)
				{
					preArray.Add("14000000");	//공정코드
					preArray.Add("소재");		//공정명
				}
				else
				{
					preArray.Add(this.ddlProcessName.SelectedItem.Value);	//공정코드
					preArray.Add(this.ddlProcessName.SelectedItem.Text);	//공정명
				}
				
				preArray.Add(CSC1.Company);		//거래처명
				preArray.Add(CSC1.BusinessRegistrationNum);			//사업자등록번호
				preArray.Add(decimal.Parse(txtThisTimeOutStorehouseQuantity.Text));//금번출고량
				if(this.wdcOutStorehouseDate.Value == null)
					preArray.Add(DateTime.Now.ToShortDateString());			//출고일자
				else
					preArray.Add(this.wdcOutStorehouseDate.Value);			//출고일자
				preArray.Add(Textbox3.Text);								//Lot번호
			
				KIT_ERP.Register register = new KIT_ERP.Register(preArray,"PreOutStorehousePopUp",Session["ID"].ToString());
				register.Registration();

			}		
		}

		private bool Exist(string ItemNum)
		{
			string PropertyClassification = "";
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);			
			conn.Open();
			//품목번호와 공정코드로 공정순서정보테이블에서 진척비율과 공정순서번호를 가지고 온다.
			string str = "select PropertyClassification from II_MT where ItemNum = @ItemNum and RecodingState = 1";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@ItemNum",ItemNum);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				PropertyClassification = dr["PropertyClassification"].ToString();
			}
			conn.Close();

			if(PropertyClassification == "원자재")
				return false;
			else
				return true;
			
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

		private int Exist(string ItemNum, string Process, string BusinessRegistrationNum)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);			
			conn.Open();
			//품목번호와 공정코드로 공정순서정보테이블에서 진척비율과 공정순서번호를 가지고 온다.
			string str = "select Count(*) from OS_MT where ItemNum = @ItemNum and ProcessCode = @Process and BusinessRegistrationNum = @BusinessRegistrationNum and RecodingState = 1 and [Year] =  @year";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@ItemNum",ItemNum);
			if(!Exist(ItemNum))
				comm.Parameters.Add("@Process","14000000");
			else
				comm.Parameters.Add("@Process",Process);
			comm.Parameters.Add("@BusinessRegistrationNum",BusinessRegistrationNum);
			if(this.wdcOutStorehouseDate.Value == null)
				comm.Parameters.Add("@year",DateTime.Now.Year);
			else
				comm.Parameters.Add("@year",DateTime.Parse(wdcOutStorehouseDate.Text).Year);

			int count = int.Parse(comm.ExecuteScalar().ToString());
			conn.Close();

			return count;
		}

		//초기화 버튼 클릭시 각 컨트롤의 내용이 초기화 된다
		private void btnInit_Click(object sender, System.EventArgs e)
		{
			//품목정보 초기화
			
			this.txtThisTimeOutStorehouseQuantity.Text = null;
			this.ddlProcessName.SelectedIndex = 0;
			this.wdcOutStorehouseDate.Value = DateTime.Now.ToShortDateString();
		}

		
		//화면 닫기 버튼 클릭시 현제 띄어진 팝업창이 사라진다.
		private void btnClose_Click(object sender, System.EventArgs e)
		{			
			Response.Write("<script language='javascript'>");
			Response.Write("window.close()");
			Response.Write("</script>");  			
		}

		
		private void LinkButton1_Click(object sender, System.EventArgs e)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);			
			conn.Open();
			string str = @"select SmallClassificationName, PSI_MT.ProcessCode From PSI_MT inner join PUC_MT on PSI_MT.ProcessCode = PUC_MT.SmallClassificationCode where PSI_MT.RecodingState = 1 and ItemNum = @num order by SmallClassificationName  asc";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = str;
			comm.Parameters.Add("@num",ItemSearchControl1.ItemNum.ToString());
			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			da.Fill(ds);
			comm.Parameters.Clear();
			this.ddlProcessName.DataSource = ds; 
			this.ddlProcessName.DataTextField =  ds.Tables[0].Columns[0].ToString();
			this.ddlProcessName.DataValueField =  ds.Tables[0].Columns[1].ToString();
			this.ddlProcessName.DataBind();
			this.ddlProcessName.Items.Insert(0,"-----선택-----");
			this.ddlProcessName.Items[0].Value = "";
			conn.Close();
		}
		
		private void btLot_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script>window.open('../LotNumberManagement.aspx','LotNumberManagement','width=760,height=400,left=300,top=400, center=yes, resizable= yes');</script>");				

		}

		
		
	}
}
