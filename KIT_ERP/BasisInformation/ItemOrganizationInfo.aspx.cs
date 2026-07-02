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
using System.IO;
using System.Data.OleDb;
using Microsoft.Win32;

namespace KIT_ERP.BasisInformation
{
	/// <summary>
	/// ItemOrganizationInfo에 대한 요약 설명입니다.
	/// </summary>
	public class ItemOrganizationInfo : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button bt_Delete;
		protected System.Web.UI.WebControls.Button bt_Clear;
		protected System.Web.UI.WebControls.Button bt_Registration;
		protected System.Web.UI.WebControls.Button bt_Update;
		protected System.Web.UI.WebControls.Label Label30;
		protected System.Web.UI.WebControls.Label Label31;
		protected System.Web.UI.WebControls.Label lb_ParentItemNum;
		protected System.Web.UI.WebControls.Label lb_ParentItemDrawNum;
		protected System.Web.UI.WebControls.Label lb_ParentItemName;
		protected System.Web.UI.WebControls.Label lb_ParentPropertyClassification;
		protected System.Web.UI.WebControls.Label lb_ParentUnit;
		protected System.Web.UI.WebControls.Label lb_ParentStandard;
		protected System.Web.UI.WebControls.Label lb_ChildItemNum;
		protected System.Web.UI.WebControls.Label lb_ChildItemDrawNum;
		protected System.Web.UI.WebControls.Label lb_ChildItemName;
		protected System.Web.UI.WebControls.Label lb_ChildPropertyClassification;
		protected System.Web.UI.WebControls.Label lb_ChildUnit;
		protected System.Web.UI.WebControls.Label lb_ChildStandard;
		protected System.Web.UI.WebControls.Label lb_NeedQuantityNumerator;
		protected System.Web.UI.WebControls.Label lb_NeedQuantityDenominator;
		protected System.Web.UI.WebControls.TextBox tb_ParentPropertyClassification;
		protected System.Web.UI.WebControls.TextBox tb_ParentUnit;
		protected System.Web.UI.WebControls.TextBox tb_ParentStandard;
		protected System.Web.UI.WebControls.TextBox tb_ChildPropertyClassification;
		protected System.Web.UI.WebControls.TextBox tb_ChildUnit;
		protected System.Web.UI.WebControls.TextBox tb_ChildStandard;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		protected System.Web.UI.WebControls.Label lb_ProcessManagement;
		protected System.Web.UI.WebControls.DropDownList dl_ProcessManagement;
		protected System.Web.UI.WebControls.Label lb_SubDivision;
		protected System.Web.UI.WebControls.DropDownList dl_SubDivision;
		protected System.Web.UI.WebControls.Label lb_SupplyDivision;
		protected System.Web.UI.WebControls.DropDownList dl_SupplyDivision;
		protected System.Web.UI.WebControls.Label lb_BOMUnit;
		protected System.Web.UI.WebControls.DropDownList dl_BOMUnit;
		protected System.Web.UI.WebControls.Label lb_BeginDate;
		protected System.Web.UI.WebControls.Label lb_EndDate;
		protected System.Web.UI.WebControls.Label lb_ItemIndex;
		protected System.Web.UI.WebControls.Label lb_ItemIndex1;
		protected System.Web.UI.WebControls.Button bt_Expansion;
		protected System.Web.UI.WebControls.Button bt_ExpansionView;
		protected System.Web.UI.WebControls.Button bt_Reference;
		protected System.Web.UI.WebControls.Label lb_ItemOrganization;
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Index;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_ReferenceIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_ReferenceTable;
		protected System.Web.UI.HtmlControls.HtmlInputHidden ItemIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Hidden1;
		protected System.Web.UI.WebControls.TextBox tb_NeedQuantityNumerator;
		protected System.Web.UI.WebControls.TextBox tb_NeedQuantityDenominator;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcBeginDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcEndDate;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid Ultrawebgrid2;
		protected System.Web.UI.WebControls.LinkButton LinkButton2;		
		protected System.Web.UI.WebControls.LinkButton LinkButton3;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdItemNum;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.TextBox tbUpdateReason;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected System.Web.UI.WebControls.Button btHistory;
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.Button tbTotalUpdate;
		protected System.Web.UI.HtmlControls.HtmlInputFile File1;
		protected KIT_ERP.BasisInformation.ItemSearch.ItemSearch ItemSearch1;
		private string m_UserName = "";
		private DataSet dataset = new DataSet();
	
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
			this.tbTotalUpdate.Click += new System.EventHandler(this.tbTotalUpdate_Click);
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
			//	**  조달구분 드롭다운리스트... 데이타바인딩... **		SupplyDivision
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

		/// <summary>
		/// 등록버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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
				
