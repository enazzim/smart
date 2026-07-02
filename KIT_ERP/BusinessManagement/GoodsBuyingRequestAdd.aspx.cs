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
	/// CollectMoneyRegistration에 대한 요약 설명입니다.
	/// </summary>
	public class CollectMoneyRegistration : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label19;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.Label Label15;
		protected System.Web.UI.WebControls.Label Label16;
		protected System.Web.UI.WebControls.Label Label17;
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.Label Label18;
		protected System.Web.UI.WebControls.Label Label14;
		protected System.Web.UI.WebControls.TextBox tb_PropertyClassification;
		protected System.Web.UI.WebControls.TextBox tb_DeliveryDemandQuantity1;
		protected System.Web.UI.WebControls.TextBox tb_DeliveryDemandQuantity2;
		protected System.Web.UI.WebControls.TextBox tb_DeliveryDemandQuantity3;
		protected System.Web.UI.WebControls.TextBox tb_DeliveryDemandQuantity4;
		protected System.Web.UI.WebControls.TextBox tb_DeliveryDemandQuantity5;
		protected System.Web.UI.WebControls.TextBox tb_TotalDeliveryDemandQuantity;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcDeliveryDemandDate1;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcDeliveryDemandDate2;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcDeliveryDemandDate3;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcDeliveryDemandDate4;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcDeliveryDemandDate5;
		protected System.Web.UI.WebControls.Button bt_Clear;
		protected System.Web.UI.WebControls.Button bt_Update;
		protected System.Web.UI.WebControls.Button bt_Delete;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Index;
		protected System.Web.UI.HtmlControls.HtmlInputHidden UnitCost;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		protected System.Web.UI.WebControls.DropDownList dlBuyingRequestSource;
		protected System.Web.UI.WebControls.LinkButton LinkButton2;
		protected System.Web.UI.HtmlControls.HtmlInputButton btAdd;
		protected System.Web.UI.WebControls.TextBox txtItemState;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
	
	
		private void Page_Load(object sender, System.EventArgs e)
		{



			ItemSearchControl1.Commodity = true; //상품 바인딩
			ItemSearchControl1.IsDoPost = true; //상품 바인딩


			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}
			if(!Page.IsPostBack)
			{
				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 영업관리 > 상품구매의뢰추가';</script>");

				//bt_Add.Attributes.Add("onClick", "return OK('입력한 품목을 추가');");
				//bt_Update.Attributes.Add("onClick", "return OK('선택한 품목을 수정');");
				bt_Delete.Attributes.Add("onClick", "return OK('선택한 품목을 삭제');");
				bt_Clear.Attributes.Add("onClick", "ResetTextBox();");

				tb_DeliveryDemandQuantity1.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this);");
				// onkeyup 이벤트가 발생하면 수량을 계산하는 자바스크립트 함수 매핑
				tb_DeliveryDemandQuantity1.Attributes.Add("OnKeyUp", "Process();OnKeyUp_Currency(this);");
				// focus를 얻으면 자동으로 select()
				tb_DeliveryDemandQuantity1.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				// focus를 잃으면 다시 입력란을 체크
				tb_DeliveryDemandQuantity1.Attributes.Add("OnBlur", "OnBlur_Cur(this);");

				tb_DeliveryDemandQuantity2.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this);");
				tb_DeliveryDemandQuantity2.Attributes.Add("OnKeyUp", "Process();OnKeyUp_Currency(this);");
				tb_DeliveryDemandQuantity2.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				tb_DeliveryDemandQuantity2.Attributes.Add("OnBlur", "OnBlur_Cur(this);");

				tb_DeliveryDemandQuantity3.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this);");
				tb_DeliveryDemandQuantity3.Attributes.Add("OnKeyUp", "Process();OnKeyUp_Currency(this);");
				tb_DeliveryDemandQuantity3.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				tb_DeliveryDemandQuantity3.Attributes.Add("OnBlur", "OnBlur_Cur(this);");

				tb_DeliveryDemandQuantity4.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this);");
				tb_DeliveryDemandQuantity4.Attributes.Add("OnKeyUp", "Process();OnKeyUp_Currency(this);");
				tb_DeliveryDemandQuantity4.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				tb_DeliveryDemandQuantity4.Attributes.Add("OnBlur", "OnBlur_Cur(this);");

				tb_DeliveryDemandQuantity5.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this);");
				tb_DeliveryDemandQuantity5.Attributes.Add("OnKeyUp", "Process();OnKeyUp_Currency(this);");
				tb_DeliveryDemandQuantity5.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				tb_DeliveryDemandQuantity5.Attributes.Add("OnBlur", "OnBlur_Cur(this);");
				bt_Clear.Attributes.Add("onClick", "ResetTextBox()");


				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				conn.Open();

				


				//	*************************************************
				//	**  구매의뢰원천 드롭다운리스트... 데이타바인딩... **
				//	*************************************************
				//코드분류표에서 대분류명이 구매의뢰원천인 소분류명을 가지고 옴.
				string str_Source = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and SmallClassificationName != '정상' and RecodingState = 1 and LargeClassificationCode = '0330' order by SmallClassificationName";
				SqlCommand comm_Source = new SqlCommand(str_Source,conn);
				SqlDataAdapter da_Source = new SqlDataAdapter(comm_Source) ;
				DataSet ds_Source = new DataSet() ;
				da_Source.Fill(ds_Source);
				
				dlBuyingRequestSource.DataSource = ds_Source;
				dlBuyingRequestSource.DataTextField = ds_Source.Tables[0].Columns[0].ToString();
				dlBuyingRequestSource.DataValueField = ds_Source.Tables[0].Columns[1].ToString();
				dlBuyingRequestSource.DataBind();
				//dlBuyingRequestSource.Items.Insert(0, "-선택-") ;
				//dlBuyingRequestSource.Items[0].Value = "";

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
			this.LinkButton2.Click += new System.EventHandler(this.bt_Add_Click);
			this.LinkButton1.Click += new System.EventHandler(this.LinkButton1_Click);
			this.bt_Clear.Click += new System.EventHandler(this.bt_Clear_Click);
			this.bt_Update.Click += new System.EventHandler(this.bt_Update_Click);
			this.bt_Delete.Click += new System.EventHandler(this.bt_Delete_Click);			
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		/// <summary>
		/// 추가버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Add_Click(object sender, System.EventArgs e)
		{
			if(dlBuyingRequestSource.SelectedItem.Value == null || dlBuyingRequestSource.SelectedItem.Value == "")
				RegisterStartupScript("","<script>alert('의뢰원천을 선택해 주세요!');</script>");
			else if(ItemSearchControl1.ItemName.ToString() == null || ItemSearchControl1.ItemName.ToString() == "")
				RegisterStartupScript("","<script>alert('품목을 선택해 주세요!');</script>");
			else
			{
				string m_PageName = "GoodsBuyingRequestAdd";
				string m_User = Session["ID"].ToString();
				string m_Index = lb_Index.Value;
				decimal TotalCost = 0;

				TotalCost = (decimal.Parse(tb_DeliveryDemandQuantity1.Text) +
					decimal.Parse(tb_DeliveryDemandQuantity2.Text)+ decimal.Parse(tb_DeliveryDemandQuantity3.Text)+
					decimal.Parse(tb_DeliveryDemandQuantity4.Text)+ decimal.Parse(tb_DeliveryDemandQuantity5.Text)) * decimal.Parse(UnitCost.Value);
            


				ArrayList m_InputList = new ArrayList();

				m_InputList.Add(ItemSearchControl1.ItemNum.ToString());
				m_InputList.Add(ItemSearchControl1.ItemDrawNum.ToString());
				m_InputList.Add(ItemSearchControl1.ItemName.ToString());
//				m_InputList.Add(tb_ItemNum.Text);
//				m_InputList.Add(tb_ItemDrawNum.Text);
//				m_InputList.Add(wcItemName.DisplayValue);
				m_InputList.Add(tb_PropertyClassification.Text);
				m_InputList.Add(dlBuyingRequestSource.SelectedItem.Value);
				m_InputList.Add(dlBuyingRequestSource.SelectedItem.Text);
				m_InputList.Add(tb_DeliveryDemandQuantity1.Text);
				m_InputList.Add(wdcDeliveryDemandDate1.Text);
				m_InputList.Add(tb_DeliveryDemandQuantity2.Text);
				m_InputList.Add(wdcDeliveryDemandDate2.Text);
				m_InputList.Add(tb_DeliveryDemandQuantity3.Text);
				m_InputList.Add(wdcDeliveryDemandDate3.Text);
				m_InputList.Add(tb_DeliveryDemandQuantity4.Text);
				m_InputList.Add(wdcDeliveryDemandDate4.Text);
				m_InputList.Add(tb_DeliveryDemandQuantity5.Text);
				m_InputList.Add(wdcDeliveryDemandDate5.Text);
				m_InputList.Add(tb_TotalDeliveryDemandQuantity.Text);
				m_InputList.Add(UnitCost.Value);
				m_InputList.Add(TotalCost);


				Registration reg = new Registration(m_PageName,m_User,m_Index,m_InputList);
				UltraWebGrid1.DataSource = reg.MainTableRegistration();
				UltraWebGrid1.DataBind();
				FindIndex();
			}
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
		/// 수정버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Update_Click(object sender, System.EventArgs e)
		{
			if(dlBuyingRequestSource.SelectedItem.Value == "")
				RegisterStartupScript("","<script>alert('의뢰원천을 선택해 주세요!');</script>");
			else if(ItemSearchControl1.ItemName.ToString() == "")
				RegisterStartupScript("","<script>alert('품목을 선택해 주세요!');</script>");
			else
			{
				decimal TotalCost = 0;

				TotalCost = (decimal.Parse(tb_DeliveryDemandQuantity1.Text) +
					decimal.Parse(tb_DeliveryDemandQuantity2.Text)+ decimal.Parse(tb_DeliveryDemandQuantity3.Text)+
					decimal.Parse(tb_DeliveryDemandQuantity4.Text)+ decimal.Parse(tb_DeliveryDemandQuantity5.Text)) * decimal.Parse(UnitCost.Value);


				ArrayList m_InputList = new ArrayList();

				m_InputList.Add(ItemSearchControl1.ItemNum.ToString());
				m_InputList.Add(ItemSearchControl1.ItemDrawNum.ToString());
				m_InputList.Add(ItemSearchControl1.ItemName.ToString());
//				m_InputList.Add(tb_ItemNum.Text);
//				m_InputList.Add(tb_ItemDrawNum.Text);
//				m_InputList.Add(wcItemName.DisplayValue);
				m_InputList.Add(tb_PropertyClassification.Text);
				m_InputList.Add(dlBuyingRequestSource.SelectedItem.Value);
				m_InputList.Add(dlBuyingRequestSource.SelectedItem.Text);
				m_InputList.Add(tb_DeliveryDemandQuantity1.Text);
				m_InputList.Add(wdcDeliveryDemandDate1.Text);
				m_InputList.Add(tb_DeliveryDemandQuantity2.Text);
				m_InputList.Add(wdcDeliveryDemandDate2.Text);
				m_InputList.Add(tb_DeliveryDemandQuantity3.Text);
				m_InputList.Add(wdcDeliveryDemandDate3.Text);
				m_InputList.Add(tb_DeliveryDemandQuantity4.Text);
				m_InputList.Add(wdcDeliveryDemandDate4.Text);
				m_InputList.Add(tb_DeliveryDemandQuantity5.Text);
				m_InputList.Add(wdcDeliveryDemandDate5.Text);
				m_InputList.Add(tb_TotalDeliveryDemandQuantity.Text);
				m_InputList.Add(UnitCost.Value);
				m_InputList.Add(TotalCost);

				Registration rg = new Registration("GoodsBuyingRequestAdd",Session["ID"].ToString(),lb_Index.Value,m_InputList);
				UltraWebGrid1.DataSource = rg.MainTableUpdate();
				UltraWebGrid1.DataBind();
			}
		}

		/// <summary>
		/// 삭제버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Delete_Click(object sender, System.EventArgs e)
		{
			
			Registration rg = new Registration("GoodsBuyingRequestAdd",lb_Index.Value);
			UltraWebGrid1.DataSource = rg.MainTableDelete();
			UltraWebGrid1.DataBind();


			dlBuyingRequestSource.SelectedIndex = 0;

			tb_PropertyClassification.Text = "";
			tb_DeliveryDemandQuantity1.Text = "0";
			tb_DeliveryDemandQuantity2.Text = "0";
			tb_DeliveryDemandQuantity3.Text = "0";
			tb_DeliveryDemandQuantity4.Text = "0";
			tb_DeliveryDemandQuantity5.Text = "0";
			wdcDeliveryDemandDate1.Value = "";
			wdcDeliveryDemandDate2.Value = "";
			wdcDeliveryDemandDate3.Value = "";
			wdcDeliveryDemandDate4.Value = "";
			wdcDeliveryDemandDate5.Value = "";
			tb_TotalDeliveryDemandQuantity.Text = "0";
			lb_Index.Value = "0";
			ItemSearchControl1.ItemNum = "";
			ItemSearchControl1.ItemDrawNum = "";
			ItemSearchControl1.ItemName = "";
			txtItemState.Text = "";
			
		}


		private void bt_Clear_Click(object sender, System.EventArgs e)
		{
			dlBuyingRequestSource.SelectedIndex = 0;

			tb_PropertyClassification.Text = "";
			tb_DeliveryDemandQuantity1.Text = "0";
			tb_DeliveryDemandQuantity2.Text = "0";
			tb_DeliveryDemandQuantity3.Text = "0";
			tb_DeliveryDemandQuantity4.Text = "0";
			tb_DeliveryDemandQuantity5.Text = "0";
			wdcDeliveryDemandDate1.Value = "";
			wdcDeliveryDemandDate2.Value = "";
			wdcDeliveryDemandDate3.Value = "";
			wdcDeliveryDemandDate4.Value = "";
			wdcDeliveryDemandDate5.Value = "";
			tb_TotalDeliveryDemandQuantity.Text = "0";
			lb_Index.Value = "0";
			txtItemState.Text = "";
		}

		private void LinkButton1_Click(object sender, System.EventArgs e)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select PropertyClassification, StandardUnitCost, SmallClassificationName From II_MT inner join PUC_MT on II_MT.ItemState = PUC_MT.SmallClassificationCode Where ItemNum = @num and (PropertyClassification = '제품' or PropertyClassification = '반제품' or PropertyClassification = '팬텀' or PropertyClassification = '상품') and II_MT.RecodingState = 1 and PUC_MT.RecodingState = 1 ";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = ItemSearchControl1.ItemNum;
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				tb_PropertyClassification.Text = dr["PropertyClassification"].ToString();
				txtItemState.Text = dr["SmallClassificationName"].ToString(); 
				if(decimal.Parse(dr["StandardUnitCost"].ToString()) == 0)
				{
					UnitCost.Value = "0";
				}
				else
				{
					UnitCost.Value = dr["StandardUnitCost"].ToString();
				}
				
			}
			dr.Close();
			conn.Close();
		}

		
	}
}
