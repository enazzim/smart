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

namespace KIT_ERP.BusinessManagement
{
	/// <summary>
	/// AddItemReceive에 대한 요약 설명입니다.
	/// </summary>
	public class AddItemReceive : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox txtCost;
		protected System.Web.UI.WebControls.TextBox txtQuantity;
		protected System.Web.UI.WebControls.TextBox txtTotalCost;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wcDeliveryDate;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		protected System.Web.UI.WebControls.Button btnUpdate;
		protected System.Web.UI.WebControls.Button btnDelete;
		protected System.Web.UI.WebControls.Button btnReg;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdindex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden rowIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdItemNum;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lblItemDrawNum;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdBusinessRegistrationNum;
		protected System.Web.UI.WebControls.TextBox tb_OrderNum;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wcReceiveDate;
		protected System.Web.UI.WebControls.TextBox txtOrderNum;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdHistoryIndex;
		protected System.Web.UI.WebControls.TextBox txtPropertyClassification;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;

		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			ItemSearchControl1.HalfFinishedProducts = true;
			ItemSearchControl1.RawMaterials = true;
			ItemSearchControl1.IsDoPost = true;
			CSC1.UnitCostDistinction = "수주거래처";

			if(!Page.IsPostBack)
			{
				//bt_Update.Attributes.Add("onClick", "return OK('선택한 품목을 수정');");
				btnDelete.Attributes.Add("onClick", "return OK('선택한 품목을 삭제');");



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

				wcReceiveDate.NullDateLabel = DateTime.Now.ToShortDateString();
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
			this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnReg.Click += new System.EventHandler(this.btnReg_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void LinkButton1_Click(object sender, System.EventArgs e)
		{
			txtCost.Text = "0";
			txtQuantity.Text = "0";
			txtPropertyClassification.Text = "";

			
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlTransaction tr = conn.BeginTransaction();
			try
			{
				string str = @"Select ItemNum, ItemDrawNum, StandardUnitCost, PropertyClassification From II_MT	
								Where ItemNum = @num and II_MT.RecodingState = 1";
				SqlCommand comm = new SqlCommand();
				comm.Connection = conn;
				comm.Transaction = tr;
				comm.Parameters.Add("@num",SqlDbType.VarChar).Value = ItemSearchControl1.ItemNum;
				comm.CommandText = str;
				SqlDataReader dr = comm.ExecuteReader();
				while(dr.Read())
				{
					txtCost.Text = dr["StandardUnitCost"].ToString();
					txtPropertyClassification.Text = dr["PropertyClassification"].ToString();
					
				}
				dr.Close();
				comm.Parameters.Clear();
			
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

		private void btnReg_Click(object sender, System.EventArgs e)
		{
			if(CSC1.BusinessRegistrationNum.Trim() == "")
				RegisterStartupScript("","<script>alert('거래처를 선택해 주세요!');</script>");
			else if(ItemSearchControl1.ItemNum == "")
				RegisterStartupScript("","<script>alert('품목을 선택해 주세요!');</script>");
			else if(txtCost.Text.Trim() == "0" || txtCost.Text.Trim() == "")
				RegisterStartupScript("","<script>alert('적용단가를 입력해 주세요!');</script>");
			else if(wcDeliveryDate.Text.Trim() == "")
				RegisterStartupScript("","<script>alert('납기일을 입력해 주세요!');</script>");
			else
			{
				string m_RequirePage = "ReceiveingRegistration";
				string m_User = Session["ID"].ToString();
				string m_RecodeIdx = hdindex.Value;

				ArrayList InputList = new ArrayList();

				InputList.Add(ItemSearchControl1.ItemNum.ToString());
				InputList.Add(ItemSearchControl1.ItemDrawNum.ToString());
				InputList.Add(ItemSearchControl1.ItemName.ToString());
				InputList.Add(CSC1.Company);
				InputList.Add(CSC1.BusinessRegistrationNum);
				InputList.Add(txtPropertyClassification.Text);
				InputList.Add("0");
				InputList.Add(wcReceiveDate.Text);
				InputList.Add(decimal.Parse(txtCost.Text));
				InputList.Add(decimal.Parse(txtQuantity.Text));
				InputList.Add(wcDeliveryDate.Text);
				InputList.Add("0");
				InputList.Add("");
				InputList.Add("0");
				InputList.Add("");
				InputList.Add("0");
				InputList.Add("");
				InputList.Add("0");
				InputList.Add("");
				InputList.Add(decimal.Parse(txtQuantity.Text));
				InputList.Add(decimal.Parse(txtQuantity.Text)*decimal.Parse(txtCost.Text));
				InputList.Add(0);//출고수량
				InputList.Add(0);//합격수량
				InputList.Add(0);//미검수량
				InputList.Add(decimal.Parse(txtQuantity.Text));//납품잔량
				InputList.Add(txtOrderNum.Text);
				InputList.Add("");
			
				Registration reg = new Registration(m_RequirePage,m_User,m_RecodeIdx, InputList);
			
				UltraWebGrid1.DataSource = reg.MainTableRegistration();
				UltraWebGrid1.DataBind();
				Data_Bind();
				
				btnUpdate.Enabled = false;
				btnDelete.Enabled = false;
				btnReg.Visible = true;
				
			}
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			string m_TableName = "ReceiveingRegistration";
			string m_RecodeIdx = hdindex.Value;
		

			if(hdindex.Value == "0")
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('품목을 선택한뒤 삭제해 주세요.');");
				Response.Write("</script>");
			}
			else
			{
				Registration del= new Registration(m_TableName, m_RecodeIdx);

				UltraWebGrid1.DataSource = del.MainTableDelete();
				UltraWebGrid1.DataBind();
				clear();

				Data_Bind();

				btnUpdate.Enabled = false;
				btnDelete.Enabled = false;
				btnReg.Visible = true;

			}
			
		}

		private void clear()
		{
			
			wcReceiveDate.Value = DateTime.Now.ToShortDateString();
			wcDeliveryDate.Value = "";

			txtPropertyClassification.Text ="";
			txtCost.Text = "0";
			txtOrderNum.Text = "";
			
			txtQuantity.Text = "0";
			txtTotalCost.Text = "0";
			hdindex.Value ="0";
			
			ItemSearchControl1.ItemDrawNum = "";
			ItemSearchControl1.ItemNum = "";
			ItemSearchControl1.ItemName = "";
			CSC1.BusinessRegistrationNum ="";
			CSC1.Company = "";
			hdBusinessRegistrationNum.Value = "";
			hdItemNum.Value = "";
			rowIndex.Value = "";

		}

		private void btnUpdate_Click(object sender, System.EventArgs e)
		{
			
			string m_RequirePage = "ReceiveingRegistration";
			string m_User = Session["ID"].ToString();
			string m_RecodeIdx = hdindex.Value;

			ArrayList InputList = new ArrayList();

			InputList.Add(ItemSearchControl1.ItemNum.ToString());
			InputList.Add(ItemSearchControl1.ItemDrawNum.ToString());
			InputList.Add(ItemSearchControl1.ItemName.ToString());
			InputList.Add(CSC1.Company);
			InputList.Add(CSC1.BusinessRegistrationNum);
			InputList.Add(txtPropertyClassification.Text);
			InputList.Add("0");
			InputList.Add(wcReceiveDate.Text);
			InputList.Add(decimal.Parse(txtCost.Text));
			InputList.Add(decimal.Parse(txtQuantity.Text));
			InputList.Add(wcDeliveryDate.Text);
			InputList.Add("0");
			InputList.Add("");
			InputList.Add("0");
			InputList.Add("");
			InputList.Add("0");
			InputList.Add("");
			InputList.Add("0");
			InputList.Add("");
			InputList.Add(decimal.Parse(txtQuantity.Text));
			InputList.Add(decimal.Parse(txtQuantity.Text)*decimal.Parse(txtCost.Text));
			InputList.Add(0);//출고수량
			InputList.Add(0);//합격수량
			InputList.Add(0);//미검수량
			InputList.Add(decimal.Parse(txtQuantity.Text));//납품잔량
			InputList.Add(txtOrderNum.Text);
			InputList.Add("");
			InputList.Add(hdindex.Value);
			
			if(hdindex.Value == "0")
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('품목을 선택한뒤 수정해 주세요.');");
				Response.Write("</script>");
			}
			else
			{
				if(txtCost.Text.Trim() == "0" || txtCost.Text.Trim() == "")
					RegisterStartupScript("","<script>alert('적용단가를 입력해 주세요!');</script>");
				else
				{
					Registration up = new Registration(m_RequirePage,m_User,m_RecodeIdx, InputList);
			
					UltraWebGrid1.DataSource = up.MainTableUpdate();
					UltraWebGrid1.DataBind();

					Data_Bind();

					btnUpdate.Enabled = false;
					btnDelete.Enabled = false;
					btnReg.Visible = true;
					
				}
			}
		}


		/// <summary>
		/// 등록후 다시 바인딩 되면서 제일큰 인덱스 번호를 hdindex에 저장하는 함수
		/// </summary>
		private void FindIndex()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "SELECT Max(ReceivingOrderHistoryIndex) From RO_HT";
			SqlCommand comm =  new SqlCommand(str,conn);
			hdindex.Value = comm.ExecuteScalar().ToString();
			conn.Close();
		}

		private void Data_Bind()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string str = @"SELECT isnull(PUC.SmallClassificationName,'') as ItemState,   II_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName,
				II_MT.PropertyClassification, PUC_MT.SmallClassificationName as Unit, II_MT.Standard , 
				II_MT.Standard, RO_HT.CompanyName, RO_HT.BusinessRegistrationNum, CompanyPersonInCharge, CI_MT.TelephoneNum, 
				RO_HT.ReceivingOrderDate, 
				RO_HT.ApplyUnitCost, RO_HT.DeliveryRequestQuantity1, 
				CASE RO_HT.ProductionRequestDivision WHEN '1' THEN '예' ELSE '아니오' END AS ProductionRequestDivision,
				RO_HT.DeliveryRequestDate1, RO_HT.DeliveryRequestQuantity2, 
				RO_HT.DeliveryRequestDate2, RO_HT.DeliveryRequestQuantity3,
				RO_HT.DeliveryRequestDate3, RO_HT.DeliveryRequestQuantity4, 
				RO_HT.DeliveryRequestDate4, RO_HT.DeliveryRequestQuantity5,
				RO_HT.DeliveryRequestDate5, RO_HT.TotalReceiveingOrderQuantity,
				RO_HT.TotalCost, RO_HT.OutStorehouseQuantity, RO_HT.SuitabilityQuantity,
				RO_HT.UnInspectionQuantity, RO_HT.RemainderQuantity, RO_HT.OrderNum,
				RO_HT.DeliveryPlace, RO_HT.VolumNum, RO_HT.ProgressCondition,
				RO_HT.RegistrationPerson, RO_HT.RegistrationPersonID,
				RO_HT.RegistrationDate, RO_HT.UpdatingPerson, RO_HT.UpdatingPersonID, 
				RO_HT.UpdatingDate, RO_HT.ReceivingOrderHistoryIndex 
				FROM RO_HT
				INNER JOIN II_MT ON RO_HT.ItemNum = II_MT.ItemNum
				Inner join CI_MT on RO_HT.BusinessRegistrationNum = CI_MT.BusinessRegistrationNum
				INNER JOIN PUC_MT ON II_MT.Unit = PUC_MT.SmallClassificationCode
				left outer join PUC_MT PUC on II_MT.ItemState = PUC.SmallClassificationCode				
				Where (RO_HT.PropertyClassification = '원자재' or RO_HT.PropertyClassification = '반제품') and CI_MT.RecodingState = 1 and II_MT.RecodingState = '1' and RO_HT.ReceivingOrderDate = @date and RO_HT.ProgressCondition = '대기'";
			
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@date",SqlDbType.SmallDateTime).Value = DateTime.Parse(wcReceiveDate.Text).ToShortDateString();

			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			da.Fill(ds);
			conn.Close();

			UltraWebGrid1.DataSource = ds;
			UltraWebGrid1.DataBind();
		}
	}
}
