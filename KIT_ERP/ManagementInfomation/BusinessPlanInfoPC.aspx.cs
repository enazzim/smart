using System;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;

namespace KIT_ERP.ManagementInfomation
{
	/// <summary>
	/// BusinessPlanInfoPC에 대한 요약 설명입니다.
	/// </summary>
	public class BusinessPlanInfoPC : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		protected System.Web.UI.WebControls.TextBox tb_Unit;
		protected System.Web.UI.WebControls.TextBox tb_Standard;
		protected System.Web.UI.WebControls.Label lb_ItemIndex;
		protected System.Web.UI.WebControls.LinkButton LinkButton2;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected Infragistics.WebUI.WebCombo.WebCombo wcCompany;
		protected System.Web.UI.WebControls.Button bt_Clear;
		protected System.Web.UI.WebControls.Button bt_Update;
		protected System.Web.UI.WebControls.Button bt_Delete;
		protected System.Web.UI.WebControls.Button bt_Registration;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid Ultrawebgrid2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Index;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_ReferenceIndex;
		protected System.Web.UI.WebControls.Button bt_BusinessPlanView;
		protected System.Web.UI.WebControls.DropDownList dl_Year;
		protected System.Web.UI.WebControls.DropDownList dl_Month;
		protected System.Web.UI.WebControls.DropDownList dl_Day;
		protected System.Web.UI.WebControls.TextBox tb_PlanQuantity;
		protected System.Web.UI.WebControls.TextBox tb_SaleUnitCost;
		protected System.Web.UI.WebControls.TextBox tb_PlanTotalCost;
		protected System.Web.UI.WebControls.Button bt_Reference;
		protected System.Web.UI.WebControls.Label lb_Idx;
		protected System.Web.UI.WebControls.LinkButton ComSelect;
		protected System.Web.UI.HtmlControls.HtmlInputHidden c;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_ReferenceTable;
		protected System.Web.UI.WebControls.TextBox tb_PropertyClassification;

		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			ItemSearchControl1.Commodity = true; //상품 바인딩			
			ItemSearchControl1.Products = true; //제품 바인딩	

			ItemSearchControl1.IsDoPost = true;

			if(!Page.IsPostBack)
			{
				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 기준정보 > 사업계획정보';</script>");

				bt_Registration.Attributes.Add("onClick", "return OK('입력한 항목을 등록');");
				bt_Update.Attributes.Add("onClick", "return OK('선택한 항목을 수정');");
				bt_Delete.Attributes.Add("onClick", "return OK('선택한 항목을 삭제');");




				
				tb_PlanQuantity.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this);");

				// onkeyup 이벤트가 발생하면 수량입력과동시에 금액을 계산하는 자바스크립트 함수 매핑
				tb_PlanQuantity.Attributes.Add("OnKeyUp", "return Process()");
			
				// focus를 얻으면 자동으로 select()
				tb_PlanQuantity.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
			
				// focus를 잃으면 다시 입력란을 체크
				tb_PlanQuantity.Attributes.Add("OnBlur", "OnBlur_Float(this);");



