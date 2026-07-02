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

namespace KIT_ERP.BasisInformation
{
	/// <summary>
	/// SellingUnitCostInfo에 대한 요약 설명입니다.
	/// </summary>
	public class SaleUnitCodeInfo : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lb_ItemNum;
		protected System.Web.UI.WebControls.Label lb_ItemDrawNum;
		protected System.Web.UI.WebControls.Label lb_ItemName;
		protected System.Web.UI.WebControls.Label lb_PropertyClassification;
		protected System.Web.UI.WebControls.TextBox tb_PropertyClassification;
		protected System.Web.UI.WebControls.Label lb_Unit;
		protected System.Web.UI.WebControls.TextBox tb_Unit;
		protected System.Web.UI.WebControls.Label lb_Standard;
		protected System.Web.UI.WebControls.TextBox tb_Standard;
		protected System.Web.UI.WebControls.Button bt_Clear;
		protected System.Web.UI.WebControls.Button bt_Update;
		protected System.Web.UI.WebControls.Button bt_Delete;
		protected System.Web.UI.WebControls.Button bt_Registration;
		protected Infragistics.WebUI.WebCombo.WebCombo WebCombo2;
		protected System.Web.UI.WebControls.Label lb_Company;
		protected System.Web.UI.WebControls.Label lb_StandardUnitCost;
		protected System.Web.UI.WebControls.Label lb_DiscountUnitCost;
		protected System.Web.UI.WebControls.Label lb_BeginDate;
		protected System.Web.UI.WebControls.Label lb_EndDate;
		protected System.Web.UI.WebControls.Label lb_ItemChoice;
		protected System.Web.UI.WebControls.Button bt_SellingUnitCost;
		protected System.Web.UI.WebControls.Button bt_RelationView;
		protected System.Web.UI.WebControls.Label lb_ItemIndex;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Index;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_ReferenceIndex;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid Ultrawebgrid2;
		protected System.Web.UI.WebControls.TextBox tb_StandardUnitCost;
		protected System.Web.UI.WebControls.TextBox tb_DiscountUnitCost;
		protected Infragistics.WebUI.WebCombo.WebCombo wcCompany;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcBeginDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcEndDate;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.LinkButton LinkButton2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_ReferenceTable;
		protected System.Web.UI.WebControls.TextBox tbUpdateReason;
		protected System.Web.UI.WebControls.Button btHistory;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.

//			if(Session["ID"] == null)
//			{	
//				Session.Abandon();
//				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
//			}
//			

			ItemSearchControl1.Commodity = true; //상품 바인딩
			ItemSearchControl1.RawMaterials = true; //원자재 바인딩	
//			ItemSearchControl1.Phantom = true; //팬텀 바인딩	
			ItemSearchControl1.HalfFinishedProducts =  true; //반제품 바인딩	
			ItemSearchControl1.Products = true; //제품 바인딩	
			ItemSearchControl1.IsDoPost = true;

