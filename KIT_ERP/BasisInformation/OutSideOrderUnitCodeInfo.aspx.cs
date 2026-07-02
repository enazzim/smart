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
	/// OutSideUnitCostInfo에 대한 요약 설명입니다.
	/// </summary>
	public class OutSideOrderUnitCodeInfo : System.Web.UI.Page
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
		protected System.Web.UI.WebControls.Label lb_Company;
		protected System.Web.UI.WebControls.Label lb_BeginProcessCode;
		protected System.Web.UI.WebControls.Label lb_EndProcessCode;
		protected System.Web.UI.WebControls.Label lb_StandardUnitCost;
		protected System.Web.UI.WebControls.Label lb_DiscountUnitCost;
		protected System.Web.UI.WebControls.Label lb_BeginDate;
		protected System.Web.UI.WebControls.Label lb_EndDate;
		protected System.Web.UI.WebControls.Label lb_OrderRate;
		protected System.Web.UI.WebControls.Label lb_ItemChoice;
		protected System.Web.UI.WebControls.Button bt_OutSideUnitCost;
		protected System.Web.UI.WebControls.Button bt_RelationView;
		protected System.Web.UI.WebControls.DropDownList dl_BeginProcess;
		protected System.Web.UI.WebControls.DropDownList dl_EndProcess;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Index;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_ReferenceIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_ReferenceTable;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid Ultrawebgrid2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden ItemIndex;
		protected System.Web.UI.WebControls.TextBox tb_OrderRate1;
		protected System.Web.UI.WebControls.TextBox tb_StandardUnitCost1;
		protected System.Web.UI.WebControls.TextBox tb_DiscountUnitCost1;
		protected System.Web.UI.WebControls.TextBox tb_StandardUnitCost;
		protected System.Web.UI.WebControls.TextBox tb_OrderRate;
		protected System.Web.UI.WebControls.TextBox tb_DiscountUnitCost;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcBeginDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcEndDate;
		protected Infragistics.WebUI.WebCombo.WebCombo wcCompany;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.LinkButton lnk_Update;
		protected System.Web.UI.WebControls.LinkButton LinkButton2;
		protected System.Web.UI.WebControls.Button bt_Registration;
		protected System.Web.UI.WebControls.Button Button1;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected System.Web.UI.WebControls.TextBox tbUpdateReason;
		protected System.Web.UI.WebControls.Button btHistory;

		protected DataSet dsExcel = new DataSet();
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			//ItemSearchControl1.Commodity = true; //상품 바인딩
			//ItemSearchControl1.RawMaterials = true; //원자재 바인딩	
			//ItemSearchControl1.Phantom = true; //팬텀 바인딩	
			ItemSearchControl1.HalfFinishedProducts =  true; //반제품 바인딩	
			ItemSearchControl1.Products = true; //제품 바인딩	

			ItemSearchControl1.IsDoPost = true;
			
			if(!Page.IsPostBack)
			{
				////Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 기준정보 > 외주단가정보';</script>");

				bt_Registration.Attributes.Add("onClick", "return OK('입력한 항목을 등록');");
				bt_Update.Attributes.Add("onClick", "return OK('선택한 항목을 수정');");
				bt_Delete.Attributes.Add("onClick", "return OK('선택한 항목을 삭제');");

				//

				tb_StandardUnitCost.Attributes.Add("OnKeyUp", "OnKeyUp_Currency(this)");
				tb_StandardUnitCost.Attributes.Add("OnKeyDown", "OnKeyDown_Currency(this);");
				// focus를 얻으면 자동으로 select()
				tb_StandardUnitCost.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				// focus를 잃으면 다시 입력란을 체크
				tb_StandardUnitCost.Attributes.Add("OnBlur", "OnBlur_Float(this);");
                			
				tb_DiscountUnitCost.Attributes.Add("OnKeyDown", "OnKeyDown_Currency(this);");
				// focus를 얻으면 자동으로 select()
				tb_DiscountUnitCost.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				// focus를 잃으면 다시 입력란을 체크
				tb_DiscountUnitCost.Attributes.Add("OnBlur", "OnBlur_Float(this);");
				tb_DiscountUnitCost.Attributes.Add("OnKeyUp", "OnKeyUp_Currency(this)");


				tb_OrderRate.Attributes.Add("OnKeyDown", "OnKeyDown_Currency(this);");
				// focus를 얻으면 자동으로 select()
				tb_OrderRate.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				// focus를 잃으면 다시 입력란을 체크
				tb_OrderRate.Attributes.Add("OnBlur", "OnBlur_Float(this);");
				
				bt_Clear.Attributes.Add("onClick", "ResettxtBox()");			
				

			
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				conn.Open();
				

				// 거래처바인딩
				string strSQL = "SELECT CompanyName, PresidentName, BusinessRegistrationNum, CompanyInfoIndex FROM CI_MT where RecodingState = 1 and OutSideOrderCompany = 1 order by CompanyName";
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
			this.lnk_Update.Click += new System.EventHandler(this.lnk_Update_Click);
			this.LinkButton2.Click += new System.EventHandler(this.LinkButton2_Click);
			this.bt_OutSideUnitCost.Click += new System.EventHandler(this.bt_OutSideUnitCost_Click);
			this.btHistory.Click += new System.EventHandler(this.btHistory_Click);
			this.bt_RelationView.Click += new System.EventHandler(this.bt_RelationView_Click);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.bt_Clear.Click += new System.EventHandler(this.bt_Clear_Click);
			this.bt_Update.Click += new System.EventHandler(this.bt_Update_Click);
			this.bt_Delete.Click += new System.EventHandler(this.bt_Delete_Click);
			this.bt_Registration.Click += new System.EventHandler(this.bt_Registration_Click);
			this.Ultrawebgrid2.DblClick += new Infragistics.WebUI.UltraWebGrid.ClickEventHandler(this.Ultrawebgrid2_DblClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		
		/// <summary>
		/// 외주단가보기 버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_OutSideUnitCost_Click(object sender, System.EventArgs e)
		{
			if(ItemSearchControl1.ItemNum.ToString() =="")
			{
				RegisterStartupScript("","<script language=javascript>alert('품목을 선택해주세요');</script>");
			}
			else
			{
				this.DataView();
			}
		}

		/// <summary>
		/// 참조보기 버튼 클릭시
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

				string m_TableName = "외주단가정보";
				int m_RecodeIdx = int.Parse(lb_Index.Value);
			

				Reference rf = new Reference(m_TableName, m_RecodeIdx);
				Ultrawebgrid2.DataSource = rf.reference();
				Ultrawebgrid2.DataBind();
			}
		}

		/// <summary>
		/// 외주단가 그리드 바인딩후 보여주는 함수
		/// </summary>
		private void DataView()
		{
			string m_itemnum = ItemSearchControl1.ItemNum.ToString();
			string m_page = "OutSideOrderUnitCodeInfo";

			
			View view = new View(m_itemnum,m_page);
			UltraWebGrid1.DataSource = view.ItemView();
			UltraWebGrid1.DataBind();
			
		}

		/// <summary>
		/// 등록버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Registration_Click(object sender, System.EventArgs e)
		{
			if(ItemSearchControl1.ItemNum.ToString() =="" || wcCompany.DisplayValue == null || wcCompany.DisplayValue == ""  || dl_BeginProcess.SelectedIndex ==0||dl_EndProcess.SelectedIndex==0 || tb_OrderRate.Text =="" || tb_StandardUnitCost.Text == "" || wdcBeginDate.Text == null)
			{
				RegisterStartupScript("","<script>alert('필수항목을 입력해 주세요');</script>");
			}
			else
			{
				if(Page.IsValid == true)
				{
					string m_RequirePage = "OutSideOrderUnitCodeInfo";
					string m_RequireAction = "Registration";
					string m_TableName = "UCI_MT";
					int m_Idx = (int)PageName.OutSideOrderUnitCodeInfo;
					string m_User = Session["ID"].ToString();
					string m_RecodeIdx = lb_Index.Value;

					ArrayList InputList = new ArrayList();

					InputList.Add(ItemSearchControl1.ItemNum.ToString());
					InputList.Add("외주단가");
					InputList.Add(wcCompany.DataValue.ToString());//사업자등록번호
					InputList.Add(dl_BeginProcess.SelectedItem.Value);
					InputList.Add(dl_EndProcess.SelectedItem.Value);
					InputList.Add(tb_OrderRate.Text);
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
		/// 삭제버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Delete_Click(object sender, System.EventArgs e)
		{
			
			string m_RequirePage = "OutSideOrderUnitCodeInfo";
			string m_RequireAction = "Delete";
			string m_TableName = "UCI_MT";
			int m_Idx = (int)PageName.OutSideOrderUnitCodeInfo;
			string m_User = Session["ID"].ToString();
			string m_RecodeIdx = lb_Index.Value;

			ArrayList InputList = new ArrayList();

			InputList.Add(ItemSearchControl1.ItemNum.ToString());
			InputList.Add("외주단가");
			if(wcCompany.DisplayValue == null || wcCompany.DisplayValue == "")
				InputList.Add(0);//사업자등록번호
			else
				InputList.Add(wcCompany.DataValue.ToString());//사업자등록번호
			InputList.Add(dl_BeginProcess.SelectedItem.Value);
			InputList.Add(dl_EndProcess.SelectedItem.Value);
			InputList.Add(tb_OrderRate.Text);
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

			//하위품목 Clear
			clear();
		
			this.DataView();
			
		}

		/// <summary>
		/// 클리어 함수
		/// </summary>
		private void clear()
		{
			wcCompany.DisplayValue ="";
			wcCompany.DataValue ="";
			dl_BeginProcess.SelectedIndex =0;
			dl_EndProcess.SelectedIndex =0;
			tb_OrderRate.Text = "0";
			tb_StandardUnitCost.Text = "";
			tb_DiscountUnitCost.Text = "0";
			wdcBeginDate.Value = "";
			wdcEndDate.Value = "";
			lb_Index.Value = "0";

		}


		/// <summary>
		/// 수정버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Update_Click(object sender, System.EventArgs e)
		{
			if(Page.IsValid == true)
			{
				
				string m_RequirePage = "OutSideOrderUnitCodeInfo";
				string m_RequireAction = "Update";
				string m_TableName = "UCI_MT";
				int m_Idx = (int)PageName.OutSideOrderUnitCodeInfo;
				string m_User = Session["ID"].ToString();
				string m_RecodeIdx = lb_Index.Value;

				ArrayList InputList = new ArrayList();

				InputList.Add(ItemSearchControl1.ItemNum.ToString());
				InputList.Add("외주단가");
				if(wcCompany.DisplayValue == null || wcCompany.DisplayValue == "")
					InputList.Add(0);//사업자등록번호
				else
					InputList.Add(wcCompany.DataValue.ToString());//사업자등록번호
				InputList.Add(dl_BeginProcess.SelectedItem.Value);
				InputList.Add(dl_EndProcess.SelectedItem.Value);
				InputList.Add(tb_OrderRate.Text);
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


		/// <summary>
		/// 초기화 버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Clear_Click(object sender, System.EventArgs e)
		{
;
			tb_PropertyClassification.Text = "";
			tb_Standard.Text = "";
			tb_Unit.Text ="";
			
			this.clear();
		}


		/// <summary>
		/// 어깨창 단가정보 보기
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LinkButton1_Click(object sender, System.EventArgs e)
		{
			RegisterStartupScript("","<script>window.open('./Popup/UnitCost.aspx','UnitCost'," + "'width=830" + "," + "height=500px');</script>");
		}

		private void UltraWebGrid1_SelectedRowsChange(object sender, Infragistics.WebUI.UltraWebGrid.SelectedRowsEventArgs e)
		{
			
		
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
//			string formID = "OutSideOrderUnitCodeInfo" ;
//			string txtID = "ItemIndex";
//			
//			//RegisterStartupScript("","<script>window.open('./Popup/Item.aspx?formID=" + formID + "&txtID=" + txtID +  "&txt=" + txt + "','Item'," + "'width=800" + "," + "height=400px');</script>");
//			RegisterStartupScript("","<script>window.open('./Popup/Iteminfo.aspx?formID=" + formID + "&txtID=" + txtID +  "&txt=" + txt + "','Item'," + "'width=800" + "," + "height=400px');</script>");
//		}

		/// <summary>
		/// 참조보기 그리드 더블클릭
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

		private void lnk_Update_Click(object sender, System.EventArgs e)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = @"Select II_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName, II_MT.PropertyClassification, 
			II_MT.Standard,II_MT.ItemInfoIndex, PUC_MT.SmallClassificationName  From II_MT Join PUC_MT on 
			II_MT.Unit = PUC_MT.SmallClassificationCode Where II_MT.RecodingState = 1 and II_MT.ItemNum = @num";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@num",ItemSearchControl1.ItemNum);
			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			da.Fill(ds);

			foreach(DataRow dr in ds.Tables[0].Rows)
			{		
//				ItemSearchControl1.ItemNum = dr["ItemNum"].ToString();
//				ItemSearchControl1.ItemDrawNum = dr["ItemDrawNum"].ToString();
//				ItemSearchControl1.ItemName = dr["ItemName"].ToString();

				tb_PropertyClassification.Text = dr["PropertyClassification"].ToString();
				tb_Unit.Text = dr["SmallClassificationName"].ToString();
				tb_Standard.Text = dr["Standard"].ToString();
			}


			//	*************************************************
			//	**  시작공정 드롭다운리스트... 데이타바인딩... ** 
			//	*************************************************
			//코드분류표에서 대분류명이 공정인 소분류명을 가지고 옴.
			string str_BeginProcess = @"select SmallClassificationName, PSI_MT.ProcessCode From PSI_MT inner join PUC_MT on PSI_MT.ProcessCode = PUC_MT.SmallClassificationCode where PSI_MT.RecodingState = 1 and ItemNum = @num";
			SqlCommand comm_BeginProcess = new SqlCommand(str_BeginProcess,conn);
			comm_BeginProcess.Parameters.Add("@num",ItemSearchControl1.ItemNum);
			SqlDataAdapter da_BeginProcess = new SqlDataAdapter(comm_BeginProcess) ;
			DataSet ds_BeginProcess = new DataSet() ;
			da_BeginProcess.Fill(ds_BeginProcess);
				
			dl_BeginProcess.DataSource = ds_BeginProcess;
			dl_BeginProcess.DataTextField = ds_BeginProcess.Tables[0].Columns[0].ToString();
			dl_BeginProcess.DataValueField = ds_BeginProcess.Tables[0].Columns[1].ToString();
			dl_BeginProcess.DataBind();
			dl_BeginProcess.Items.Insert(0, "-선택-") ;
			dl_BeginProcess.Items[0].Value = "";


			//	*************************************************
			//	**  종료공정 드롭다운리스트... 데이타바인딩... ** 
			//	*************************************************
			//코드분류표에서 대분류명이 공정인 소분류명을 가지고 옴.
			string str_EndProcess = @"select SmallClassificationName, PSI_MT.ProcessCode From PSI_MT inner join PUC_MT on PSI_MT.ProcessCode = PUC_MT.SmallClassificationCode where PSI_MT.RecodingState = 1 and ItemNum = @num";
			SqlCommand comm_EndProcess = new SqlCommand(str_EndProcess,conn);
			comm_EndProcess.Parameters.Add("@num",ItemSearchControl1.ItemNum);
			SqlDataAdapter da_EndProcess = new SqlDataAdapter(comm_EndProcess) ;
			DataSet ds_EndProcess = new DataSet() ;
			da_EndProcess.Fill(ds_EndProcess);
				
			dl_EndProcess.DataSource = ds_EndProcess;
			dl_EndProcess.DataTextField = ds_EndProcess.Tables[0].Columns[0].ToString();
			dl_EndProcess.DataValueField = ds_EndProcess.Tables[0].Columns[1].ToString();
			dl_EndProcess.DataBind();
			dl_EndProcess.Items.Insert(0, "-선택-") ;
			dl_EndProcess.Items[0].Value = "";


			conn.Close();
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

		private void Button1_Click(object sender, System.EventArgs e)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlTransaction tr = conn.BeginTransaction();
			try
			{
				string year =  DateTime.Now.Year.ToString();
				
				//string str = "Select * From UCI_MT where RecodingState =  1 and UnitCostDistinction = '외주단가' and ItemNum ='TG16-1104A-2'";
				string str = "Select * From UCI_MT where RecodingState =  1 and UnitCostDistinction = '외주단가'  and OrderRate > 0 and (RegistrationDate >= @date)";
				SqlCommand comm = new SqlCommand();
				//comm.Parameters.Add("@date", year + "-01-01");
				
				comm.CommandText = str;
				comm.Connection = conn;
				comm.Transaction = tr;
				//comm.Parameters.Add("@date",SqlDbType.SmallDateTime).Value = year+"-01-01";
				comm.Parameters.Add("@date",SqlDbType.SmallDateTime).Value = "2010-01-01";
				DataSet ds = new DataSet();
				SqlDataAdapter da = new SqlDataAdapter(comm);
				da.Fill(ds);
				foreach(DataRow dr in ds.Tables[0].Rows)
				{
					OutSideStoreTable(conn,tr,dr["ItemNum"].ToString(),dr["BeginProcessCode"].ToString(),dr["BusinessRegistrationNum"].ToString());
				}

				tr.Commit();
				Response.Write("<script language=javascript>");
				Response.Write("alert('창고를 생성하였습니다.');");
				Response.Write("</script>");
			}
			catch(Exception ee)
			{
				
				Response.Write("<script language=javascript>");
				Response.Write("alert('"+ee.Message+"');");
				Response.Write("</script>");
				tr.Rollback();
			}
			finally
			{
				conn.Close();
			}
			


		}

		/// <summary>
		/// 외주창고 생성함수
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="ItemNum"></param>
		/// <param name="ProcessCode"></param>
		/// <param name="Num"></param>
		private void OutSideStoreTable(SqlConnection conn, SqlTransaction tr,string ItemNum,string ProcessCode,string Num)
		{
			//시작공정의 하위품목을 가지고 등록한다.
			//시작공정이 최하위공정이면 바로 아래단계품목들을 출고시킨다
			//이때 바로 아랫단계가 원자재면 외주창고에 레코드를 생성시킨다
			//또한 아랫단계가 반제품인경우 공정을 순서대로 정렬해서 그 공정을 하나씩 꺼내어 외주단가 테이블에
			//동일한 거래처가 아닌것이 존재하면 그 공정을 등록하고 외주단가에 그 공정이 없으면 하나씩 내려가면서
			//공정을 판단해 등록시킨다.


			// 일단 발주원장의 시작공정 아래단계가 어떤 공정인지 알아야 한다
			// 공정순서가 제일 낮은 것이면 바로 아래단계 품목이 나가야 하고
			// 그렇지 않으면 그아래 공정순서 품목이 나가야 한다.
			// 그러므로 아랫단계에 공정이 있는지 없는지 확인하는것이 우선이다

			// 일단 지금 시작공정의 공정순서를 구한다음 그아래 가 있는지 확인한다
			int sequence = 0;

			
			string str = "Select ProcessSequenceNum From PSI_MT Where RecodingState = 1 and ProcessCode = @code and ItemNum = @itemnum";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			comm.CommandText = str;
			comm.Parameters.Add("@code",SqlDbType.VarChar).Value = ProcessCode;				//시작공정코드
			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = ItemNum;			//품목번호
			
			SqlDataReader dr_num = comm.ExecuteReader();
			while(dr_num.Read())
			{
				sequence = int.Parse(dr_num["ProcessSequenceNum"].ToString());
			}
			dr_num.Close();
			comm.Parameters.Clear();
			
			//지금 공정순서보다 아래에 공정순서가 있는지 없는지를 구한다
			int sequence1 = 0;	//공정순서
			string Code = "";	// 공정코드
			str = "Select Max(ProcessSequenceNum) From PSI_MT Where RecodingState = 1 and ProcessSequenceNum < @num and ItemNum = @itemnum";
			comm.Parameters.Add("@num",SqlDbType.Int).Value = sequence;
			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = ItemNum;			//품목번호
			comm.CommandText = str;
			if(comm.ExecuteScalar() == null || comm.ExecuteScalar().ToString().Trim() == "")
				sequence1 = 0;
			else
				sequence1 = int.Parse(comm.ExecuteScalar().ToString());
			comm.Parameters.Clear();


			
			str = "Select ProcessCode From PSI_MT Where RecodingState = 1 and ProcessSequenceNum = @num and ItemNum = @itemnum";
			comm.Parameters.Add("@num",SqlDbType.Int).Value = sequence1;
			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = ItemNum;			//품목번호
			comm.CommandText = str;
			SqlDataReader dr2 = comm.ExecuteReader();
			while(dr2.Read())
			{
				Code = dr2["ProcessCode"].ToString();
			}
			dr2.Close();
			comm.Parameters.Clear();

			

			// 아래 공정순서번호가 존재할때는(공정순서가 0이 아닌 경우) 바로 그 품목을 출고시킨다.
			// 지금 들어있는 공정순서번호와 품목을 출고시킨다.
			string st_ItemNum = ItemNum;			//품목번호

			

			if(sequence1 != 0)
			{
				//지금 공정 이하를 스택에 저장시킨다.
				//하나씩 꺼내서 해당 공정이 자가이거나, 단가 테이블에 해당 품목의 공정이 종료공정으로 가지고 있으면 다른회사로 나가는 품목이므로
				//바로 아래공정을 출고시킨다.
				//꺼낸 품목의 공정순서를 넣어둔다.
				Stack stack = new Stack();
				str = "Select ProcessSequenceNum from PSI_MT where RecodingState = 1 AND ItemNum = @itemnum and ProcessSequenceNum < @num  order by ProcessSequenceNum";
				comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = st_ItemNum;
				comm.Parameters.Add("@num",SqlDbType.VarChar).Value = sequence;
				comm.CommandText = str;
				SqlDataReader dr_Sequence = comm.ExecuteReader();
				while(dr_Sequence.Read())
				{
					stack.Push(int.Parse(dr_Sequence["ProcessSequenceNum"].ToString()));
				}
				dr_Sequence.Close();
				comm.Parameters.Clear();

				// 스택1에 있는 공정순서를 하나씩꺼낸다.
				while(stack.Count > 0)
				{
					int sequencenum = int.Parse(stack.Pop().ToString());
					str = " SELECT  count(*) FROM PSI_MT WHERE RecodingState = 1 and WorkDistinction <> '외주' and ItemNum = @item and ProcessSequenceNum = @num";
					comm.CommandText =str;
					comm.Parameters.Add("@item",SqlDbType.VarChar).Value = st_ItemNum;//품목번호
					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = sequencenum;//공정순서
					int aaa = int.Parse(comm.ExecuteScalar().ToString());
					comm.Parameters.Clear();
								
					//공정코드를 구한다
					string processcode="";
					str = " SELECT  ProcessSequenceNum, ProcessCode, SmallClassificationName " +
						" FROM      PSI_MT INNER JOIN " +
						" PUC_MT ON ProcessCode = SmallClassificationCode " +
						" WHERE PSI_MT.RecodingState = 1 AND ItemNum = @itemnum and ProcessSequenceNum = @num";
					comm.CommandText= str;
					comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = st_ItemNum;
					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = sequencenum;
					SqlDataReader dr_code = comm.ExecuteReader();
					comm.Parameters.Clear();
					while(dr_code.Read())
					{
						processcode = dr_code["ProcessCode"].ToString();//공정코드
					}
					dr_code.Close();


					// 외주가 아니면 등록
					if(aaa != 0)
					{
						if(OS_MT_ExistingRecord(conn,tr,st_ItemNum,sequencenum,Num) == false)
						{
							str = "Insert Into OS_MT (ItemNum,ProcessSequenceNum,ProcessCode,BusinessRegistrationNum,RecodingState,[Year]) values(@num,@sequence,@code,@Com,1,@year)";
									
							comm.Parameters.Add("@num",SqlDbType.VarChar).Value = st_ItemNum;			//품목번호
							comm.Parameters.Add("@sequence",SqlDbType.Int).Value = sequencenum;	//공정순서
							comm.Parameters.Add("@code",SqlDbType.VarChar).Value = processcode;	//공정코드
							comm.Parameters.Add("@Com",SqlDbType.VarChar).Value = Num;	//거래처
							comm.Parameters.Add("@year",DateTime.Now.Year);
							comm.CommandText = str;
							comm.ExecuteNonQuery();
							comm.Parameters.Clear();
						}
						break;
					}
					else//외주이면 외주단가에서 지금 찾은 공정이랑 종료공정이 동일한것이 있는지 살펴본다. 있으면 출고시키고 없으면 이전공정을 찾는다
					{
						// 외주단가에서 동일회사 외주공정인지 파악한다.
						str = "select count(*) From UCI_MT Where RecodingState = 1 and ItemNum = @itemnum and EndProcessCode = @code and BusinessRegistrationNum = @com";
						comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = st_ItemNum;
						comm.Parameters.Add("@code",SqlDbType.VarChar).Value = processcode;
						comm.Parameters.Add("@com",SqlDbType.VarChar).Value = Num;	//거래처
						comm.CommandText = str;
						int find = int.Parse(comm.ExecuteScalar().ToString());
						comm.Parameters.Clear();
									

						// 동일회사가 아니면 등록
						if(find == 0)
						{
							if(OS_MT_ExistingRecord(conn,tr,st_ItemNum,sequencenum,Num) == false)
							{
								str = "Insert Into OS_MT (ItemNum,ProcessSequenceNum,ProcessCode,BusinessRegistrationNum,RecodingState,[Year]) values(@num,@sequence,@code,@Com,1,@year)";
								comm.CommandText = str;
								comm.Parameters.Add("@num",SqlDbType.VarChar).Value = st_ItemNum;			//품목번호
								comm.Parameters.Add("@sequence",SqlDbType.Int).Value = sequencenum;	//공정순서
								comm.Parameters.Add("@code",SqlDbType.VarChar).Value = processcode;	//공정코드
								comm.Parameters.Add("@Com",SqlDbType.VarChar).Value = Num;	//거래처
								comm.Parameters.Add("@year",DateTime.Now.Year);
								comm.ExecuteNonQuery();
								comm.Parameters.Clear();
							}
							break;
						}
					}
				}
			}			
			else
			{
				// 순서번호가 0 이면 하위품목을 찾아 스택에 Push한다
				Stack st = new Stack();	

				string item = "";
				//하위품목을 찾기위해서는 품목구성정보에 지금 품목을 모품으로 하는 모든 레코드를 스택에 넣는다
				//str = "Select ChildItemNum, ItemDrawNum, Item From IOI_MT JOIN ON II_MT Where RecodingState = 1 and ParentItemNum = @itemnum";
				str = "SELECT ItemNum, ItemDrawNum, ItemName, NeedQuantityNumerator, NeedQuantityDenominator FROM II_MT INNER JOIN IOI_MT ON II_MT.ItemNum = IOI_MT.ChildItemNum WHERE (IOI_MT.RecodingState = 1) AND (IOI_MT.ParentItemNum = @itemnum) ";
				
				comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = st_ItemNum;
				comm.CommandText= str;
				SqlDataReader dr_Child = comm.ExecuteReader();
				while(dr_Child.Read())
				{
					st.Push(dr_Child["ItemNum"].ToString());
				}
				dr_Child.Close();
				comm.Parameters.Clear();
				
				
				// 스택에 품목이 있는지 확인한다.
				if(st.Count != 0)
				{
					// 스택의 내용이 있으면 다 꺼낼때까지 계속 Pop한다
					while(st.Count > 0)
					{	
						item =st.Pop().ToString();
						// 꺼낸품목의 공정순서를 스택1에 넣는다
						Stack st1 = new Stack();					
						int sequencenum=0;

						//꺼낸 품목의 공정순서를 넣어둔다.
						str = "Select ProcessSequenceNum from PSI_MT where RecodingState = 1 AND ItemNum = @itemnum  order by ProcessSequenceNum";
						comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = item;
						comm.CommandText = str;
						SqlDataReader dr_Sequence = comm.ExecuteReader();
						while(dr_Sequence.Read())
						{
							//공정순서
							st1.Push(int.Parse(dr_Sequence["ProcessSequenceNum"].ToString()));
							
						}
						dr_Sequence.Close();
						comm.Parameters.Clear();

						
						//공정순서가 없으면 지금 품목이 원자재이므로 이를 출고시킨다.
						if(st1.Count == 0)
						{
							//품번,도번,품명 구한다
							string itemdraw=""; string name="";
							str = "Select * From II_MT Where ItemNum = @num and RecodingState = 1";
							
							comm.CommandText = str;
							comm.Parameters.Add("@num",SqlDbType.VarChar).Value = item;
							SqlDataReader dr_Item = comm.ExecuteReader();
							comm.Parameters.Clear();
							while(dr_Item.Read())
							{
								itemdraw = dr_Item["ItemDrawNum"].ToString();
								name = dr_Item["ItemName"].ToString();
							}
							dr_Item.Close();
														
							if(OS_MT_ExistingRecord(conn,tr,item,0,Num) == false)
							{
								str = "Insert Into OS_MT (ItemNum,ProcessSequenceNum,ProcessCode,BusinessRegistrationNum,RecodingState, [Year]) values(@num,'0','14000000',@Com,1, @year)";
							
								comm.Parameters.Add("@num",SqlDbType.VarChar).Value = item;
								comm.Parameters.Add("@Com",SqlDbType.VarChar).Value = Num;
								comm.Parameters.Add("@year",DateTime.Now.Year);
								comm.CommandText = str;
								
								comm.ExecuteNonQuery();
								comm.Parameters.Clear();
							}
						}
						else //그렇지 않으면 그 품목의 최상위 공정순서를 찾아서 그 공정이 외주인지 판단한다. 카운트가 0이면 외주가 아니므로 출고시킨다.
						{
							// 스택1에 있는 공정순서를 하나씩꺼낸다.
							while(st1.Count > 0)
							{
								sequencenum = int.Parse(st1.Pop().ToString());
								str = " SELECT  count(*) FROM PSI_MT WHERE RecodingState = 1 and WorkDistinction <> '외주' and ItemNum = @item and ProcessSequenceNum = @num";
								comm.CommandText =str;
								comm.Parameters.Add("@item",SqlDbType.VarChar).Value = item;//품목번호
								comm.Parameters.Add("@num",SqlDbType.VarChar).Value = sequencenum;//공정순서
								int aaa = int.Parse(comm.ExecuteScalar().ToString());
								comm.Parameters.Clear();
								
								//공정코드를 구한다
								string processcode="";
								str = " SELECT  ProcessSequenceNum, ProcessCode, SmallClassificationName " +
									" FROM      PSI_MT INNER JOIN " +
									" PUC_MT ON ProcessCode = SmallClassificationCode " +
									" WHERE PSI_MT.RecodingState = 1 AND ItemNum = @itemnum and ProcessSequenceNum = @num";
								comm.CommandText= str;
								comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = item;
								comm.Parameters.Add("@num",SqlDbType.VarChar).Value = sequencenum;
								SqlDataReader dr_code = comm.ExecuteReader();
								comm.Parameters.Clear();
								while(dr_code.Read())
								{
									processcode = dr_code["ProcessCode"].ToString();//공정코드
								}
								dr_code.Close();


								// 외주가 아니면 등록
								if(aaa != 0)
								{
									if(OS_MT_ExistingRecord(conn,tr,item,sequencenum,Num) == false)
									{
										str = "Insert Into OS_MT (ItemNum,ProcessSequenceNum,ProcessCode,BusinessRegistrationNum,RecodingState,[Year]) values(@num,@sequence,@code,@Com,1,@year)";
									
										comm.Parameters.Add("@num",SqlDbType.VarChar).Value = item;			//품목번호
										comm.Parameters.Add("@sequence",SqlDbType.Int).Value = sequencenum;	//공정순서
										comm.Parameters.Add("@code",SqlDbType.VarChar).Value = processcode;	//공정코드
										comm.Parameters.Add("@Com",SqlDbType.VarChar).Value = Num;	//거래처
										comm.Parameters.Add("@year",DateTime.Now.Year);
										comm.CommandText = str;
										comm.ExecuteNonQuery();
										comm.Parameters.Clear();
									}
									break;
								}
								else//외주이면 외주단가에서 지금 찾은 공정이랑 종료공정이 동일한것이 있는지 살펴본다. 있으면 출고시키고 없으면 이전공정을 찾는다
								{
									// 외주단가에서 동일회사 외주공정인지 파악한다.
									str = "select count(*) From UCI_MT Where RecodingState = 1 and ItemNum = @itemnum and EndProcessCode = @code and BusinessRegistrationNum = @com";
									comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = item;
									comm.Parameters.Add("@code",SqlDbType.VarChar).Value = processcode;
									comm.Parameters.Add("@com",SqlDbType.VarChar).Value = Num;	//거래처
									comm.CommandText = str;
									int find = int.Parse(comm.ExecuteScalar().ToString());
									comm.Parameters.Clear();
									

									// 동일회사가 아니면 등록
									if(find == 0)
									{
										if(OS_MT_ExistingRecord(conn,tr,item,sequencenum,Num) == false)
										{
											str = "Insert Into OS_MT (ItemNum,ProcessSequenceNum,ProcessCode,BusinessRegistrationNum,RecodingState,[Year]) values(@num,@sequence,@code,@Com,1,@year)";
											comm.CommandText = str;
											comm.Parameters.Add("@num",SqlDbType.VarChar).Value = item;			//품목번호
											comm.Parameters.Add("@sequence",SqlDbType.Int).Value = sequencenum;	//공정순서
											comm.Parameters.Add("@code",SqlDbType.VarChar).Value = processcode;	//공정코드
											comm.Parameters.Add("@Com",SqlDbType.VarChar).Value = Num;	//거래처
											comm.Parameters.Add("@year",DateTime.Now.Year);
											comm.ExecuteNonQuery();
											comm.Parameters.Clear();
										}
										break;
									}
								}
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// 외주창고에 레코드 존재하는지여부
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="ItemNum"></param>
		/// <param name="Sequnce"></param>
		/// <param name="Company"></param>
		/// <returns></returns>
		private bool OS_MT_ExistingRecord(SqlConnection con,SqlTransaction trans,string ItemNum, int Sequnce,string Company)
		{
			string str = "Select count(*) From OS_MT Where RecodingState = 1 and ItemNum = @num and ProcessSequenceNum = @seq and BusinessRegistrationNum = @com and [Year] = @year";
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			comm.Parameters.Add("@num",ItemNum);
			comm.Parameters.Add("@seq",Sequnce);
			comm.Parameters.Add("@com",Company);
			comm.Parameters.Add("@year",DateTime.Now.Year);
			int Exist = int.Parse(comm.ExecuteScalar().ToString());
			comm.Parameters.Clear();
			if(Exist == 0)
				return false;				
			else
				return true;
		}

		private void btHistory_Click(object sender, System.EventArgs e)
		{
			RegisterStartupScript("","<script language='JavaScript'>window.open('UpdateUnitCostHistory.aspx','UpdateUnitCostHistory','width=830,height=600px,resizable=yes');</script>");
		}

		
		
	}
}