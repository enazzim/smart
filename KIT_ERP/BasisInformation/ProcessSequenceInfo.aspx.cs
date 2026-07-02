using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace KIT_ERP.BasisInformation
{
	/// <summary>
	/// ProcessSequenceNumInfor에 대한 요약 설명입니다.
	/// </summary>
	public class ProcessSequenceNumInfor : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button bt_Clear;
		protected System.Web.UI.WebControls.Button bt_Update;
		protected System.Web.UI.WebControls.Button bt_Delete;
		protected System.Web.UI.WebControls.Label lb_ItemNum;
		protected System.Web.UI.WebControls.Label lb_ItemDrawNum;
		protected System.Web.UI.WebControls.Label lb_ItemName;
		protected System.Web.UI.WebControls.Label lb_PropertyClassification;
		protected System.Web.UI.WebControls.Label lb_Unit;
		protected System.Web.UI.WebControls.Label lb_Standard;
		protected System.Web.UI.WebControls.Label lb_ProcessCode;
		protected System.Web.UI.WebControls.DropDownList dl_ProcessCode;
		protected System.Web.UI.WebControls.Label lb_ProcessSequanceNum;
		protected System.Web.UI.WebControls.TextBox tb_PropertyClassification;
		protected System.Web.UI.WebControls.TextBox tb_Unit;
		protected System.Web.UI.WebControls.TextBox tb_Standard;
		protected System.Web.UI.WebControls.Label lb_WorkDistinction;
		protected System.Web.UI.WebControls.DropDownList dl_WorkDistinction;
		protected System.Web.UI.WebControls.Label lb_WCName;
		protected System.Web.UI.WebControls.Label lb_OutsideOrderRate;
		protected System.Web.UI.WebControls.Label lb_ProgressRate;
		protected System.Web.UI.WebControls.Label lb_LeadTime;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Label lb_ItemChoice;
		protected System.Web.UI.WebControls.Label lb_ItemIndex;
		protected System.Web.UI.WebControls.Button bt_ReferanseView;
		protected System.Web.UI.WebControls.Button bt_ProcessSequence;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Index;
		protected System.Web.UI.WebControls.DropDownList dl_WCName;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_ReferenceIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_ReferenceTable;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid Ultrawebgrid2;
		protected System.Web.UI.WebControls.TextBox tb_ProcessSequanceNum;
		protected System.Web.UI.WebControls.TextBox tbOutsideOrderRate;
		protected System.Web.UI.WebControls.TextBox tb_ProgressRate;
		protected System.Web.UI.WebControls.TextBox tb_LeadTime;
		protected System.Web.UI.WebControls.LinkButton LinkButton2;
		protected System.Web.UI.WebControls.Button bt_Registration;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdWCName;
		protected System.Web.UI.WebControls.TextBox tbEtcText;
		protected System.Web.UI.WebControls.Button Button2;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}
			
			//ItemSearchControl1.Commodity = true; //상품 바인딩
			//ItemSearchControl1.RawMaterials = true; //원자재 바인딩	
			//ItemSearchControl1.Phantom = true; //팬텀 바인딩	
			ItemSearchControl1.HalfFinishedProducts =  true; //반제품 바인딩	
			ItemSearchControl1.Products = true; //팬텀 바인딩	

			ItemSearchControl1.IsDoPost = true;

			if(!Page.IsPostBack)
			{
				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 기준정보 > 공정순서정보';</script>");

				bt_Registration.Attributes.Add("onClick", "return OK('입력한 항목을 등록');");
				bt_Update.Attributes.Add("onClick", "return OK('선택한 항목을 수정');");
				bt_Delete.Attributes.Add("onClick", "return OK('선택한 항목을 삭제');");


				tb_ProcessSequanceNum.Attributes.Add("OnKeyDown", "OnKeyDown_Currency(this);");
				tb_ProcessSequanceNum.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				
				tbOutsideOrderRate.Attributes.Add("OnKeyDown", "OnKeyDown_Currency(this);");
				tbOutsideOrderRate.Attributes.Add("OnFocus", "OnFocus_Obj(this);");

				tb_ProgressRate.Attributes.Add("OnKeyDown", "OnKeyDown_Currency(this);");
				tb_ProgressRate.Attributes.Add("OnFocus", "OnFocus_Obj(this);");

				tb_LeadTime.Attributes.Add("OnKeyDown", "OnKeyDown_Currency(this);");
				tb_LeadTime.Attributes.Add("OnFocus", "OnFocus_Obj(this);");

				bt_Clear.Attributes.Add("onClick", "ResetTextBox()");				
			
				page_Lode();
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
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.bt_ProcessSequence.Click += new System.EventHandler(this.bt_ProcessSequence_Click);
			this.dl_WorkDistinction.SelectedIndexChanged += new System.EventHandler(this.dl_WorkDistinction_SelectedIndexChanged);
			this.bt_ReferanseView.Click += new System.EventHandler(this.bt_ReferanseView_Click);
			this.bt_Clear.Click += new System.EventHandler(this.bt_Clear_Click);
			this.bt_Update.Click += new System.EventHandler(this.bt_Update_Click);
			this.bt_Delete.Click += new System.EventHandler(this.bt_Delete_Click);
			this.bt_Registration.Click += new System.EventHandler(this.bt_Registration_Click);
			this.Ultrawebgrid2.DblClick += new Infragistics.WebUI.UltraWebGrid.ClickEventHandler(this.Ultrawebgrid2_DblClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		private void page_Lode()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			
			//	*************************************************
			//	**  WC명 드롭다운리스트... 데이타바인딩... **
			//	*************************************************
			//WC정보테이블에서에서 WC명과 WC정보번호를 가지고옴
			string str_WC = "Select WCName, WCInfoIndex from WCI_MT where RecodingState = 1";
			SqlCommand comm_WC = new SqlCommand(str_WC,conn);
			SqlDataAdapter da_WC = new SqlDataAdapter(comm_WC) ;
			DataSet ds_WC = new DataSet() ;
			da_WC.Fill(ds_WC);
				
			dl_WCName.DataSource = ds_WC;
			dl_WCName.DataTextField = ds_WC.Tables[0].Columns[0].ToString();
			dl_WCName.DataValueField = ds_WC.Tables[0].Columns[0].ToString();
			dl_WCName.DataBind();
			dl_WCName.Items.Insert(0, "-선택-") ;
			dl_WCName.Items[0].Value = "";





			//	*************************************************
			//	**  대표공정 드롭다운리스트... 데이타바인딩... ** 
			//	*************************************************
			//코드분류표에서 대분류명이 단위인 소분류명을 가지고 옴.
			string str_Process = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '1400' and (SmallClassificationCode != '14000000' and SmallClassificationCode != '14009999') order by SmallClassificationName" ;
			SqlCommand comm_Process = new SqlCommand(str_Process,conn);
			SqlDataAdapter da_Process = new SqlDataAdapter(comm_Process) ;
			DataSet ds_Process = new DataSet() ;
			da_Process.Fill(ds_Process);
				
			dl_ProcessCode.DataSource = ds_Process;
			dl_ProcessCode.DataTextField = ds_Process.Tables[0].Columns[0].ToString();
			dl_ProcessCode.DataValueField = ds_Process.Tables[0].Columns[1].ToString();
			dl_ProcessCode.DataBind();
			dl_ProcessCode.Items.Insert(0, "-선택-") ;
			dl_ProcessCode.Items[0].Value = "";
				
			conn.Close();
		}

		/// <summary>
		/// 등록버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Registration_Click(object sender, System.EventArgs e)
		{

			if(ItemSearchControl1.ItemNum.ToString() =="" || ItemSearchControl1.ItemDrawNum.ToString() =="" || tb_ProcessSequanceNum.Text =="" || tb_ProcessSequanceNum.Text.Trim() == "0" ||
				dl_ProcessCode.SelectedIndex == 0 || dl_WorkDistinction.SelectedIndex ==0)
			{
				RegisterStartupScript("","<script>alert('필수항목을 입력해주세요');</script>");
			}
			else if(tb_ProcessSequanceNum.Text.Trim() == "99")
			{
				RegisterStartupScript("","<script>alert('공정순서 99번은 상품이므로 사용할수 없습니다.');</script>");
			}
			else
			{
				if(Page.IsValid == true)
				{
					string m_RequirePage = "ProcessSequenceInfo";
					string m_RequireAction = "Registration";
					string m_TableName = "PSI_MT";
					int m_Idx = (int)PageName.ProcessSequenceInfo;
					string m_User = Session["ID"].ToString();
					string m_RecodeIdx = lb_Index.Value;
			
			
					ArrayList InputList = new ArrayList();


					InputList.Add(ItemSearchControl1.ItemNum.ToString());
					InputList.Add(tb_ProcessSequanceNum.Text);
					InputList.Add(dl_ProcessCode.SelectedItem.Value);
					InputList.Add(dl_WorkDistinction.SelectedItem.Value);
					if(dl_WorkDistinction.SelectedItem.Value == "외주")
						InputList.Add("");
					else
						InputList.Add(dl_WCName.SelectedItem.Value);
					InputList.Add(tbOutsideOrderRate.Text);	
					InputList.Add(tb_ProgressRate.Text);
					InputList.Add(tb_LeadTime.Text);
					InputList.Add(tbEtcText.Text.Trim());

					MasterInfoRecordRUD rud = new MasterInfoRecordRUD(m_RequirePage, m_RequireAction, m_TableName, m_Idx, m_User, InputList,"낱개입력");
					rud.ActivateRUD();

					bt_Update.Enabled = false;
					bt_Delete.Enabled = false;
					bt_Registration.Enabled = true;
				}
				this.DataView();
			}
		}

		

		/// <summary>
		/// 참고보기 버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_ReferanseView_Click(object sender, System.EventArgs e)
		{
			if(lb_Index.Value == "0")
			{
				RegisterStartupScript("","<script>alert('항목을 선택후 버튼을 눌러주세요');</script>");
			}
			else
			{

				string m_TableName = "공정순서정보";
				int m_RecodeIdx = int.Parse(lb_Index.Value);

				Reference rf = new Reference(m_TableName, m_RecodeIdx);
				Ultrawebgrid2.DataSource = rf.reference();
				Ultrawebgrid2.DataBind();
			}		
		}

		/// <summary>
		/// 초기화버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Clear_Click(object sender, System.EventArgs e)
		{
			Clear();
		}

		private void Clear()
		{
			tb_Unit.Text ="";
			tb_Standard.Text ="";
			tb_PropertyClassification.Text = "";
			lb_ItemIndex.Text = "";
			lb_Index.Value = "0";
			tb_ProcessSequanceNum.Text = "";
			dl_ProcessCode.SelectedIndex =0;
			dl_WorkDistinction.SelectedIndex=0;
			tbOutsideOrderRate.Text = "";
			tb_ProgressRate.Text = "0";
			tb_LeadTime.Text = "0";
			tbEtcText.Text = "";
			dl_WCName.SelectedIndex = 0;

			View view = new View("", "ProcessSequenceInfo");
			UltraWebGrid1.DataSource = view.ItemView();
			UltraWebGrid1.DataBind();

			bt_Update.Enabled = false;
			bt_Delete.Enabled = false;
			bt_Registration.Enabled = true;
		}

		/// <summary>
		/// 삭제버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Delete_Click(object sender, System.EventArgs e)
		{
			if(lb_Index.Value =="0")
			{
				RegisterStartupScript("","<script>alert('항목을 선택후 삭제해 주세요');</script>");
			}
			else
			{
				if(Page.IsValid == true)
				{
					string m_RequirePage = "ProcessSequenceInfo";
					string m_RequireAction = "Delete";
					string m_TableName = "PSI_MT";
					int m_Idx = (int)PageName.ProcessSequenceInfo;
					string m_User = Session["ID"].ToString();
							
			
					ArrayList InputList = new ArrayList();

					InputList.Add(ItemSearchControl1.ItemNum.ToString());
					InputList.Add(tb_ProcessSequanceNum.Text);
					InputList.Add(dl_ProcessCode.SelectedItem.Value);
					InputList.Add(dl_WorkDistinction.SelectedItem.Value);
					InputList.Add(dl_WCName.SelectedItem.Value);
					InputList.Add(tbOutsideOrderRate.Text);	
					InputList.Add(tb_ProgressRate.Text);
					InputList.Add(tb_LeadTime.Text);
					InputList.Add(tbEtcText.Text.Trim());
					InputList.Add(lb_Index.Value);

					MasterInfoRecordRUD rud = new MasterInfoRecordRUD(m_RequirePage, m_RequireAction, m_TableName, m_Idx, m_User, InputList,"낱개입력");
					rud.ActivateRUD();

					bt_Update.Enabled = false;
					bt_Delete.Enabled = false;
					bt_Registration.Enabled = true;
				}
				Clear();
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

			if(lb_Index.Value =="0")
			{
				RegisterStartupScript("","<script>alert('항목을 선택후 수정해 주세요');</script>");
			}
			else
			{
				if(Page.IsValid == true)
				{
					string m_RequirePage = "ProcessSequenceInfo";
					string m_RequireAction = "Update";
					string m_TableName = "PSI_MT";
					int m_Idx = (int)PageName.ProcessSequenceInfo;
					string m_User = Session["ID"].ToString();
					
			
					ArrayList InputList = new ArrayList();

					InputList.Add(ItemSearchControl1.ItemNum.ToString());
					InputList.Add(tb_ProcessSequanceNum.Text);
					InputList.Add(dl_ProcessCode.SelectedItem.Value);
					InputList.Add(dl_WorkDistinction.SelectedItem.Value);
					if(dl_WorkDistinction.SelectedItem.Value == "외주")
						InputList.Add("");
					else
					{
						if(dl_WCName.SelectedItem.Value.Trim() == "")
							InputList.Add(hdWCName.Value);
							
						else
							InputList.Add(dl_WCName.SelectedItem.Value);
					}
					
					InputList.Add(tbOutsideOrderRate.Text);	
					InputList.Add(tb_ProgressRate.Text);
					InputList.Add(tb_LeadTime.Text);
					InputList.Add(tbEtcText.Text.Trim());
					InputList.Add(lb_Index.Value);



					MasterInfoRecordRUD rud = new MasterInfoRecordRUD(m_RequirePage, m_RequireAction, m_TableName, m_Idx, m_User, InputList,"낱개입력");
					rud.ActivateRUD();

					bt_Update.Enabled = false;
					bt_Delete.Enabled = false;
					bt_Registration.Enabled = true;
				}
				this.DataView();

			}
		}


		/// <summary>
		/// 공정순서보기 버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_ProcessSequence_Click(object sender, System.EventArgs e)
		{
			if(ItemSearchControl1.ItemNum.ToString() == "")
			{
				RegisterStartupScript("","<script>alert('품목을 입력해 주세요');</script>");
			}
			else 
			{
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				conn.Open();
				string str = "Select * From II_MT Where RecodingState = 1 and ItemNum = @num ";
				SqlCommand comm = new SqlCommand(str,conn);
				comm.Parameters.Add("@num",SqlDbType.VarChar).Value = Convert.ToString(ItemSearchControl1.ItemNum.ToString());
				SqlDataReader dr = comm.ExecuteReader();
				while(dr.Read())
				{
					
					tb_PropertyClassification.Text = dr["PropertyClassification"].ToString();
					tb_Unit.Text = dr["Unit"].ToString();
					tb_Standard.Text = dr["Standard"].ToString();
					lb_ItemIndex.Text = dr["ItemInfoIndex"].ToString();
				}
				dr.Close();

				string str1 = "Select SmallClassificationName from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and SmallClassificationCode = @code" ;
				SqlCommand comm1 = new SqlCommand(str1,conn);
				comm1.Parameters.Add("@code",SqlDbType.VarChar).Value = tb_Unit.Text;
				SqlDataReader dr1 = comm1.ExecuteReader();
				while(dr1.Read())
				{
					tb_Unit.Text = dr1["SmallClassificationName"].ToString();
				}
				conn.Close();

				this.DataView();
				bt_Update.Enabled = false;
				bt_Delete.Enabled = false;
				bt_Registration.Enabled = true;
			}
			
		}

		private void dl_WorkDistinction_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//Response.Write("<script>alert('"+lb_Index.Value +"');</script>");
			if(lb_Index.Value !="0")
			{
				bt_Update.Enabled = true;
				bt_Delete.Enabled = true;
				bt_Registration.Enabled = false;
			}
			else
			{
				bt_Update.Enabled = false;
				bt_Delete.Enabled = false;
				bt_Registration.Enabled = true;
			}

			
			if(dl_WorkDistinction.SelectedIndex == 1)//자가이면 
			{
				//자가이면 발주비율 비활성화
				//			WC 활성화
				//			리드타임 비활성화
				tbOutsideOrderRate.Text = "0";
				tbOutsideOrderRate.Enabled = false;
				dl_WCName.SelectedIndex = 0;
				dl_WCName.Enabled = true;
				tb_LeadTime.Text = "0";
				tb_LeadTime.Enabled = false;
			}
			else if(dl_WorkDistinction.SelectedIndex == 2)
			{
				//외주이면 발주비율 비활성화
				//			WC 비활성화
				//			리드타임 활성화
				tbOutsideOrderRate.Text = "100";
				tbOutsideOrderRate.Enabled = false;
				dl_WCName.SelectedIndex = 0;
				dl_WCName.Enabled = false;
				tb_LeadTime.Enabled = true;
			}
			else if(dl_WorkDistinction.SelectedIndex == 3)
			{
				//자가/외주이면 발주비율 활성화
				//			WC 활성화
				//			리드타임 비활성화
				tbOutsideOrderRate.Text = "50";
				tbOutsideOrderRate.Enabled = true;
				//dl_WCName.SelectedIndex = 0;
				dl_WCName.Enabled = true;
				tb_LeadTime.Enabled = true;
			}
			


		}

		private void DataView()
		{
			View view = new View(ItemSearchControl1.ItemNum.ToString(), "ProcessSequenceInfo");
			UltraWebGrid1.DataSource = view.ItemView();
			UltraWebGrid1.DataBind();
		}

		/// <summary>
		/// 어깨창 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LinkButton1_Click(object sender, System.EventArgs e)
		{
			string txt = lb_ItemIndex.Text;
			string txtID = "lb_ItemIndex";
			string formID = "ProcessSequence.aspx" ;
			RegisterStartupScript("","<script>window.open('./Popup/ProcessSequence.aspx?formID=" + formID + "&txtID=" + txtID + "&txt=" + txt + "','ProcessSequence'," + "'width=830" + "," + "height=500px');</script>");
		}

		

		/// <summary>
		/// 품번과 동일한 품목이 있으면 뿌려주고 그렇지 않으면 에러메시지 출력
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
//		private void tb_ItemNum_TextChanged(object sender, System.EventArgs e)
//		{
//			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
//			conn.Open();
//			string str = "Select ItemNum, ItemDrawNum, ItemName, PropertyClassification, Unit, Standard, ItemInfoIndex From II_MT Where PropertyClassification != '원자재' and RecodingState = 1 and ItemNum = @num";
//			SqlCommand comm = new SqlCommand(str,conn);
//			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = ItemSearchControl1.ItemNum.ToString();
//			SqlDataAdapter da = new SqlDataAdapter(comm);
//			DataSet ds = new DataSet();
//			da.Fill(ds);
//
//			if(ds.Tables[0].Rows.Count != 0)
//			{
//				
//				tb_ItemNum.Text = ds.Tables[0].Rows[0]["ItemNum"].ToString();
//				tb_ItemDrawNum.Text = ds.Tables[0].Rows[0]["ItemDrawNum"].ToString();
//				tb_ItemName.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
//				tb_PropertyClassification.Text = ds.Tables[0].Rows[0]["PropertyClassification"].ToString();
//				tb_Unit.Text = ds.Tables[0].Rows[0]["Unit"].ToString();
//				tb_Standard.Text = ds.Tables[0].Rows[0]["Standard"].ToString();
//				lb_ItemIndex.Text = ds.Tables[0].Rows[0]["ItemInfoIndex"].ToString();
//
//				string str1 = "Select SmallClassificationName from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and SmallClassificationCode = @code" ;
//				SqlCommand comm1 = new SqlCommand(str1,conn);
//				comm1.Parameters.Add("@code",SqlDbType.VarChar).Value = tb_Unit.Text;
//				SqlDataReader dr1 = comm1.ExecuteReader();
//				while(dr1.Read())
//				{
//					tb_Unit.Text = dr1["SmallClassificationName"].ToString();
//				}
//				dr1.Close();
//				
//			}
//			else
//			{
//				RegisterStartupScript("","<script>alert('품번을 정확히 입력해 주세요');</script>");
//
//				tb_ItemDrawNum.Text = "";
//				tb_ItemName.Text = "";
//				tb_Unit.Text ="";
//				tb_Standard.Text ="";
//				tb_PropertyClassification.Text = "";
//				lb_ItemIndex.Text = "";
//			}
//		
//			conn.Close();
//
//		}

		/// <summary>
		/// 팝업버튼클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
//		private void pop_Click(object sender, System.EventArgs e)
//		{
//			// 품목명찾기 팝업창 띄우기
//			string txt = Textbox1.Text ;
//			string formID = "ProcessSequenceNumInfo" ;
//			string txtID = "ItemIndex";
//			RegisterStartupScript("","<script>window.open('./Popup/Item.aspx?formID=" + formID + "&txtID=" + txtID +  "&txt=" + txt + "','Item'," + "'width=800" + "," + "height=400px');</script>");
//		}

		/// <summary>
		/// 참고보기 그리드 더블클릭시
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

		private void LinkButton2_Click(object sender, System.EventArgs e)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select ItemNum, ItemDrawNum,ItemName, PropertyClassification, Unit, Standard, ChargePerson From II_MT Where ItemNum = @num and (PropertyClassification = '제품' or PropertyClassification = '팬텀' or PropertyClassification = '상품' or PropertyClassification = '반제품' or PropertyClassification = '원자재') and RecodingState = 1";
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

		private void Button2_Click(object sender, System.EventArgs e)
		{
			RegisterStartupScript("","<script language='JavaScript'>window.open('CopyProcessSequenceInfo.aspx','CopyProcessSequenceInfo','width=810,height=100px,resizable=yes, status=yes');</script>");
		}
	}
}
