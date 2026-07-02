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
using Infragistics.WebUI;

namespace KIT_ERP.BusinessManagement
{
	/// <summary>
	/// ProductionRequestAdd에 대한 요약 설명입니다.
	/// </summary>
	public class ProductionRequestAdd : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.Label Label14;
		protected System.Web.UI.WebControls.Label Label15;
		protected System.Web.UI.WebControls.Label Label16;
		protected System.Web.UI.WebControls.Label Label17;
		protected System.Web.UI.WebControls.Label Label18;
		protected System.Web.UI.WebControls.Label Label2;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.DropDownList dl_ProductionRequestSource;
		protected System.Web.UI.WebControls.TextBox tb_PropertyClassification;
		protected System.Web.UI.WebControls.TextBox tb_RequestQuantity1;
		protected System.Web.UI.WebControls.TextBox tb_RequestQuantity2;
		protected System.Web.UI.WebControls.TextBox tb_RequestQuantity3;
		protected System.Web.UI.WebControls.TextBox tb_RequestQuantity4;
		protected System.Web.UI.WebControls.TextBox tb_RequestQuantity5;
		protected System.Web.UI.WebControls.TextBox tb_TotalQuantity;
		protected System.Web.UI.WebControls.TextBox tb_TotalCost;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcRequestDate1;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcRequestDate2;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcRequestDate3;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcRequestDate4;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcRequestDate5;
		protected System.Web.UI.WebControls.Button bt_Clear;
		protected System.Web.UI.WebControls.Button bt_Update;
		protected System.Web.UI.WebControls.Button bt_Delete;
		protected System.Web.UI.WebControls.Button bt_Registration;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Index;
		protected System.Web.UI.HtmlControls.HtmlInputHidden UnitCost;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		protected System.Web.UI.WebControls.TextBox txtItemState;
		protected System.Web.UI.WebControls.LinkButton LinkButton2;
		protected System.Web.UI.HtmlControls.HtmlInputButton btAdd;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;

		private void Page_Load(object sender, System.EventArgs e)
		{
			ItemSearchControl1.Products = true; //제품 바인딩
			ItemSearchControl1.HalfFinishedProducts = true; //반제품 바인딩
			ItemSearchControl1.Phantom = true;//펜텀 바인딩
			ItemSearchControl1.IsDoPost = true;//포스트백

			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}
			if(!Page.IsPostBack)
			{
				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 영업관리 > 생산의뢰추가';</script>");

				//bt_Registration.Attributes.Add("onClick", "return Add('입력한 품목을 등록 하시겠습니까?');");
				//bt_Update.Attributes.Add("onClick", "return OK('선택한 품목을 수정');");
				bt_Delete.Attributes.Add("onClick", "return OK('선택한 품목을 삭제');");
				bt_Clear.Attributes.Add("onClick", "ResetTextBox();");

				tb_RequestQuantity1.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this); Process();");
				// onkeyup 이벤트가 발생하면 수량입력과동시에 금액을 계산하는 자바스크립트 함수 매핑
				tb_RequestQuantity1.Attributes.Add("OnKeyUp", "return Process()");
				// focus를 얻으면 자동으로 select()
				tb_RequestQuantity1.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				// focus를 잃으면 다시 입력란을 체크
				tb_RequestQuantity1.Attributes.Add("OnBlur", "OnBlur_Cur(this);");