				string m_RequirePage = "ItemOrganizationInfo";
				string m_RequireAction = "Registration";
				string m_TableName = "IOI_MT";
				int m_Idx = (int)PageName.ItemOrganizationInfo;
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

				if(wdcEndDate.Text.Trim() == "")
					InputList.Add("2076-06-06");
				else
					InputList.Add(wdcEndDate.Text);

				Session["Reason"] = "신규등록";

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

		private bool Validator()
		{

			if(tb_ChildPropertyClassification.Text.Trim() == "")
				return false;
			else
				return true;
		}

		/// <summary>
		/// 초기화버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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

		
		/// <summary>
		/// 소요자재보기 버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_ExpansionView_Click(object sender, System.EventArgs e)
		{
			//if(lb_ItemIndex.Text == "")
			if(ItemSearchControl1.ItemNum.ToString() == "")
			{
				RegisterStartupScript("","<script language='JavaScript'>alert('모품목을 입력해주세요.');</script>");
			}
			else
			{
				View view = new View(ItemSearchControl1.ItemNum.ToString(), "ItemOrganizationInfo");
				UltraWebGrid1.DataSource = view.ItemView();
				UltraWebGrid1.DataBind();

				//자품목쪽 Clear
				
				clear();
			}
		}

		/// <summary>
		/// 어깨창 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LinkButton1_Click(object sender, System.EventArgs e)
		{
			RegisterStartupScript("","<script language='JavaScript'>window.open('./Popup/ItemOrganization.aspx','ItemOrganization'," + "'width=830" + "," + "height=500px');</script>");
		
		}

		