				tb_SaleUnitCost.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this);");

				// onkeyup 이벤트가 발생하면 수량입력과동시에 금액을 계산하는 자바스크립트 함수 매핑
				tb_SaleUnitCost.Attributes.Add("OnKeyUp", "return Process()");
			
				// focus를 얻으면 자동으로 select()
				tb_SaleUnitCost.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
			
				// focus를 잃으면 다시 입력란을 체크
				tb_SaleUnitCost.Attributes.Add("OnBlur", "OnBlur_Float(this);");

				bt_Clear.Attributes.Add("onClick", "ResetTextBox()");
			

				page_Load();
			}
		}

		private void page_Load()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			// 거래처바인딩
			string strSQL = "SELECT CompanyName, PresidentName, BusinessRegistrationNum, CompanyInfoIndex FROM CI_MT where RecodingState = 1 and ReceiveingOrderCompany = 1";
			SqlCommand commCom = new SqlCommand(strSQL,conn);
			SqlDataAdapter daCom = new SqlDataAdapter(commCom) ;
			DataSet dsCom = new DataSet() ;
			daCom.Fill(dsCom);

			wcCompany.DataSource = dsCom;
			wcCompany.DataTextField = dsCom.Tables[0].Columns[0].ToString();
			wcCompany.DataValueField = dsCom.Tables[0].Columns[2].ToString();
			wcCompany.DataBind();

			conn.Close();

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
			this.bt_BusinessPlanView.Click += new System.EventHandler(this.bt_BusinessPlanView_Click);
			this.LinkButton2.Click += new System.EventHandler(this.LinkButton2_Click);
			this.bt_Reference.Click += new System.EventHandler(this.bt_Reference_Click);
			this.ComSelect.Click += new System.EventHandler(this.ComSelect_Click);
			this.bt_Clear.Click += new System.EventHandler(this.bt_Clear_Click);
			this.bt_Update.Click += new System.EventHandler(this.bt_Update_Click);
			this.bt_Delete.Click += new System.EventHandler(this.bt_Delete_Click);
			this.bt_Registration.Click += new System.EventHandler(this.bt_Registration_Click);
			this.Ultrawebgrid2.DblClick += new Infragistics.WebUI.UltraWebGrid.ClickEventHandler(this.Ultrawebgrid2_DblClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void bt_BusinessPlanView_Click(object sender, System.EventArgs e)
		{
			if(ItemSearchControl1.ItemNum.ToString() == "")
			{
				RegisterStartupScript("","<script language=javascript>alert('품목을 먼저 선택해 주세요');</script>");
			}
			else
			{
				//아래쪽 Clear
				clear();

				KIT_ERP.BasisInformation.View view = new KIT_ERP.BasisInformation.View(ItemSearchControl1.ItemNum.ToString(), "BusinessPlanInfo");
				UltraWebGrid1.DataSource = view.ItemView();
				UltraWebGrid1.DataBind();
			}
		}

		private void bt_Reference_Click(object sender, System.EventArgs e)
		{
			if(lb_Index.Value == "0")
			{

				RegisterStartupScript("","<script language=javascript>alert('항목을 선택후 버튼을 눌러주세요');</script>");
			}
			else
			{
				string m_TableName = "사업계획정보";
				int m_RecodeIdx = int.Parse(lb_Index.Value);
			

				KIT_ERP.BasisInformation.Reference rf = new KIT_ERP.BasisInformation.Reference(m_TableName, m_RecodeIdx);
				Ultrawebgrid2.DataSource = rf.reference();
				Ultrawebgrid2.DataBind();
			}
		}

		private void bt_Clear_Click(object sender, System.EventArgs e)
		{
			tb_PropertyClassification.Text = "";
			tb_Unit.Text = "";
			tb_Standard.Text = "";
			lb_ItemIndex.Text = "0";

			KIT_ERP.BasisInformation.View view = new KIT_ERP.BasisInformation.View("", "BusinessPlanInfo");
			UltraWebGrid1.DataSource = view.ItemView();
			UltraWebGrid1.DataBind();

			

			//아래쪽 Clear
			clear();
		}

		private void bt_Update_Click(object sender, System.EventArgs e)
		{
			string m_RequirePage = "BusinessPlanInfo";
			string m_RequireAction = "Update";
			string m_TableName = "BPI_MT";
			int m_Idx = (int)PageName.BusinessPlanInfo;
			
			
			
			ArrayList InputList = new ArrayList();

			InputList.Add(ItemSearchControl1.ItemNum.ToString());
			InputList.Add(dl_Year.SelectedItem.Value);
			InputList.Add(dl_Month.SelectedItem.Value);
			InputList.Add(dl_Day.SelectedItem.Value);
			InputList.Add(tb_PlanQuantity.Text);
			InputList.Add(tb_SaleUnitCost.Text);
			InputList.Add(tb_PlanTotalCost.Text);
			
			if(Convert.IsDBNull(wcCompany.DataValue))
				InputList.Add(0);//사업자등록번호
			else
				InputList.Add(wcCompany.DataValue.ToString());//사업자등록번호
			
			InputList.Add(lb_Index.Value);


			KIT_ERP.BasisInformation.MasterInfoRecordRUD rud = new KIT_ERP.BasisInformation.MasterInfoRecordRUD(m_RequirePage, m_RequireAction, m_TableName, m_Idx, Session["ID"].ToString(), InputList,"낱개입력");
			rud.ActivateRUD();
			
			this.databind();
		}

		/// <summary>
		/// 웹그리드에 바인딩해주는 함수
		/// </summary>
		private void databind()
		{
			string m_itemnum = ItemSearchControl1.ItemNum.ToString();
			string m_table = "BPI_MT";
			string m_page = "BusinessPlanInfo";

			KIT_ERP.BasisInformation.View view = new KIT_ERP.BasisInformation.View(m_itemnum, m_table, m_page);
			UltraWebGrid1.DataSource = view.ItemView();
			UltraWebGrid1.DataBind();
		}

		private void clear()
		{
			dl_Year.SelectedIndex = 0;
			dl_Month.SelectedIndex =0;
			dl_Day.SelectedIndex = 0;
			tb_PlanQuantity.Text = "0";
			tb_SaleUnitCost.Text = "0";
			tb_PlanTotalCost.Text = "0";
			lb_Index.Value = "0";

			wcCompany.DataValue = "";
			wcCompany.DisplayValue = "";
		}

		private void bt_Delete_Click(object sender, System.EventArgs e)
		{
			string m_RequirePage = "BusinessPlanInfo";
			string m_RequireAction = "Delete";
			string m_TableName = "BPI_MT";
			int m_Idx = (int)PageName.BusinessPlanInfo;
			
			ArrayList InputList = new ArrayList();

			InputList.Add(ItemSearchControl1.ItemNum.ToString());
			InputList.Add(dl_Year.SelectedItem.Value);
			InputList.Add(dl_Month.SelectedItem.Value);
			InputList.Add(dl_Day.SelectedItem.Value);
			InputList.Add(tb_PlanQuantity.Text);
			InputList.Add(tb_SaleUnitCost.Text);
			InputList.Add(tb_PlanTotalCost.Text);
			if(Convert.IsDBNull(wcCompany.DataValue))
				InputList.Add(0);//사업자등록번호
			else
				InputList.Add(wcCompany.DataValue.ToString());//사업자등록번호
			InputList.Add(lb_Index.Value);
			InputList.Add(lb_Index.Value);


			KIT_ERP.BasisInformation.MasterInfoRecordRUD rud = new KIT_ERP.BasisInformation.MasterInfoRecordRUD(m_RequirePage, m_RequireAction, m_TableName, m_Idx, Session["ID"].ToString(), InputList,"낱개입력");
			rud.ActivateRUD();
			
			this.databind();
			clear();
		}

		private void bt_Registration_Click(object sender, System.EventArgs e)
		{
			ArrayList InputList = new ArrayList();

			InputList.Add(ItemSearchControl1.ItemNum.ToString());
			InputList.Add(dl_Year.SelectedItem.Value);
			InputList.Add(dl_Month.SelectedItem.Value);
			InputList.Add(dl_Day.SelectedItem.Value);
			InputList.Add(tb_PlanQuantity.Text);
			InputList.Add(tb_SaleUnitCost.Text);
			InputList.Add(tb_PlanTotalCost.Text);
			//InputList.Add(dl_Company.SelectedItem.Value);
			if(wcCompany.DisplayValue == null || wcCompany.DisplayValue == "")
				InputList.Add(0);//사업자등록번호
			else
				InputList.Add(wcCompany.DataValue.ToString());//사업자등록번호



			KIT_ERP.BasisInformation.MasterInfoRecordRUD rud = new  KIT_ERP.BasisInformation.MasterInfoRecordRUD("BusinessPlanInfo","Registration","BPI_MT",(int)PageName.BusinessPlanInfo,Session["ID"].ToString(),InputList,"낱개입력");
			rud.ActivateRUD();
				
			this.databind();
		}

		/// <summary>
		/// 거래처 선택시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ComSelect_Click(object sender, System.EventArgs e)
		{
		
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string str = "Select StandardUnitCost from UCI_MT Where BusinessRegistrationNum = @num and ItemNum = @item";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = wcCompany.DataValue.ToString();
			comm.Parameters.Add("@item",SqlDbType.VarChar).Value = ItemSearchControl1.ItemNum.ToString();

			SqlDataReader dr = comm.ExecuteReader();

			while(dr.Read())
			{
				tb_SaleUnitCost.Text = dr["StandardUnitCost"].ToString();
			}
			conn.Close();
		}

		/// <summary>
		/// 어깨창 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LinkButton1_Click(object sender, System.EventArgs e)
		{
			RegisterStartupScript("","<script language=javascript>window.open('../BasisInformation/Popup/BusinessPlan.aspx','BusinessPlan'," + "'width=850" + "," + "height=500px');</script>");
		}

		private void LinkButton2_Click(object sender, System.EventArgs e)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select ItemNum, ItemDrawNum,ItemName, PropertyClassification, Unit, Standard, ChargePerson From II_MT Where ItemNum = @num and (PropertyClassification = '제품' or PropertyClassification = '상품') and RecodingState = 1";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = ItemSearchControl1.ItemNum;
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				//				tb_ItemNum.Text = dr["ItemNum"].ToString();
				//				tb_ItemDrawNum.Text = dr["ItemDrawNum"].ToString();
				//				tb_ItemName.Text = dr["ItemName"].ToString();
				//tb_PropertyClassification.Text = dr["PropertyClassification"].ToString();
				tb_Unit.Text = dr["Unit"].ToString();
				tb_Standard.Text = dr["Standard"].ToString();
				
			}
			dr.Close();
			
			//단위 입력
			string str1 = "Select SmallClassificationName from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and SmallClassificationCode = @code" ;
			SqlCommand comm1 = new SqlCommand(str1,conn);
			comm1.Parameters.Add("@code",SqlDbType.VarChar).Value = tb_Unit.Text;
			SqlDataReader dr1 = comm1.ExecuteReader();
			while(dr1.Read())
			{
				tb_Unit.Text = dr1["SmallClassificationName"].ToString();
			}
			dr1.Close();
			conn.Close();
		}

		private void Ultrawebgrid2_DblClick(object sender, Infragistics.WebUI.UltraWebGrid.ClickEventArgs e)
		{
			string table = lb_ReferenceTable.Value;
			string index = lb_ReferenceIndex.Value;

			if(table == "일반자료실")
				table = "CGD_T";
			else if(table =="기술자료실")
				table = "CTD_T";
			else
				table = "CCS_T";
			
			RegisterStartupScript("","<script>window.open('../BasisInformation/Popup/View.aspx?table=" + table + "&index=" + index + "','Reference'," + "'width=510" + "," + "height=435px');</script>");
		
		}
	}
}