				tb_RequestQuantity2.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this); Process();");
				tb_RequestQuantity2.Attributes.Add("OnKeyUp", "return Process()");
				tb_RequestQuantity2.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				tb_RequestQuantity2.Attributes.Add("OnBlur", "OnBlur_Cur(this);");

				tb_RequestQuantity3.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this); Process();");
				tb_RequestQuantity3.Attributes.Add("OnKeyUp", "return Process()");
				tb_RequestQuantity3.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				tb_RequestQuantity3.Attributes.Add("OnBlur", "OnBlur_Cur(this);");

				tb_RequestQuantity4.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this); Process();");
				tb_RequestQuantity4.Attributes.Add("OnKeyUp", "return Process()");
				tb_RequestQuantity4.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				tb_RequestQuantity4.Attributes.Add("OnBlur", "OnBlur_Cur(this);");

				tb_RequestQuantity5.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this); Process();");
				tb_RequestQuantity5.Attributes.Add("OnKeyUp", "return Process()");
				tb_RequestQuantity5.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				tb_RequestQuantity5.Attributes.Add("OnBlur", "OnBlur_Cur(this);");

				UnitCost.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this); Process();");
				UnitCost.Attributes.Add("OnKeyUp", "return Process()");
				UnitCost.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				UnitCost.Attributes.Add("OnBlur", "OnBlur_Cur(this);");

				bt_Clear.Attributes.Add("onClick", "ResetTextBox()");

				page_Load();
				//dl_ProductionRequestSource

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
			this.bt_Registration.Click += new System.EventHandler(this.bt_Registration_Click);
			this.LinkButton2.Click += new System.EventHandler(this.bt_Registration_Click);
			this.LinkButton1.Click += new System.EventHandler(this.LinkButton1_Click);
			this.bt_Clear.Click += new System.EventHandler(this.bt_Clear_Click);
			this.bt_Update.Click += new System.EventHandler(this.bt_Update_Click);
			this.bt_Delete.Click += new System.EventHandler(this.bt_Delete_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		private void page_Load()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			//	*************************************************
			//	**  생산의뢰원천 드롭다운리스트... 데이타바인딩... **
			//	*************************************************
			//코드분류표에서 대분류명이 생산의뢰원천인 소분류명을 가지고 옴.
			string str2 = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and SmallClassificationName != '정상' and RecodingState = 1 and LargeClassificationName = '생산의뢰원천'" ;
			SqlCommand comm2 = new SqlCommand(str2,conn);
			SqlDataAdapter da2 = new SqlDataAdapter(comm2) ;
			DataSet ds2 = new DataSet() ;
			da2.Fill(ds2);
				
			dl_ProductionRequestSource.DataSource = ds2;
			dl_ProductionRequestSource.DataTextField = ds2.Tables[0].Columns[0].ToString();
			dl_ProductionRequestSource.DataValueField = ds2.Tables[0].Columns[1].ToString();
			dl_ProductionRequestSource.DataBind();
			//dl_ProductionRequestSource.Items.Insert(0, "-선택-") ;
			//dl_ProductionRequestSource.Items[0].Value = "";

			conn.Close();


		}
		
		/// <summary>
		/// 초기화버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Clear_Click(object sender, System.EventArgs e)
		{
			clear();

		}

		/// <summary>
		/// 추가버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Registration_Click(object sender, System.EventArgs e)
		{
			
			if(dl_ProductionRequestSource.SelectedItem.Value == "")
				RegisterStartupScript("","<script>alert('의뢰원천을 선택해 주세요!');</script>");
			else if(ItemSearchControl1.ItemName.ToString() == "")
				RegisterStartupScript("","<script>alert('품목을 선택해 주세요!');</script>");
			else
			{

				ArrayList arr = new ArrayList();


				arr.Add(ItemSearchControl1.ItemNum.ToString());
				arr.Add(ItemSearchControl1.ItemDrawNum.ToString());
				arr.Add(ItemSearchControl1.ItemName.ToString());
				arr.Add(dl_ProductionRequestSource.SelectedItem.Value);
				arr.Add(dl_ProductionRequestSource.SelectedItem.Text);
				arr.Add(tb_PropertyClassification.Text);
				arr.Add("");//거래처명
				arr.Add(tb_TotalQuantity.Text);
				arr.Add(tb_RequestQuantity1.Text);
				arr.Add(wdcRequestDate1.Text);
				arr.Add(tb_RequestQuantity2.Text);
				arr.Add(wdcRequestDate2.Text);
				arr.Add(tb_RequestQuantity3.Text);
				arr.Add(wdcRequestDate3.Text);
				arr.Add(tb_RequestQuantity4.Text);
				arr.Add(wdcRequestDate4.Text);
				arr.Add(tb_RequestQuantity5.Text);
				arr.Add(wdcRequestDate5.Text);
				arr.Add(UnitCost.Value);

				Registration reg = new Registration("ProductionRequestAdd",Session["ID"].ToString(),lb_Index.Value,arr);
				UltraWebGrid1.DataSource = reg.MainTableRegistration();
				UltraWebGrid1.DataBind();
			}


		}

		/// <summary>
		/// 수정버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Update_Click(object sender, System.EventArgs e)
		{
			ArrayList arr = new ArrayList();


			arr.Add(ItemSearchControl1.ItemNum.ToString());
			arr.Add(ItemSearchControl1.ItemDrawNum.ToString());
			arr.Add(ItemSearchControl1.ItemName.ToString());
			arr.Add(dl_ProductionRequestSource.SelectedItem.Value);
			arr.Add(dl_ProductionRequestSource.SelectedItem.Text);
			arr.Add(tb_PropertyClassification.Text);
			arr.Add("");//거래처명
			arr.Add(tb_TotalQuantity.Text);
			arr.Add(tb_RequestQuantity1.Text);
			arr.Add(wdcRequestDate1.Text);
			arr.Add(tb_RequestQuantity2.Text);
			arr.Add(wdcRequestDate2.Text);
			arr.Add(tb_RequestQuantity3.Text);
			arr.Add(wdcRequestDate3.Text);
			arr.Add(tb_RequestQuantity4.Text);
			arr.Add(wdcRequestDate4.Text);
			arr.Add(tb_RequestQuantity5.Text);
			arr.Add(wdcRequestDate5.Text);
			arr.Add(UnitCost.Value);

			Registration reg = new Registration("ProductionRequestAdd",Session["ID"].ToString(),lb_Index.Value,arr);
			UltraWebGrid1.DataSource = reg.MainTableUpdate();
			UltraWebGrid1.DataBind();
		
		}

		/// <summary>
		/// 삭제버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Delete_Click(object sender, System.EventArgs e)
		{
			Registration reg = new Registration("ProductionRequestAdd",lb_Index.Value);
			UltraWebGrid1.DataSource = reg.MainTableDelete();
			UltraWebGrid1.DataBind();

			clear();

			ItemSearchControl1.ItemNum = "";
			ItemSearchControl1.ItemDrawNum = "";
			ItemSearchControl1.ItemName = "";
		}

		/// <summary>
		/// 초기화 함수
		/// </summary>
		private void clear()
		{
			dl_ProductionRequestSource.SelectedIndex = 0;
			tb_PropertyClassification.Text = "";
			UnitCost.Value = "0";
			tb_TotalQuantity.Text = "0";
			tb_TotalCost.Text = "0";
			lb_Index.Value = "0";
			tb_RequestQuantity1.Text = "0";
			tb_RequestQuantity2.Text = "0";
			tb_RequestQuantity3.Text = "0";
			tb_RequestQuantity4.Text = "0";
			tb_RequestQuantity5.Text = "0";
			wdcRequestDate1.Value = "";
			wdcRequestDate2.Value = "";
			wdcRequestDate3.Value = "";
			wdcRequestDate4.Value = "";
			wdcRequestDate5.Value = "";
			txtItemState.Text = "";
		}

		private void LinkButton1_Click(object sender, System.EventArgs e)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select PropertyClassification, StandardUnitCost, SmallClassificationName From II_MT inner join PUC_MT on ItemState = SmallClassificationCode Where ItemNum = @num and (PropertyClassification = '제품' or PropertyClassification = '반제품' or PropertyClassification = '팬텀' or PropertyClassification = '상품') and II_MT.RecodingState = 1 and PUC_MT.RecodingState = 1";
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
