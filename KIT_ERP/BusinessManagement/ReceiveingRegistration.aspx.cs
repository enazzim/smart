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
	/// ReceiveingRegistration에 대한 요약 설명입니다.
	/// </summary>
	public class ReceiveingRegistration : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label searchTitle;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.Label Label16;
		protected System.Web.UI.WebControls.Label Label17;
		protected System.Web.UI.WebControls.Label Label18;
		protected System.Web.UI.WebControls.Label Label19;
		protected System.Web.UI.WebControls.Label Label20;
		protected System.Web.UI.WebControls.Label Label21;
		protected System.Web.UI.WebControls.Label Label22;
		protected System.Web.UI.WebControls.Label Label23;
		protected System.Web.UI.WebControls.Label Label25;
		protected System.Web.UI.WebControls.Label Label26;
		protected System.Web.UI.WebControls.Label Label27;
		protected System.Web.UI.WebControls.Label Label24;
		protected System.Web.UI.WebControls.Label Label28;
		protected System.Web.UI.WebControls.Label Label29;
		protected System.Web.UI.WebControls.TextBox tb_PropertyClassification;
		protected System.Web.UI.WebControls.TextBox tb_Unit;
		protected System.Web.UI.WebControls.TextBox tb_Standard;
		protected System.Web.UI.WebControls.TextBox tb_ReceiveUnitCost;
		protected System.Web.UI.WebControls.TextBox tb_ApplyUnitCost;
		protected System.Web.UI.WebControls.TextBox tb_DeliveryPlace;
		protected System.Web.UI.WebControls.TextBox tb_TotalReceiveingOrderQuantity;
		protected System.Web.UI.WebControls.TextBox tb_TotalCost;
		//protected System.Web.UI.WebControls.Label lb_BusinessNum;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcDeliveryRequestDate1;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Button bt_Update;
		protected System.Web.UI.WebControls.Button bt_Delete;
		protected System.Web.UI.WebControls.TextBox tb_CompanyPersonInCharge;
		protected System.Web.UI.WebControls.TextBox tb_TelephoneNum;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.Label Label15;
		protected System.Web.UI.WebControls.TextBox tb_OrderNum;
		
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.TextBox txtItemState;
		protected System.Web.UI.WebControls.LinkButton LinkButton2;
		protected System.Web.UI.HtmlControls.HtmlInputButton btRegister;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;

		
		protected System.Web.UI.WebControls.LinkButton LinkButton3;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdItemNum;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdBusinessRegistrationNum;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcReceivingOrderDate;
		protected System.Web.UI.WebControls.DropDownList dlProductionRequestDivision;
		protected System.Web.UI.WebControls.LinkButton LinkButton;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Index;
		private string param;
		private string param1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			ItemSearchControl1.Commodity = true; //상품 바인딩				
			ItemSearchControl1.Products = true; //제품 바인딩
			ItemSearchControl1.HalfFinishedProducts = true;//반제품 바인딩
			ItemSearchControl1.RawMaterials = true;//원자재 바인딩
			CSC1.IsDoPost1 = true;
			ItemSearchControl1.IsDoPost = true;
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

				//bt_Update.Attributes.Add("onClick", "return OK('선택한 품목을 수정');");
				bt_Delete.Attributes.Add("onClick", "return OK('선택한 품목을 삭제');");



				tb_TotalReceiveingOrderQuantity.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this);");
				// onkeyup 이벤트가 발생하면 수량입력과동시에 금액을 계산하는 자바스크립트 함수 매핑
				tb_TotalReceiveingOrderQuantity.Attributes.Add("OnKeyUp", "Process(); OnKeyUp_Currency(this);");
				// focus를 얻으면 자동으로 select()
				tb_TotalReceiveingOrderQuantity.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				// focus를 잃으면 다시 입력란을 체크
				tb_TotalReceiveingOrderQuantity.Attributes.Add("OnBlur", "OnBlur_Cur(this);");
				//tb_DeliveryRequestQuantity1.Attributes.Add("OnChange", "return checkNumber(this)");

				tb_ApplyUnitCost.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this);");
				tb_ApplyUnitCost.Attributes.Add("OnKeyUp", "Process(); OnKeyUp_Currency(this);");
				tb_ApplyUnitCost.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				tb_ApplyUnitCost.Attributes.Add("OnBlur", "OnBlur_Cur(this);");

				wdcReceivingOrderDate.NullDateLabel = DateTime.Now.ToShortDateString();
				
				UltraWebGrid1.DataSource = Search();
				UltraWebGrid1.DataBind();

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

				UltraWebGrid1.DataSource = Search();
				UltraWebGrid1.DataBind();
			}
		}

		private DataSet Search()
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
				
				Where RO_HT.VolumNum is null and CI_MT.RecodingState = 1 and II_MT.RecodingState = '1' and RO_HT.RegistrationDate = @date and RO_HT.ProgressCondition = '대기' order by ReceivingOrderHistoryIndex desc";
			
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@date", DateTime.Now.ToShortDateString());

			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			da.Fill(ds);
			conn.Close();

			return ds;

			
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
			this.LinkButton.Click += new System.EventHandler(this.LinkButton_Click);
			this.LinkButton3.Click += new System.EventHandler(this.LinkButton3_Click);
			this.LinkButton2.Click += new System.EventHandler(this.LinkButton2_Click);
			this.LinkButton1.Click += new System.EventHandler(this.LinkButton1_Click);
			this.bt_Update.Click += new System.EventHandler(this.bt_Update_Click);
			this.bt_Delete.Click += new System.EventHandler(this.bt_Delete_Click);
			this.btRegister.ServerClick += new System.EventHandler(this.btRegister_ServerClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion



		/// <summary>
		/// 수정버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Update_Click(object sender, System.EventArgs e)
		{
			
			string m_RequirePage = "ReceiveingRegistration";
			string m_User = Session["ID"].ToString();
			string m_RecodeIdx = lb_Index.Value;

			ArrayList InputList = new ArrayList();

			InputList.Add(ItemSearchControl1.ItemNum.ToString());
			InputList.Add(ItemSearchControl1.ItemDrawNum.ToString());
			InputList.Add(ItemSearchControl1.ItemName.ToString());
			InputList.Add(CSC1.Company);
			InputList.Add(CSC1.BusinessRegistrationNum);
			InputList.Add(tb_PropertyClassification.Text);
			InputList.Add(dlProductionRequestDivision.SelectedItem.Value);
			InputList.Add(wdcReceivingOrderDate.Text);
			InputList.Add(decimal.Parse(tb_ApplyUnitCost.Text));
			InputList.Add(decimal.Parse(tb_TotalReceiveingOrderQuantity.Text));
			InputList.Add(wdcDeliveryRequestDate1.Text);
			InputList.Add("0");
			InputList.Add("");
			InputList.Add("0");
			InputList.Add("");
			InputList.Add("0");
			InputList.Add("");
			InputList.Add("0");
			InputList.Add("");
			InputList.Add(decimal.Parse(tb_TotalReceiveingOrderQuantity.Text));
			InputList.Add(decimal.Parse(tb_TotalReceiveingOrderQuantity.Text)*decimal.Parse(tb_ApplyUnitCost.Text));
			InputList.Add(0);//출고수량
			InputList.Add(0);//적합수량
			InputList.Add(0);//미검수량
			InputList.Add(decimal.Parse(tb_TotalReceiveingOrderQuantity.Text));//납품잔량
			InputList.Add(tb_OrderNum.Text);
			InputList.Add(tb_DeliveryPlace.Text);
			InputList.Add(lb_Index.Value);
			
			if(lb_Index.Value == "0")
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('품목을 선택한뒤 수정해 주세요.');");
				Response.Write("</script>");
			}
			else
			{
				if(tb_ApplyUnitCost.Text.Trim() == "0" || tb_ApplyUnitCost.Text.Trim() == "")
					RegisterStartupScript("","<script>alert('적용단가를 입력해 주세요!');</script>");
				else
				{
					Registration up = new Registration(m_RequirePage,m_User,m_RecodeIdx, InputList);
			
//					UltraWebGrid1.DataSource = up.MainTableUpdate();
//                    UltraWebGrid1.DataBind();
					up.ReceiveUpdate();

					UltraWebGrid1.DataSource = Search();
					UltraWebGrid1.DataBind();

					bt_Update.Enabled = false;
					bt_Delete.Enabled = false;
					btRegister.Visible = true;
				}
			}
			ItemSearchControl1.BusinessRegistrationNum = hdBusinessRegistrationNum.Value;
			ItemSearchControl1.UnitCostDistinction = "판매단가";
			CSC1.ItemNum = hdItemNum.Value;
			CSC1.UnitCostDistinction= "수주거래처";
		}

		/// <summary>
		/// 발주번호가 동일한 것이 있는지 파악하는 함수
		/// </summary>
		/// <returns></returns>
		private bool ReceiveingValidate()
		{
			int Exist = 0;
			if(tb_OrderNum.Text.Trim() != "")
			{
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			
				string str = @"Select count(OrderNum) From RO_HT where OrderNum = @OrderNum and ReceivingOrderHistoryIndex != @Index";
				SqlCommand comm = new SqlCommand(str,conn);
				//발주번호가 동일한 것이 있는지 파악
				comm.Parameters.Add("@OrderNum", tb_OrderNum.Text.Trim());
				comm.Parameters.Add("@Index",lb_Index.Value);
			
				conn.Open();
				Exist = int.Parse(comm.ExecuteScalar().ToString());
				comm.Parameters.Clear();
				conn.Close();
			}
			if(Exist == 0)
				return true;
			else
				return false;
			
		}

		/// <summary>
		/// 삭제버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Delete_Click(object sender, System.EventArgs e)
		{
			string m_TableName = "ReceiveingRegistration";
			string m_RecodeIdx = lb_Index.Value;
		

			if(lb_Index.Value == "0")
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('품목을 선택한뒤 삭제해 주세요.');");
				Response.Write("</script>");
			}
			else
			{
				Registration del= new Registration(m_TableName, m_RecodeIdx);

				//UltraWebGrid1.DataSource = del.MainTableDelete();
                //UltraWebGrid1.DataBind();
				 del.ReceiveDelete();

				UltraWebGrid1.DataSource = Search();
				UltraWebGrid1.DataBind();


				bt_Update.Enabled = false;
				bt_Delete.Enabled = false;
				btRegister.Visible = true;

			}
			ItemSearchControl1.BusinessRegistrationNum = hdBusinessRegistrationNum.Value;
			ItemSearchControl1.UnitCostDistinction = "판매단가";
			CSC1.ItemNum = hdItemNum.Value;
			CSC1.UnitCostDistinction= "수주거래처";
			
		}

		private void clear()
		{
			
			wdcReceivingOrderDate.Value = DateTime.Now.ToShortDateString();
			tb_TelephoneNum.Text = "";
			tb_CompanyPersonInCharge.Text = "";
			hdBusinessRegistrationNum.Value = "";			
			tb_PropertyClassification.Text ="";
			tb_Unit.Text = "";
			tb_Standard.Text = "";
			tb_ApplyUnitCost.Text = "0";
			tb_ReceiveUnitCost.Text = "0";
			tb_DeliveryPlace.Text = "";
			tb_OrderNum.Text = "";
			

			dlProductionRequestDivision.SelectedIndex = 0;
			wdcDeliveryRequestDate1.Value = "";
			tb_TotalReceiveingOrderQuantity.Text = "0";
			tb_TotalCost.Text = "0";
			lb_Index.Value ="0";
			hdBusinessRegistrationNum.Value = "";
			ItemSearchControl1.ItemDrawNum = "";
			ItemSearchControl1.ItemNum = "";
			ItemSearchControl1.ItemName = "";
			CSC1.BusinessRegistrationNum ="";
			CSC1.Company = "";
			hdBusinessRegistrationNum.Value = "";
			hdItemNum.Value = "";
			txtItemState.Text = "";

		}



		/// <summary>
		/// 등록후 다시 바인딩 되면서 제일큰 인덱스 번호를 lb_Index에 저장하는 함수
		/// </summary>
		private void FindIndex()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "SELECT Max(ReceivingOrderHistoryIndex) From RO_HT";
			SqlCommand comm =  new SqlCommand(str,conn);
			lb_Index.Value = comm.ExecuteScalar().ToString();
			conn.Close();
		}


		/// <summary>
		/// 품목을 선택했을때
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LinkButton1_Click(object sender, System.EventArgs e)
		{

			if(ItemSearchControl1.ItemNum != null)
			{
				Session["param1"] = ItemSearchControl1.ItemNum;			//품목을 선택했는지 확인하는 변수
				CSC1.ItemNum = ItemSearchControl1.ItemNum;

				tb_ReceiveUnitCost.Text = "0";
				tb_ApplyUnitCost.Text = "0";
				txtItemState.Text = "";

				string person = "";
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				conn.Open();
				SqlTransaction tr = conn.BeginTransaction();
				try
				{
//					string str = @"Select ItemNum, ItemDrawNum,PropertyClassification, Unit, Standard, ChargePerson, isnull(SmallClassificationName, '') as ItemState 
//								From II_MT left outer join PUC_MT on ItemState = SmallClassificationCode
//								Where ItemNum = @num and (PropertyClassification = '제품' or PropertyClassification = '상품') and II_MT.RecodingState = 1";
					string str = @"Select ItemNum, ItemDrawNum,PropertyClassification, Unit, Standard, ChargePerson, isnull(SmallClassificationName, '') as ItemState 
								From II_MT left outer join PUC_MT on ItemState = SmallClassificationCode
								Where ItemNum = @num and II_MT.RecodingState = 1";
					SqlCommand comm = new SqlCommand();
					comm.Connection = conn;
					comm.Transaction = tr;
					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = ItemSearchControl1.ItemNum;
					comm.CommandText = str;
					SqlDataReader dr = comm.ExecuteReader();
					while(dr.Read())
					{
						tb_PropertyClassification.Text = dr["PropertyClassification"].ToString();
						tb_Unit.Text = dr["Unit"].ToString();
						tb_Standard.Text = dr["Standard"].ToString();
						person = dr["ChargePerson"].ToString();
						if(tb_PropertyClassification.Text == "상품")
							dlProductionRequestDivision.SelectedIndex = 1;
						else
							dlProductionRequestDivision.SelectedIndex = 0;
						txtItemState.Text = dr["ItemState"].ToString();
					}
					dr.Close();
					comm.Parameters.Clear();
			
					//단위 입력
					str = "Select SmallClassificationName from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and SmallClassificationCode = @code" ;
					comm.Parameters.Add("@code",SqlDbType.VarChar).Value = tb_Unit.Text;
					comm.CommandText = str;
					SqlDataReader dr1 = comm.ExecuteReader();
					while(dr1.Read())
					{
						tb_Unit.Text = dr1["SmallClassificationName"].ToString();
					}
					dr1.Close();
					comm.Parameters.Clear();

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
			ItemSearchControl1.UnitCostDistinction = "판매단가";
			hdItemNum.Value = ItemSearchControl1.ItemNum;
			
			
			

		}


		public void AppCost(SqlConnection con, SqlTransaction trans)
		{
			//품목을 선택했을때 품목의 적용단가를 가져오는 구문
			decimal StandardUnitCost=0;				
			decimal ApplyUnitCost=0;				

			
			//단가정보테이블에서 내가 선택한 품목과 거래처에 해당하는 품목의 단가구하는 쿼리
			string sqlApply = @"SELECT UCI_MT.StandardUnitCost FROM UCI_MT where UCI_MT.ItemNum = @ItemNum AND UCI_MT.BusinessRegistrationNum like @Num";

			SqlCommand Applycom = new SqlCommand();
			Applycom.Connection = con;
			Applycom.Transaction = trans;
			Applycom.CommandText = sqlApply;
			Applycom.Parameters.Add("@ItemNum",ItemSearchControl1.ItemNum);
			Applycom.Parameters.Add("@Num",CSC1.BusinessRegistrationNum);
			SqlDataReader dr = Applycom.ExecuteReader();
			Applycom.Parameters.Clear();
			while(dr.Read())
			{
				StandardUnitCost =  decimal.Parse(dr["StandardUnitCost"].ToString());
				ApplyUnitCost = decimal.Parse(dr["StandardUnitCost"].ToString());
			}
			dr.Close();
			if(CSC1.BusinessRegistrationNum.Trim() != "" && ItemSearchControl1.ItemNum.Trim() != "" && StandardUnitCost == 0)
				throw new Exception("품목 "+ItemSearchControl1.ItemNum+"의 단가정보가 존재하지 않습니다!");
			else
			{
				tb_ReceiveUnitCost.Text =  StandardUnitCost.ToString();
				tb_ApplyUnitCost.Text = ApplyUnitCost.ToString();
			}
			
		}

		/// <summary>
		/// 등록버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btRegister_ServerClick(object sender, System.EventArgs e)
		{
			if(CSC1.BusinessRegistrationNum.Trim() == "")
				RegisterStartupScript("","<script>alert('거래처를 선택해 주세요!');</script>");
			else if(!Compare(CSC1.Company, CSC1.BusinessRegistrationNum))
				RegisterStartupScript("","<script> alert('사업자번호가 거래처명과 다릅니다!');</script>");
			else if(ItemSearchControl1.ItemNum == "")
				RegisterStartupScript("","<script>alert('품목을 선택해 주세요!');</script>");
			else if(tb_ApplyUnitCost.Text.Trim() == "0" || tb_ApplyUnitCost.Text.Trim() == "")
				RegisterStartupScript("","<script>alert('적용단가를 입력해 주세요!');</script>");
			else if(wdcDeliveryRequestDate1.Text.Trim() == "")
				RegisterStartupScript("","<script>alert('납기요구일을 입력해 주세요!');</script>");
			else
			{
				string m_RequirePage = "ReceiveingRegistration";
				string m_User = Session["ID"].ToString();
				string m_RecodeIdx = lb_Index.Value;

				ArrayList InputList = new ArrayList();

				InputList.Add(ItemSearchControl1.ItemNum.ToString());
				InputList.Add(ItemSearchControl1.ItemDrawNum.ToString());
				InputList.Add(ItemSearchControl1.ItemName.ToString());
				InputList.Add(CSC1.Company);
				InputList.Add(CSC1.BusinessRegistrationNum);
				InputList.Add(tb_PropertyClassification.Text);
				InputList.Add(dlProductionRequestDivision.SelectedItem.Value);
				InputList.Add(wdcReceivingOrderDate.Text);
				InputList.Add(decimal.Parse(tb_ApplyUnitCost.Text));
				InputList.Add(decimal.Parse(tb_TotalReceiveingOrderQuantity.Text));
				InputList.Add(wdcDeliveryRequestDate1.Text);
				InputList.Add("0");
				InputList.Add("");
				InputList.Add("0");
				InputList.Add("");
				InputList.Add("0");
				InputList.Add("");
				InputList.Add("0");
				InputList.Add("");
				InputList.Add(decimal.Parse(tb_TotalReceiveingOrderQuantity.Text));
				InputList.Add(decimal.Parse(tb_TotalReceiveingOrderQuantity.Text)*decimal.Parse(tb_ApplyUnitCost.Text));
				InputList.Add(0);//출고수량
				InputList.Add(0);//합격수량
				InputList.Add(0);//미검수량
				InputList.Add(decimal.Parse(tb_TotalReceiveingOrderQuantity.Text));//납품잔량
				InputList.Add(tb_OrderNum.Text);
				InputList.Add(tb_DeliveryPlace.Text);
			
				Registration reg = new Registration(m_RequirePage,m_User,m_RecodeIdx, InputList);
				reg.ReceiveRegistration();
				//UltraWebGrid1.DataSource = reg.MainTableRegistration();
				//UltraWebGrid1.DataBind();
	
				UltraWebGrid1.DataSource = Search();
				UltraWebGrid1.DataBind();
				
				bt_Update.Enabled = false;
				bt_Delete.Enabled = false;
				btRegister.Visible = true;

				ItemSearchControl1.BusinessRegistrationNum = hdBusinessRegistrationNum.Value;
				ItemSearchControl1.UnitCostDistinction = "판매단가";
			}
		}

		private void LinkButton_Click(object sender, System.EventArgs e)
		{
		
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			if(lb_Index.Value == "0")
				Response.Write("<script>alert('품목을 선택해 주세요');</script>");
			else
			{
				Session["ItemNum"] = ItemSearchControl1.ItemNum;
				Page.RegisterClientScriptBlock("","<script>window.open('PopUp/StockQuantity.aspx','','width = 850, height= 500,scrollbars=yes,menubar=yes,status=no,toolbar=yes,center=yes');</script>");	
			}
				
		}

		/// <summary>
		/// 등록버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LinkButton3_Click(object sender, System.EventArgs e)
		{
//			if(CSC1.BusinessRegistrationNum.Trim() == "")
//				RegisterStartupScript("","<script>alert('거래처를 선택해 주세요!');</script>");
//			else if(!Compare(CSC1.Company, CSC1.BusinessRegistrationNum))
//				RegisterStartupScript("","<script> alert('사업자번호가 거래처명과 다릅니다!');</script>");
//			else if(ItemSearchControl1.ItemNum == "")
//				RegisterStartupScript("","<script>alert('품목을 선택해 주세요!');</script>");
//			else if(tb_ApplyUnitCost.Text.Trim() == "0" || tb_ApplyUnitCost.Text.Trim() == "")
//				RegisterStartupScript("","<script>alert('적용단가를 입력해 주세요!');</script>");
//			else if(wdcDeliveryRequestDate1.Text.Trim() == "")
//				RegisterStartupScript("","<script>alert('납기요구일을 입력해 주세요!');</script>");
//			else
//			{
//				string m_RequirePage = "ReceiveingRegistration";
//				string m_User = Session["ID"].ToString();
//				string m_RecodeIdx = lb_Index.Value;
//
//				ArrayList InputList = new ArrayList();
//
//				InputList.Add(ItemSearchControl1.ItemNum.ToString());
//				InputList.Add(ItemSearchControl1.ItemDrawNum.ToString());
//				InputList.Add(ItemSearchControl1.ItemName.ToString());
//				InputList.Add(CSC1.Company);
//				InputList.Add(CSC1.BusinessRegistrationNum);
//				InputList.Add(tb_PropertyClassification.Text);
//				InputList.Add(dlProductionRequestDivision.SelectedItem.Value);
//				InputList.Add(wdcReceivingOrderDate.Text);
//				InputList.Add(decimal.Parse(tb_ApplyUnitCost.Text));
//				InputList.Add(decimal.Parse(tb_TotalReceiveingOrderQuantity.Text));
//				InputList.Add(wdcDeliveryRequestDate1.Text);
//				InputList.Add("0");
//				InputList.Add("");
//				InputList.Add("0");
//				InputList.Add("");
//				InputList.Add("0");
//				InputList.Add("");
//				InputList.Add("0");
//				InputList.Add("");
//				InputList.Add(decimal.Parse(tb_TotalReceiveingOrderQuantity.Text));
//				InputList.Add(decimal.Parse(tb_TotalReceiveingOrderQuantity.Text)*decimal.Parse(tb_ApplyUnitCost.Text));
//				InputList.Add(0);//출고수량
//				InputList.Add(0);//합격수량
//				InputList.Add(0);//미검수량
//				InputList.Add(decimal.Parse(tb_TotalReceiveingOrderQuantity.Text));//납품잔량
//				InputList.Add(tb_OrderNum.Text);
//				InputList.Add(tb_DeliveryPlace.Text);
//			
//				Registration reg = new Registration(m_RequirePage,m_User,m_RecodeIdx, InputList);
//				reg.ReceiveRegistration();
//				//UltraWebGrid1.DataSource = reg.MainTableRegistration();
//                //UltraWebGrid1.DataBind();
//	
//				UltraWebGrid1.DataSource = Search();
//				UltraWebGrid1.DataBind();
//				
//				bt_Update.Enabled = false;
//				bt_Delete.Enabled = false;
//				btRegister.Visible = true;
//
//				ItemSearchControl1.BusinessRegistrationNum = hdBusinessRegistrationNum.Value;
//				ItemSearchControl1.UnitCostDistinction = "판매단가";
//			}
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
			//선택한 품목과 거래처에 해당하는 적용단가를 가지고오는 함추호출
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

		private void AppCost(SqlConnection con, SqlTransaction trans,string BusinessRegistrationNum)
		{
			//품목을 선택했을때 품목의 적용단가를 가져오는 구문
			decimal StandardUnitCost=0;				
			decimal ApplyUnitCost=0;				
			
			//단가정보테이블에서 내가 선택한 품목과 거래처에 해당하는 품목의 단가구하는 쿼리
			string sqlApply = @"SELECT UCI_MT.StandardUnitCost FROM UCI_MT where UCI_MT.RecodingState = 1 and UCI_MT.ItemNum = @ItemNum AND UCI_MT.BusinessRegistrationNum like @Num and UnitCostDistinction='판매단가'";

			SqlCommand Applycom = new SqlCommand();
			Applycom.Connection = con;
			Applycom.Transaction = trans;
			Applycom.CommandText = sqlApply;
			Applycom.Parameters.Add("@ItemNum",ItemSearchControl1.ItemNum);
			Applycom.Parameters.Add("@Num",BusinessRegistrationNum);

			SqlDataReader dr = Applycom.ExecuteReader();
			Applycom.Parameters.Clear();
			while(dr.Read())
			{
				StandardUnitCost =  decimal.Parse(dr["StandardUnitCost"].ToString());
				ApplyUnitCost = decimal.Parse(dr["StandardUnitCost"].ToString());
			}
			dr.Close();

			if(CSC1.BusinessRegistrationNum.Trim() != "" && ItemSearchControl1.ItemNum.Trim() != "" && StandardUnitCost == 0)
				throw new Exception("품목 "+ItemSearchControl1.ItemNum+"의 단가정보가 존재하지 않습니다!");
			else
			{
				tb_ReceiveUnitCost.Text =  StandardUnitCost.ToString();
				tb_ApplyUnitCost.Text = ApplyUnitCost.ToString();
			}
			//사용자 연락처 가져오기
			string str = "SELECT CompanyPersonInCharge,TelephoneNum From CI_MT where RecodingState = 1 and BusinessRegistrationNum = @BusinessRegistrationNum";
			Applycom.Parameters.Add("@BusinessRegistrationNum",CSC1.BusinessRegistrationNum);
			Applycom.CommandText = str;
			SqlDataReader dr1 = Applycom.ExecuteReader();
			while(dr1.Read())
			{
				tb_TelephoneNum.Text = dr1["TelephoneNum"].ToString();
				tb_CompanyPersonInCharge.Text = dr1["CompanyPersonInCharge"].ToString();
			}
			dr1.Close();

			
		}

	}
}