		/// <summary>
		/// 수정버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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
			else if(tbUpdateReason.Text.Trim() == "")
				RegisterStartupScript("","<script language='JavaScript'>alert('변경사유을 입력해주세요.');</script>");
			else
			{
				
				string m_RequirePage = "ItemOrganizationInfo";
				string m_RequireAction = "Update";
				string m_TableName = "IOI_MT";
				int m_Idx = (int)PageName.ItemOrganizationInfo;
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

		/// <summary>
		/// 삭제버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Delete_Click(object sender, System.EventArgs e)
		{
			
			string m_RequirePage = "ItemOrganizationInfo";
			string m_RequireAction = "Delete";
			string m_TableName = "IOI_MT";
			int m_Idx = (int)PageName.ItemOrganizationInfo;
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

			Session["Reason"] = "삭제";

			MasterInfoRecordRUD rud = new MasterInfoRecordRUD(m_RequirePage, m_RequireAction, m_TableName, m_Idx, m_User, InputList,"기준정보");
			rud.ActivateRUD();
			
			this.databind();

			clear();
			
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
			tbUpdateReason.Text = "";

			wdcBeginDate.Value = "";
			wdcEndDate.Value = "";
		}

		/// <summary>
		/// 웹그리드에 바인딩해주는 함수
		/// </summary>
		private void databind()
		{
			string m_itemnum = ItemSearchControl1.ItemNum.ToString();
			string m_table = "IOI_MT";
			string m_page = "ItemOrganizationInfo";

			View view = new View(m_itemnum, m_table, m_page);
			UltraWebGrid1.DataSource = view.ItemView();
			UltraWebGrid1.DataBind();
			

			

		}

		/// <summary>
		/// 참조보기 버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Reference_Click(object sender, System.EventArgs e)
		{
			if(lb_Index.Value == "0")
			{
				RegisterStartupScript("","<script language='JavaScript'>alert('항목을 선택후 버튼을 눌러주세요.');</script>");
			}
			else
			{
				string m_TableName = "품목구성정보";
				int m_RecodeIdx = int.Parse(lb_Index.Value);
			

				Reference rf = new Reference(m_TableName, m_RecodeIdx);
				Ultrawebgrid2.DataSource = rf.reference();
				Ultrawebgrid2.DataBind();
			}
		}

		/// <summary>
		/// BOM전개 버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Expansion_Click(object sender, System.EventArgs e)
		{
			string ItemNum = ItemSearchControl1.ItemNum.ToString();

			if(ItemNum == "")
			{
				RegisterStartupScript("","<script language='JavaScript'>alert('항목을 선택후 버튼을 눌러주세요.');</script>");
			}
			else
			{
				RegisterStartupScript("","<script language='JavaScript'>window.open('./Popup/Bom.aspx?ItemNum=" + ItemNum + "','BOM_Tree','width=810,height=500px,resizable=yes');</script>");
			}
		}


		private void UltraWebGrid1_SelectedRowsChange(object sender, Infragistics.WebUI.UltraWebGrid.SelectedRowsEventArgs e)
		{
			
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

		private void LinkButton3_Click(object sender, System.EventArgs e)
		{
			hdItemNum.Value = ItemSearch1.ChildItemNum;
		
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			//string str = "Select ItemNum, ItemDrawNum,ItemName, PropertyClassification, Unit, Standard, ChargePerson From II_MT Where ItemNum = @num and (PropertyClassification = '제품' or PropertyClassification = '팬텀' or PropertyClassification = '상품' or PropertyClassification = '반제품' or PropertyClassification = '원자재) and RecodingState = 1";
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
			RegisterStartupScript("","<script language='JavaScript'>window.open('./Popup/ReversalBOM.aspx','ReversalBOM','width=810,height=500px,resizable=yes');</script>");
		}

		private void btHistory_Click(object sender, System.EventArgs e)
		{
			RegisterStartupScript("","<script language='JavaScript'>window.open('UpdateItemOrganizationInfoHistory.aspx','UpdateItemOrganizationInfoHistory','width=830,height=600px,resizable=yes');</script>");
		}

		private void Button2_Click(object sender, System.EventArgs e)
		{
			RegisterStartupScript("","<script language='JavaScript'>window.open('CopyBOM.aspx','CopyBOM','width=810,height=100px,resizable=yes, status=yes');</script>");
		}

		private void tbTotalUpdate_Click(object sender, System.EventArgs e)
		{
			SqlConnection con1 = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			con1.Open();
			string str = "Select Name From UI_MT Where RecodingState = 1 and ID = @id";
			SqlCommand comm = new SqlCommand(str,con1);
			comm.Parameters.Add("@id",SqlDbType.VarChar).Value = Session["ID"].ToString();
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				m_UserName = dr["Name"].ToString();
			}
			con1.Close();

			if(File1.Value != "")
			{
				string FileName = System.IO.Path.GetDirectoryName(File1.PostedFile.FileName) + System.IO.Path.GetFileName(File1.PostedFile.FileName);
			
				if(FileName.ToString().Substring(FileName.ToString().Length - 3) == "xls" || FileName.ToString().Substring(FileName.ToString().Length - 3) == "XLS")
				{

					string upLoadFile = Path.GetTempFileName();
					File1.PostedFile.SaveAs(upLoadFile);

					OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + upLoadFile + "';Extended Properties=Excel 8.0;");
			
					con.Open();
			
					OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", con);
						
					adapter.Fill(dataset);
					con.Close();

					SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
					conn.Open();
					SqlTransaction tr = conn.BeginTransaction();

					try
					{

						UpdateItemOrganizationInfo(conn,tr);
						
						Response.Write("<script language=javascript>");
						Response.Write("alert('수정하였습니다.');");
						Response.Write("</script>");
						tr.Commit();
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
						File.Delete(upLoadFile);
						conn.Close();
					}
				}
				else
				{
					Response.Write("<script language=javascript>");
					Response.Write("alert('엑셀 양식의 파일이 아닙니다.');");
					Response.Write("</script>");
				}
			}
			else
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('입력할 엑셀자료가 없습니다.');");
				Response.Write("</script>");
			}
		}


		private void UpdateItemOrganizationInfo(SqlConnection con, SqlTransaction trans)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;
			string str = "";

			foreach(DataRow row in dataset.Tables[0].Rows)
			{
				
				str = @"Update IOI_MT Set 
						NeedQuantityNumerator = @NeedQuantityNumerator, 
						NeedQuantityDenominator = @NeedQuantityDenominator,
						BeginDate = @BeginDate, 
						EndDate = @EndDate, 
						UpdatingPerson = @UpdatingPerson, UpdatingPersonID = @UpdatingPersonID, UpdatingDate = @UpdatingDate
						Where ParentItemNum = @ParentItemNum and ChildItemNum = @ChildItemNum andRecodingState = 1";

				
				if(row["소요량분자"].ToString().Trim() == "")
					comm.Parameters.Add("@NeedQuantityNumerator","1");
				else
					comm.Parameters.Add("@NeedQuantityNumerator",decimal.Parse(row["소요량분자"].ToString().Trim()));

				if(row["소요량분모"].ToString().Trim() == "")
					comm.Parameters.Add("@NeedQuantityDenominator","1");
				else
					comm.Parameters.Add("@NeedQuantityDenominator",decimal.Parse(row["소요량분모"].ToString().Trim()));

				if(row["시작일"].ToString().Trim() == "")
					comm.Parameters.Add("@BeginDate",DateTime.Now.ToShortDateString());
				else
					comm.Parameters.Add("@BeginDate",DateTime.Parse(row["시작일"].ToString().Trim()).ToShortDateString());

				comm.Parameters.Add("@EndDate","2076-06-06");
				

				comm.Parameters.Add("@UpdatingPerson",m_UserName);
				comm.Parameters.Add("@UpdatingPersonID",Session["ID"].ToString().Trim());
				comm.Parameters.Add("@UpdatingDate",DateTime.Now.ToShortDateString());
				comm.Parameters.Add("@ParentItemNum",row["모품목번호"].ToString().Trim());
				comm.Parameters.Add("@ChildItemNum",row["자품목번호"].ToString().Trim());
				
				comm.CommandText = str;
		
				int count = comm.ExecuteNonQuery();
				if(count > 0)
				{
					//변경사유 기록
					UpdateHistory(con,trans,row);

				}
				comm.Parameters.Clear();

			}
		}	


		/// <summary>
		/// 품목구성 이력원장 등록
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		private void UpdateHistory(SqlConnection conn,SqlTransaction tr, DataRow drow)
		{
			string str = @"Insert Into UPIOI_HT (ParentItemNum, ChildItemNum, NeedQuantityNumerator, NeedQuantityDenominator, 
				ProcessManagement, SubDivision, SupplyDivision, BOMUnit, BeginDate, EndDate, UpdateReason, 
				UpdatingPerson, UpdatingPersonID, UpdatingDate, ItemOrganizationInfoIndex)
				values (@ParentItemNum, @ChildItemNum, @NeedQuantityNumerator, @NeedQuantityDenominator, 
				@ProcessManagement, @SubDivision, @SupplyDivision, @BOMUnit, @BeginDate, @EndDate, @UpdateReason, 
				@UpdatingPerson, @UpdatingPersonID, @UpdatingDate, @ItemOrganizationInfoIndex)";

			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@ParentItemNum", drow["모품목번호"].ToString().Trim());
			comm.Parameters.Add("@ChildItemNum", drow["자품목번호"].ToString().Trim());
			if(drow["소요량분자"].ToString().Trim() == "")
				comm.Parameters.Add("@NeedQuantityNumerator","1");
			else
				comm.Parameters.Add("@NeedQuantityNumerator",decimal.Parse(drow["소요량분자"].ToString().Trim()));

			if(drow["소요량분모"].ToString().Trim() == "")
				comm.Parameters.Add("@NeedQuantityDenominator","1");
			else
				comm.Parameters.Add("@NeedQuantityDenominator",decimal.Parse(drow["소요량분모"].ToString().Trim()));

			
			

			if(drow["공정관리"].ToString().Trim() == "")
				comm.Parameters.Add("@ProcessManagement", "1");
			else
				comm.Parameters.Add("@ProcessManagement", drow["공정관리"].ToString().Trim());
			if(drow["하위구분"].ToString().Trim() == "")
				comm.Parameters.Add("@SubDivision", "0");
			else
				comm.Parameters.Add("@SubDivision", drow["조달구분"].ToString().Trim());
			comm.Parameters.Add("@SupplyDivision", drow["조달구분"].ToString().Trim());
			comm.Parameters.Add("@BOMUnit", drow["공정관리"].ToString().Trim());
			if(drow["시작일"].ToString().Trim() == "")
				comm.Parameters.Add("@BeginDate",DateTime.Now.ToShortDateString());
			else
				comm.Parameters.Add("@BeginDate",DateTime.Parse(drow["시작일"].ToString().Trim()).ToShortDateString());

			
			comm.Parameters.Add("@EndDate","2076-06-06");
			comm.Parameters.Add("@UpdateReason","일괄수정");
			comm.Parameters.Add("@UpdatingPerson", Session["UserName"].ToString());
			comm.Parameters.Add("@UpdatingPersonID", Session["UserID"].ToString());
			comm.Parameters.Add("@UpdatingDate", DateTime.Now.ToShortDateString());
			
			comm.Parameters.Add("@ItemOrganizationInfoIndex", FindIOI_MT(conn,tr,drow["모품목번호"].ToString().Trim(),drow["자품목번호"].ToString().Trim()));

			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
		}


		/// <summary>
		/// 품목구성정보테이블원장번호 찾기
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <returns></returns>
		private int FindIOI_MT(SqlConnection con,SqlTransaction trans, string ParentItem,string ChildItem)
		{
			string str = "Select isnull(ItemOrganizationInfoIndex, 0) From IOI_MT where RecodingState = 1 and ParentItemNum=@ParentItemNum and ChildItemNum = @ChildItemNum";
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			comm.Parameters.Add("@ParentItemNum", ParentItem);
			comm.Parameters.Add("@ChildItemNum", ChildItem);

			int result = int.Parse(comm.ExecuteScalar().ToString());
			comm.Parameters.Clear();

			if(result == 0)
				throw new Exception("모품목 " + ParentItem + "과 자품목 " + ChildItem +"인 레코드가 존재하지 않습니다!");

			

			return result;
		}
	}
}
