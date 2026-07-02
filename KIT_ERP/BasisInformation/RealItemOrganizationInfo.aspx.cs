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
	/// RealItemOrganizationInfo에 대한 요약 설명입니다.
	/// </summary>
	public class RealItemOrganizationInfo : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lb_ItemIndex;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		protected System.Web.UI.WebControls.TextBox tb_ParentPropertyClassification;
		protected System.Web.UI.WebControls.TextBox tb_ParentUnit;
		protected System.Web.UI.WebControls.TextBox tb_ParentStandard;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Button bt_Expansion;
		protected System.Web.UI.WebControls.Button bt_ExpansionView;
		protected System.Web.UI.WebControls.LinkButton LinkButton2;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Label lb_ItemOrganization;
		protected System.Web.UI.WebControls.Label lb_ItemIndex1;
		protected System.Web.UI.WebControls.LinkButton LinkButton3;
		protected System.Web.UI.WebControls.TextBox tb_ChildPropertyClassification;
		protected System.Web.UI.WebControls.TextBox tb_ChildUnit;
		protected System.Web.UI.WebControls.TextBox tb_ChildStandard;
		protected System.Web.UI.WebControls.TextBox tb_NeedQuantityNumerator;
		protected System.Web.UI.WebControls.TextBox tb_NeedQuantityDenominator;
		protected System.Web.UI.WebControls.DropDownList dl_ProcessManagement;
		protected System.Web.UI.WebControls.DropDownList dl_SubDivision;
		protected System.Web.UI.WebControls.DropDownList dl_SupplyDivision;
		protected System.Web.UI.WebControls.DropDownList dl_BOMUnit;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcBeginDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcEndDate;
		protected System.Web.UI.WebControls.Button bt_Reference;
		protected System.Web.UI.WebControls.Button bt_Clear;
		protected System.Web.UI.WebControls.Button bt_Update;
		protected System.Web.UI.WebControls.Button bt_Delete;
		protected System.Web.UI.WebControls.Button bt_Registration;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid Ultrawebgrid2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden ItemIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Hidden1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Index;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_ReferenceIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_ReferenceTable;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdItemNum;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected System.Web.UI.WebControls.TextBox tbUpdateReason;
		protected System.Web.UI.WebControls.Button btHistory;
		protected System.Web.UI.WebControls.Button Button2;
		protected KIT_ERP.BasisInformation.ItemSearch.ItemSearch ItemSearch1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			ItemSearchControl1.HalfFinishedProducts =  true; //반제품 바인딩	
			ItemSearchControl1.Products = true; //제품 바인딩	
			ItemSearchControl1.IsDoPost = true;


			ItemSearch1.ChildRawMaterials = true; //원자재 바인딩	
			ItemSearch1.ChildHalfFinishedProducts =  true; //반제품 바인딩	
			ItemSearch1.ChildIsDoPost = true;
			
			ItemSearchControl1.Phantom = true;	// 팬텀 바인딩
			ItemSearch1.ChildProducts = true;		// 자품목 제품

			
			if(!Page.IsPostBack)
			{
				////Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 기준정보 > 품목구성정보';</script>");

				bt_Registration.Attributes.Add("onClick", "return OK('입력한 항목을 등록');");
				bt_Update.Attributes.Add("onClick", "return OK('선택한 항목을 수정');");
				bt_Delete.Attributes.Add("onClick", "return OK('선택한 항목을 삭제');");
				
				tb_NeedQuantityNumerator.Attributes.Add("OnKeyDown", "OnKeyDown_Currency(this);");
				tb_NeedQuantityNumerator.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				
				tb_NeedQuantityDenominator.Attributes.Add("OnKeyDown", "OnKeyDown_Currency(this);");
				tb_NeedQuantityDenominator.Attributes.Add("OnFocus", "OnFocus_Obj(this);");

				bt_Clear.Attributes.Add("onClick", "ResetTextBox();ResetBox();");

				page_Load();
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
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.bt_Expansion.Click += new System.EventHandler(this.bt_Expansion_Click);
			this.bt_ExpansionView.Click += new System.EventHandler(this.bt_ExpansionView_Click);
			this.LinkButton2.Click += new System.EventHandler(this.LinkButton2_Click);
			this.LinkButton3.Click += new System.EventHandler(this.LinkButton3_Click);
			this.btHistory.Click += new System.EventHandler(this.btHistory_Click);
			this.bt_Reference.Click += new System.EventHandler(this.bt_Reference_Click);
			this.bt_Clear.Click += new System.EventHandler(this.bt_Clear_Click);
			this.bt_Update.Click += new System.EventHandler(this.bt_Update_Click);
			this.bt_Delete.Click += new System.EventHandler(this.bt_Delete_Click);
			this.bt_Registration.Click += new System.EventHandler(this.bt_Registration_Click);
			this.Ultrawebgrid2.DblClick += new Infragistics.WebUI.UltraWebGrid.ClickEventHandler(this.Ultrawebgrid2_DblClick);
			this.ID = "RealItemOrganizationInfo";
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void page_Load()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			
			

			//	*************************************************
			//	**  BOM단위 드롭다운리스트... 데이타바인딩... **		SupplyDivision
			//	*************************************************
			//코드분류표에서 대분류명이 단위인 소분류명을 가지고 옴.
			string str_BOMUnit = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '0520'" ;
			SqlCommand comm_BOMUnit = new SqlCommand(str_BOMUnit,conn);
			SqlDataAdapter da_BOMUnit = new SqlDataAdapter(comm_BOMUnit) ;
			DataSet ds_BOMUnit = new DataSet() ;
			da_BOMUnit.Fill(ds_BOMUnit);
				
			dl_BOMUnit.DataSource = ds_BOMUnit;
			dl_BOMUnit.DataTextField = ds_BOMUnit.Tables[0].Columns[0].ToString();
			dl_BOMUnit.DataValueField = ds_BOMUnit.Tables[0].Columns[1].ToString();
			dl_BOMUnit.DataBind();
			dl_BOMUnit.Items.Insert(0, "-선택-") ;
			dl_BOMUnit.Items[0].Value = "";


			//	*************************************************
			//	**  조달도면 드롭다운리스트... 데이타바인딩... **		SupplyDivision
			//	*************************************************
			//코드분류표에서 대분류명이 조달도면인 소분류명을 가지고 옴.
			string str_SupplyDivision = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '0640'" ;
			SqlCommand comm_SupplyDivision = new SqlCommand(str_SupplyDivision,conn);
			SqlDataAdapter da_SupplyDivision = new SqlDataAdapter(comm_SupplyDivision) ;
			DataSet ds_SupplyDivision = new DataSet() ;
			da_SupplyDivision.Fill(ds_SupplyDivision);
				
			dl_SupplyDivision.DataSource = ds_SupplyDivision;
			dl_SupplyDivision.DataTextField = ds_SupplyDivision.Tables[0].Columns[0].ToString();
			dl_SupplyDivision.DataValueField = ds_SupplyDivision.Tables[0].Columns[1].ToString();
			dl_SupplyDivision.DataBind();
			dl_SupplyDivision.Items.Insert(0, "-선택-") ;
			dl_SupplyDivision.Items[0].Value = "";

			conn.Close();

		}

		private void bt_ExpansionView_Click(object sender, System.EventArgs e)
		{
			//if(lb_ItemIndex.Text == "")
			if(ItemSearchControl1.ItemNum.ToString() == "")
			{
				RegisterStartupScript("","<script language='JavaScript'>alert('모품목을 입력해주세요.');</script>");
			}
			else
			{
				View view = new View(ItemSearchControl1.ItemNum.ToString(), "RealItemOrganizationInfo");
				UltraWebGrid1.DataSource = view.ItemView();
				UltraWebGrid1.DataBind();

				//자품목쪽 Clear
				
				clear();
			}
		}


		private bool Validator()
		{
			if(tb_ChildPropertyClassification.Text.Trim() == "")
				return false;
			else
				return true;
		}

		private void bt_Clear_Click(object sender, System.EventArgs e)
		{
			//모품목쪽 Clear
		
			tb_ParentPropertyClassification.Text = "";
			tb_ParentUnit.Text = "";
			tb_ParentStandard.Text = "";
			lb_ItemIndex.Text = "";
			lb_ItemIndex1.Text = "";

			View view = new View("", "ItemOrganizationInfo");
			UltraWebGrid1.DataSource = view.ItemView();
			UltraWebGrid1.DataBind();

			//자품목쪽 Clear

			clear();
		}

		private void bt_Registration_Click(object sender, System.EventArgs e)
		{
			if(tb_NeedQuantityNumerator.Text == "0" || tb_NeedQuantityNumerator.Text.Trim() =="")
			{
				RegisterStartupScript("","<script language='JavaScript'>alert('분자량을 입력해주세요.');</script>");
			}
			else if(tb_NeedQuantityDenominator.Text == "0" || tb_NeedQuantityDenominator.Text.Trim() =="")
			{
				RegisterStartupScript("","<script language='JavaScript'>alert('분모량을 입력해주세요.');</script>");
			}
			else
			{
				
				string m_RequirePage = "RealItemOrganizationInfo";
				string m_RequireAction = "Registration";
				string m_TableName = "RIOI_MT";
				int m_Idx = (int)PageName.RealItemOrganizationInfo;
				string m_User = Session["ID"].ToString();
						
			
				ArrayList InputList = new ArrayList();

			

				InputList.Add(ItemSearchControl1.ItemNum.ToString());
				InputList.Add(ItemSearch1.ChildItemNum.ToString());
				InputList.Add(tb_NeedQuantityNumerator.Text);
				InputList.Add(tb_NeedQuantityDenominator.Text);
				InputList.Add(dl_ProcessManagement.SelectedItem.Value);
				InputList.Add(dl_SubDivision.SelectedItem.Value);
				InputList.Add(dl_SupplyDivision.SelectedItem.Value);
				InputList.Add(dl_BOMUnit.SelectedItem.Value);
				InputList.Add(wdcBeginDate.Text);
				Session["Reason"] = "신규등록";

				if(wdcEndDate.Text.Trim() == "")
					InputList.Add("2076-06-06");
				else
					InputList.Add(wdcEndDate.Text);


				if(ItemSearchControl1.ItemNum.ToString().Trim() == ItemSearch1.ChildItemNum.ToString().Trim())
				{
					RegisterStartupScript("","<script language='JavaScript'>alert('모품목과 자품목이 동일합니다!');</script>");
				}
				else
				{
					if(Validator())
					{
						MasterInfoRecordRUD rud = new MasterInfoRecordRUD(m_RequirePage, m_RequireAction, m_TableName, m_Idx, m_User, InputList,"낱개입력");
						rud.ActivateRUD();
				
						this.databind();
					}
					else
					{
						RegisterStartupScript("","<script language='JavaScript'>alert('자품목을 정확히 입력하세요!');</script>");
					}

				}
			}
		}

		private void LinkButton1_Click(object sender, System.EventArgs e)
		{
			RegisterStartupScript("","<script language='JavaScript'>window.open('./Popup/RealItemOrganization.aspx','RealItemOrganization'," + "'width=830" + "," + "height=500px');</script>");
		}

		private void bt_Expansion_Click(object sender, System.EventArgs e)
		{
			string ItemNum = ItemSearchControl1.ItemNum.ToString();

			if(ItemNum == "")
			{
				RegisterStartupScript("","<script language='JavaScript'>alert('항목을 선택후 버튼을 눌러주세요.');</script>");
			}
			else
			{
				//RegisterStartupScript("","<script language='JavaScript'>window.open('./Popup/RealBom.aspx?ItemNum=" + ItemNum + "','RealBOM_Tree','width=610,height=500px,resizable=yes');</script>");
				RegisterStartupScript("","<script language='JavaScript'>window.open('./Popup/Bom.aspx?ItemNum=" + ItemNum + "','RealBOM_Tree','width=810,height=500px,resizable=yes');</script>");
			}
		}

		private void bt_Update_Click(object sender, System.EventArgs e)
		{
			if(tb_NeedQuantityNumerator.Text == "0" || tb_NeedQuantityNumerator.Text.Trim() =="")
			{
				RegisterStartupScript("","<script language='JavaScript'>alert('분자량을 입력해주세요.');</script>");
			}
			else if(tb_NeedQuantityDenominator.Text == "0" || tb_NeedQuantityDenominator.Text.Trim() =="")
			{
				RegisterStartupScript("","<script language='JavaScript'>alert('분모량을 입력해주세요.');</script>");
			}
			else
			{
				
				string m_RequirePage = "RealItemOrganizationInfo";
				string m_RequireAction = "Update";
				string m_TableName = "RIOI_MT";
				int m_Idx = (int)PageName.RealItemOrganizationInfo;
				string m_User = Session["ID"].ToString();
				string m_RecodeIdx = lb_ItemOrganization.Text;
			
			
				ArrayList InputList = new ArrayList();

			

				InputList.Add(ItemSearchControl1.ItemNum.ToString());
				InputList.Add(hdItemNum.Value);
				InputList.Add(tb_NeedQuantityNumerator.Text);
				InputList.Add(tb_NeedQuantityDenominator.Text);
				InputList.Add(dl_ProcessManagement.SelectedItem.Value);
				InputList.Add(dl_SubDivision.SelectedItem.Value);
				InputList.Add(dl_SupplyDivision.SelectedItem.Value);
				InputList.Add(dl_BOMUnit.SelectedItem.Value);
				InputList.Add(wdcBeginDate.Text);
				if(wdcEndDate.Text.Trim() == "")
					InputList.Add("2076-06-06");
				else
					InputList.Add(wdcEndDate.Text);
				InputList.Add(lb_Index.Value);
				Session["Reason"] = tbUpdateReason.Text.Trim();


				if(ItemSearchControl1.ItemNum.ToString().Trim() == hdItemNum.Value.ToString().Trim())
				{
					RegisterStartupScript("","<script language='JavaScript'>alert('모품목과 자품목이 동일합니다!');</script>");
				}
				else
				{
					MasterInfoRecordRUD rud = new MasterInfoRecordRUD(m_RequirePage, m_RequireAction, m_TableName, m_Idx, m_User, InputList,"기준정보");
					rud.ActivateRUD();
					this.databind();
				}
			
				
			}
		}

		private void bt_Delete_Click(object sender, System.EventArgs e)
		{
			string m_RequirePage = "RealItemOrganizationInfo";
			string m_RequireAction = "Delete";
			string m_TableName = "RIOI_MT";
			int m_Idx = (int)PageName.RealItemOrganizationInfo;
			string m_User = Session["ID"].ToString();
			string m_RecodeIdx = lb_ItemOrganization.Text;
			
			
			ArrayList InputList = new ArrayList();

			

			InputList.Add(ItemSearchControl1.ItemNum.ToString());
			InputList.Add(hdItemNum.Value);
			InputList.Add(tb_NeedQuantityNumerator.Text);
			InputList.Add(tb_NeedQuantityDenominator.Text);
			InputList.Add(dl_ProcessManagement.SelectedItem.Value);
			InputList.Add(dl_SubDivision.SelectedItem.Value);
			InputList.Add(dl_SupplyDivision.SelectedItem.Value);
			InputList.Add(dl_BOMUnit.SelectedItem.Value);
			InputList.Add(wdcBeginDate.Text);
			InputList.Add(wdcEndDate.Text);
			InputList.Add(lb_Index.Value);
			Session["Reason"] = "삭제";


			MasterInfoRecordRUD rud = new MasterInfoRecordRUD(m_RequirePage, m_RequireAction, m_TableName, m_Idx, m_User, InputList,"기준정보");
			rud.ActivateRUD();
			
			this.databind();

			clear();
		}

		private void LinkButton3_Click(object sender, System.EventArgs e)
		{
			hdItemNum.Value = ItemSearch1.ChildItemNum;
		
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select ItemNum, ItemDrawNum,ItemName, PropertyClassification, Unit, Standard, ChargePerson From II_MT Where ItemNum = @num and RecodingState = 1";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = ItemSearch1.ChildItemNum;
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				tb_ChildPropertyClassification.Text = dr["PropertyClassification"].ToString();
				tb_ChildUnit.Text = dr["Unit"].ToString();
				tb_ChildStandard.Text = dr["Standard"].ToString();
				
			}
			dr.Close();
			
			//단위 입력
			string str1 = "Select SmallClassificationName from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and SmallClassificationCode = @code" ;
			SqlCommand comm1 = new SqlCommand(str1,conn);
			comm1.Parameters.Add("@code",SqlDbType.VarChar).Value = tb_ChildUnit.Text;
			SqlDataReader dr1 = comm1.ExecuteReader();
			while(dr1.Read())
			{
				tb_ChildUnit.Text = dr1["SmallClassificationName"].ToString();
			}
			dr1.Close();
			conn.Close();
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			RegisterStartupScript("","<script language='JavaScript'>window.open('./Popup/RealReversalBOM.aspx','RealReversalBOM','width=810,height=500px,resizable=yes');</script>");
		}

		private void bt_Reference_Click(object sender, System.EventArgs e)
		{
			if(lb_Index.Value == "0")
			{
				RegisterStartupScript("","<script language='JavaScript'>alert('항목을 선택후 버튼을 눌러주세요.');</script>");
			}
			else
			{
				string m_TableName = "진품목구성정보";
				int m_RecodeIdx = int.Parse(lb_Index.Value);
			

				Reference rf = new Reference(m_TableName, m_RecodeIdx);
				Ultrawebgrid2.DataSource = rf.reference();
				Ultrawebgrid2.DataBind();
			}
		}

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
			
			//RegisterStartupScript("","<script language='JavaScript'>window.open('./Popup/View.aspx?table=" + table + "&index=" + index + "','Reference'," + "'width=510" + "," + "height=435px');</script>");
			RegisterStartupScript("","<script>window.open('./Popup/View1.aspx?table=" + table + "&index=" + index + "','Reference'," + "'width=818" + "," + "height=610px');</script>");
		}

		/// <summary>
		/// 아래 자품목부분 초기화 함수
		/// </summary>
		private void clear()
		{
            
			tb_ChildPropertyClassification.Text = "";
			tb_ChildUnit.Text = "";
			tb_ChildStandard.Text = "";
			lb_Index.Value = "0";
			tb_NeedQuantityNumerator.Text = "0";
			tb_NeedQuantityDenominator.Text = "1";
			dl_ProcessManagement.SelectedIndex = 0;
			dl_SubDivision.SelectedIndex = 0;
			dl_SupplyDivision.SelectedIndex = 0;
			dl_BOMUnit.SelectedIndex = 0;

			wdcBeginDate.Value = "";
			wdcEndDate.Value = "";
		}

		/// <summary>
		/// 웹그리드에 바인딩해주는 함수
		/// </summary>
		private void databind()
		{
			string m_itemnum = ItemSearchControl1.ItemNum.ToString();
			string m_table = "RIOI_MT";
			string m_page = "RealItemOrganizationInfo";

			View view = new View(m_itemnum, m_table, m_page);
			UltraWebGrid1.DataSource = view.ItemView();
			UltraWebGrid1.DataBind();
		}

		private void btHistory_Click(object sender, System.EventArgs e)
		{
			RegisterStartupScript("","<script language='JavaScript'>window.open('UpdateRealItemOrganizationInfoHistory.aspx','UpdateRealItemOrganizationInfoHistory','width=830,height=600px,resizable=yes');</script>");
		}

		private void Button2_Click(object sender, System.EventArgs e)
		{
			RegisterStartupScript("","<script language='JavaScript'>window.open('CopyRealBOM.aspx','CopyRealBOM','width=810,height=100px,resizable=yes, status=yes');</script>");
		}

		private void LinkButton2_Click(object sender, System.EventArgs e)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			//string str = "Select ItemNum, ItemDrawNum,ItemName, PropertyClassification, Unit, Standard, ChargePerson From II_MT Where ItemNum = @num and (PropertyClassification = '제품' or PropertyClassification = '팬텀' or PropertyClassification = '상품' or PropertyClassification = '반제품' or PropertyClassification = '원자재') and RecodingState = 1";
			string str = "Select ItemNum, ItemDrawNum,ItemName, PropertyClassification, Unit, Standard, ChargePerson From II_MT Where ItemNum = @num and RecodingState = 1";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = ItemSearchControl1.ItemNum;
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				tb_ParentPropertyClassification.Text = dr["PropertyClassification"].ToString();
				tb_ParentUnit.Text = dr["Unit"].ToString();
				tb_ParentStandard.Text = dr["Standard"].ToString();
				
			}
			dr.Close();
			
			//단위 입력
			string str1 = "Select SmallClassificationName from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and SmallClassificationCode = @code" ;
			SqlCommand comm1 = new SqlCommand(str1,conn);
			comm1.Parameters.Add("@code",SqlDbType.VarChar).Value = tb_ParentUnit.Text;
			SqlDataReader dr1 = comm1.ExecuteReader();
			while(dr1.Read())
			{
				tb_ParentUnit.Text = dr1["SmallClassificationName"].ToString();
			}
			dr1.Close();
			conn.Close();
		}
	}
}