			if(!Page.IsPostBack)
			{
				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 기준정보 > 판매단가정보';</script>");

				bt_Registration.Attributes.Add("onClick", "return OK('입력한 항목을 등록');");
				bt_Update.Attributes.Add("onClick", "return OK('선택한 항목을 수정');");
				bt_Delete.Attributes.Add("onClick", "return OK('선택한 항목을 삭제');");

				tb_StandardUnitCost.Attributes.Add("OnKeyUp", "OnKeyUp_Currency(this)");
				tb_StandardUnitCost.Attributes.Add("OnKeyDown", "OnKeyDown_Currency(this);");
				// focus를 얻으면 자동으로 select()
				tb_StandardUnitCost.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				// focus를 잃으면 다시 입력란을 체크
				tb_StandardUnitCost.Attributes.Add("OnBlur", "OnBlur_Cur(this);");
                			
				tb_DiscountUnitCost.Attributes.Add("OnKeyDown", "OnKeyDown_Currency(this);");
				// focus를 얻으면 자동으로 select()
				tb_DiscountUnitCost.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				// focus를 잃으면 다시 입력란을 체크
				tb_DiscountUnitCost.Attributes.Add("OnBlur", "OnBlur_Cur(this);");
				tb_DiscountUnitCost.Attributes.Add("OnKeyUp", "OnKeyUp_Currency(this)");
				bt_Clear.Attributes.Add("onClick", "ResetTextBox()");





			
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				conn.Open();
				
				// 거래처바인딩
				string strSQL = "SELECT CompanyName, PresidentName, BusinessRegistrationNum, CompanyInfoIndex FROM CI_MT where RecodingState = 1 and ReceiveingOrderCompany = 1 order by CompanyName";
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
			this.LinkButton2.Click += new System.EventHandler(this.LinkButton2_Click);
			this.bt_SellingUnitCost.Click += new System.EventHandler(this.bt_SellingUnitCost_Click);
			this.btHistory.Click += new System.EventHandler(this.btHistory_Click);
			this.bt_RelationView.Click += new System.EventHandler(this.bt_RelationView_Click);
			this.bt_Clear.Click += new System.EventHandler(this.bt_Clear_Click);
			this.bt_Update.Click += new System.EventHandler(this.bt_Update_Click);
			this.bt_Delete.Click += new System.EventHandler(this.bt_Delete_Click);
			this.bt_Registration.Click += new System.EventHandler(this.bt_Registration_Click);
			this.Ultrawebgrid2.DblClick += new Infragistics.WebUI.UltraWebGrid.ClickEventHandler(this.Ultrawebgrid2_DblClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		/// <summary>
		/// 판매단가보기 버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_SellingUnitCost_Click(object sender, System.EventArgs e)
		{
			
//			if(tb_ItemNum.Text =="")
//			{
//				RegisterStartupScript("","<script>alert('품목을 선택해 주세요');</script>");
//			}
//			else
//			{
//				this.DataView();
//			}

			if(ItemSearchControl1.ItemNum.ToString() == "")
			{
				RegisterStartupScript("","<script>alert('품목을 선택해 주세요');</script>");
				
			}
			else
			{
				this.DataView();
			}
			
			
		}

		/// <summary>
		/// 등록버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Registration_Click(object sender, System.EventArgs e)
		{
			if(ItemSearchControl1.ItemNum.ToString() =="" || wcCompany.DisplayValue == null || wcCompany.DisplayValue == "" || tb_StandardUnitCost.Text == "" || wdcBeginDate.Text == "")
			{
				RegisterStartupScript("","<script>alert('필수항목을 입력해 주세요');</script>");
			}
			else
			{
				if(Page.IsValid == true)
				{
					string m_RequirePage = "SaleUnitCodeInfo";
					string m_RequireAction = "Registration";
					string m_TableName = "UCI_MT";
					int m_Idx = (int)PageName.SaleUnitCodeInfo;
					string m_User = Session["ID"].ToString();
					string m_RecodeIdx = lb_Index.Value;

					ArrayList InputList = new ArrayList();

					InputList.Add(ItemSearchControl1.ItemNum.ToString());
					InputList.Add("판매단가");
					InputList.Add(wcCompany.DataValue.ToString());//사업자등록번호
					InputList.Add("14009999");		//시작공정
					InputList.Add("14009999");		//종료공정
					InputList.Add("100");			//발주비율
					InputList.Add(decimal.Parse(tb_StandardUnitCost.Text));
					if(tb_DiscountUnitCost.Text.Trim() == "")
						InputList.Add("0");
					else
						InputList.Add(decimal.Parse(tb_DiscountUnitCost.Text));
					if(wdcBeginDate.Text.Trim() =="")
						InputList.Add(DateTime.Now.ToShortDateString());
					else
						InputList.Add(wdcBeginDate.Text);
					if(wdcEndDate.Text.Trim() =="")
						InputList.Add("2076-06-06");
					else
						InputList.Add(wdcEndDate.Text);

					Session["Reason"] = "신규등록";

					MasterInfoRecordRUD rud = new MasterInfoRecordRUD(m_RequirePage, m_RequireAction, m_TableName, m_Idx, m_User, InputList,"낱개입력");
					rud.ActivateRUD();
				}
				this.DataView();
			}


		}

		/// <summary>
		/// 수정버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Update_Click(object sender, System.EventArgs e)
		{
			
			if(ItemSearchControl1.ItemNum.ToString() =="" || wcCompany.DisplayValue == null || wcCompany.DisplayValue == "" || tb_StandardUnitCost.Text == "" || wdcBeginDate.Text == "")
			{
				RegisterStartupScript("","<script>alert('필수항목을 입력해 주세요');</script>");
			}
			else if(tbUpdateReason.Text.Trim() =="")
			{
				RegisterStartupScript("","<script>alert('변경사유를 입력해 주세요');</script>");
			}
			else
			{
				if(Page.IsValid == true)
				{
					string m_RequirePage = "SaleUnitCodeInfo";
					string m_RequireAction = "Update";
					string m_TableName = "UCI_MT";
					int m_Idx = (int)PageName.SaleUnitCodeInfo;
					string m_User = Session["ID"].ToString();
					string m_RecodeIdx = lb_Index.Value;

					ArrayList InputList = new ArrayList();

					InputList.Add(ItemSearchControl1.ItemNum.ToString());
					InputList.Add("판매단가");
					InputList.Add(wcCompany.DataValue.ToString());//사업자등록번호
					InputList.Add("14009999");
					InputList.Add("14009999");
					InputList.Add("100");
					InputList.Add(decimal.Parse(tb_StandardUnitCost.Text));
					if(tb_DiscountUnitCost.Text.Trim() == "")
						InputList.Add("0");
					else
						InputList.Add(decimal.Parse(tb_DiscountUnitCost.Text));
					if(wdcBeginDate.Text.Trim() =="")
						InputList.Add(DateTime.Now.ToShortDateString());
					else
						InputList.Add(wdcBeginDate.Text);
					if(wdcEndDate.Text.Trim() =="")
						InputList.Add("2076-06-06");
					else
						InputList.Add(wdcEndDate.Text);
					InputList.Add(lb_Index.Value);

					Session["Reason"] = tbUpdateReason.Text.Trim();

					MasterInfoRecordRUD rud = new MasterInfoRecordRUD(m_RequirePage, m_RequireAction, m_TableName, m_Idx, m_User, InputList,"낱개입력");
					rud.ActivateRUD();
				}
				this.DataView();
			}
		}

		private void DataView()
		{
			View view = new View(ItemSearchControl1.ItemNum.ToString(), "SaleUnitCodeInfo");
			UltraWebGrid1.DataSource = view.ItemView();
			UltraWebGrid1.DataBind();
			
		}

		/// <summary>
		/// 삭제버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Delete_Click(object sender, System.EventArgs e)
		{
			
			if(Page.IsValid == true)
			{
				string m_RequirePage = "SaleUnitCodeInfo";
				string m_RequireAction = "Delete";
				string m_TableName = "UCI_MT";
				int m_Idx = (int)PageName.SaleUnitCodeInfo;
				string m_User = Session["ID"].ToString();
				string m_RecodeIdx = lb_Index.Value;
				
				ArrayList InputList = new ArrayList();

				InputList.Add(ItemSearchControl1.ItemNum.ToString());
				InputList.Add("판매단가");
				if(wcCompany.DisplayValue == null || wcCompany.DisplayValue == "")
					InputList.Add(0);//사업자등록번호
				else
					InputList.Add(wcCompany.DataValue.ToString());//사업자등록번호
				InputList.Add("14009999");
				InputList.Add("14009999");
				InputList.Add("100");
				InputList.Add(decimal.Parse(tb_StandardUnitCost.Text));
				if(tb_DiscountUnitCost.Text.Trim() == "")
					InputList.Add("0");
				else
					InputList.Add(decimal.Parse(tb_DiscountUnitCost.Text));
				if(wdcBeginDate.Text.Trim() =="")
					InputList.Add(DateTime.Now.ToShortDateString());
				else
					InputList.Add(wdcBeginDate.Text);
				if(wdcEndDate.Text.Trim() =="")
					InputList.Add("2076-06-06");
				else
					InputList.Add(wdcEndDate.Text);
				InputList.Add(lb_Index.Value);
				Session["Reason"] = "삭제";

				MasterInfoRecordRUD rud = new MasterInfoRecordRUD(m_RequirePage, m_RequireAction, m_TableName, m_Idx, m_User, InputList,"낱개입력");
				rud.ActivateRUD();
			}

			wcCompany.DataValue = "";
			wcCompany.DisplayValue = "";
			tb_StandardUnitCost.Text = "";
			tb_DiscountUnitCost.Text = "0";
			wdcBeginDate.Value = "";
			wdcEndDate.Value = "";

			lb_Index.Value = "0";

			this.DataView();
			
		}

		/// <summary>
		/// 초기화버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Clear_Click(object sender, System.EventArgs e)
		{
			
			tb_PropertyClassification.Text = "";
			tb_Standard.Text ="";
			tb_Unit.Text = "";
			lb_ItemIndex.Text ="";
			wcCompany.DataValue = "";
			wcCompany.DisplayValue = "";
			tb_StandardUnitCost.Text = "";
			tb_DiscountUnitCost.Text = "0";
			wdcBeginDate.Value = "";
			wdcEndDate.Value = "";
			tbUpdateReason.Text = "";

			lb_Index.Value = "0";

			View view = new View("", "SaleUnitCodeInfo");
			UltraWebGrid1.DataSource = view.ItemView();
			UltraWebGrid1.DataBind();

		
		}

		/// <summary>
		/// 참고보기버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_RelationView_Click(object sender, System.EventArgs e)
		{
			if(lb_Index.Value == "0")
			{
				RegisterStartupScript("","<script>alert('항목을 선택후 버튼을 눌러주세요');</script>");
			}
			else
			{

				string m_TableName = "판매단가정보";
				int m_RecodeIdx = int.Parse(lb_Index.Value);
			

				Reference rf = new Reference(m_TableName, m_RecodeIdx);
				Ultrawebgrid2.DataSource = rf.reference();
				Ultrawebgrid2.DataBind();
			}
		}

		/// <summary>
		/// 팝업버튼클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
//		private void pop_Click(object sender, System.EventArgs e)
//		{
//			// 품목명찾기 팝업창 띄우기
//			string txt = Textbox1.Text ;
//			string formID = "SellingUnitCostInfo" ;
//			string txtID = "ItemIndex";
//			
//			RegisterStartupScript("","<script>window.open('./Popup/Item.aspx?formID=" + formID + "&txtID=" + txtID +  "&txt=" + txt + "','Item'," + "'width=800" + "," + "height=400px');</script>");
//		}

		/// <summary>
		/// 참고보기 그리드 더블 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Ultrawebgrid2_DblClick(object sender, Infragistics.WebUI.UltraWebGrid.ClickEventArgs e)
		{
			string table = lb_ReferenceTable.Value;
			string index = lb_ReferenceIndex.Value;

			if(table == "생산품질")
				table = "T_Product";
			else if(table =="개발실")
				table = "T_Development";
			else
				table = "T_Materials";
			
			//RegisterStartupScript("","<script>window.open('./Popup/View.aspx?table=" + table + "&index=" + index + "','Reference'," + "'width=510" + "," + "height=435px');</script>");
			RegisterStartupScript("","<script>window.open('./Popup/View1.aspx?table=" + table + "&index=" + index + "','Reference'," + "'width=818" + "," + "height=610px');</script>");
		}

		

		/// <summary>
		/// 어깨창 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LinkButton1_Click(object sender, System.EventArgs e)
		{
			RegisterStartupScript("","<script>window.open('./Popup/UnitCost.aspx','UnitCost'," + "'width=830" + "," + "height=500px');</script>");
		}

		private void LinkButton2_Click(object sender, System.EventArgs e)
		{
			
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select ItemNum, ItemDrawNum,ItemName, PropertyClassification, Unit, Standard, ChargePerson From II_MT Where ItemNum = @num and RecodingState = 1";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = ItemSearchControl1.ItemNum;
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
//				tb_ItemNum.Text = dr["ItemNum"].ToString();
//				tb_ItemDrawNum.Text = dr["ItemDrawNum"].ToString();
//				tb_ItemName.Text = dr["ItemName"].ToString();
				tb_PropertyClassification.Text = dr["PropertyClassification"].ToString();
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

		private void btHistory_Click(object sender, System.EventArgs e)
		{
			RegisterStartupScript("","<script language='JavaScript'>window.open('UpdateUnitCostHistory.aspx','UpdateUnitCostHistory','width=830,height=600px,resizable=yes');</script>");
		}

		

		
	}
}
