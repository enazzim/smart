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
	/// WCNameInfor에 대한 요약 설명입니다.
	/// </summary>
	public class WCInfo : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label30;
		protected System.Web.UI.WebControls.Button bt_Clear;
		protected System.Web.UI.WebControls.Button bt_Update;
		protected System.Web.UI.WebControls.Button bt_Delete;
		protected System.Web.UI.WebControls.Button bt_Registration;
		protected System.Web.UI.WebControls.Label lb_Index;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label lb_WCName;
		protected System.Web.UI.WebControls.TextBox tb_WCName;
		protected System.Web.UI.WebControls.DropDownList dl_MainProcessCode;
		protected System.Web.UI.WebControls.DropDownList dl_State;
		protected System.Web.UI.WebControls.DropDownList dl_CapacityDistinction;
		protected System.Web.UI.WebControls.Button bt_Referance;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_ReferenceIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_ReferenceTable;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		protected Infragistics.WebUI.WebCombo.WebCombo wc_WCChoice;
		protected System.Web.UI.WebControls.TextBox tb_RetentionStaff;
		protected System.Web.UI.WebControls.TextBox tb_OperationTime;
		protected System.Web.UI.WebControls.TextBox tb_ElectricCapacity;
		protected System.Web.UI.WebControls.TextBox tb_UnitTimeUseCost;
		protected System.Web.UI.WebControls.TextBox tb_Sorting;
		protected System.Web.UI.WebControls.Label lb_WCInfo;
	
		private void Page_Load(object sender, System.EventArgs e)
		{

			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}
			
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(!Page.IsPostBack)
			{
				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 기준정보 > 작업장정보';</script>");

				bt_Registration.Attributes.Add("onClick", "return OK('입력한 항목을 등록');");
				bt_Update.Attributes.Add("onClick", "return OK('선택한 항목을 수정');");
				bt_Delete.Attributes.Add("onClick", "return OK('선택한 항목을 삭제');");

				tb_RetentionStaff.Attributes.Add("OnKeyDown", "OnKeyDown_Currency(this);");
				tb_RetentionStaff.Attributes.Add("OnFocus", "OnFocus_Obj(this);");

				tb_OperationTime.Attributes.Add("OnKeyDown", "OnKeyDown_Currency(this);");
				tb_OperationTime.Attributes.Add("OnFocus", "OnFocus_Obj(this);");

				tb_ElectricCapacity.Attributes.Add("OnKeyDown", "OnKeyDown_Currency(this);");
				tb_ElectricCapacity.Attributes.Add("OnFocus", "OnFocus_Obj(this);");

				tb_UnitTimeUseCost.Attributes.Add("OnKeyDown", "OnKeyDown_Currency(this);");
				tb_UnitTimeUseCost.Attributes.Add("OnFocus", "OnFocus_Obj(this);");

				page_load();


				
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
			this.wc_WCChoice.SelectedRowChanged += new Infragistics.WebUI.WebCombo.SelectedRowChangedEventHandler(this.wc_WCChoice_SelectedRowChanged);
			this.LinkButton1.Click += new System.EventHandler(this.LinkButton1_Click);
			this.bt_Referance.Click += new System.EventHandler(this.bt_Referance_Click);
			this.bt_Clear.Click += new System.EventHandler(this.bt_Clear_Click);
			this.bt_Update.Click += new System.EventHandler(this.bt_Update_Click);
			this.bt_Delete.Click += new System.EventHandler(this.bt_Delete_Click);
			this.bt_Registration.Click += new System.EventHandler(this.bt_Registration_Click);
			this.UltraWebGrid1.DblClick += new Infragistics.WebUI.UltraWebGrid.ClickEventHandler(this.UltraWebGrid1_DblClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void page_load()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string str = "Select WCName, WCInfoIndex From WCI_MT Where RecodingState = 1 order by WCName";
			SqlCommand comm = new SqlCommand(str,conn);
			SqlDataAdapter da = new SqlDataAdapter(comm) ;
			DataSet ds = new DataSet() ;
			da.Fill(ds);
				
			wc_WCChoice.DataSource = ds;
			wc_WCChoice.DataTextField = ds.Tables[0].Columns[0].ToString();
			wc_WCChoice.DataValueField = ds.Tables[0].Columns[1].ToString();
			wc_WCChoice.DataBind();

				
			//	*************************************************
			//	**  대표공정 드롭다운리스트... 데이타바인딩... ** 
			//	*************************************************
			//코드분류표에서 대분류명이 단위인 소분류명을 가지고 옴.
			string str_Process = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '1400' and (SmallClassificationCode != '14000000' and SmallClassificationCode != '14009999')" ;
			SqlCommand comm_Process = new SqlCommand(str_Process,conn);
			SqlDataAdapter da_Process = new SqlDataAdapter(comm_Process) ;
			DataSet ds_Process = new DataSet() ;
			da_Process.Fill(ds_Process);
				
			dl_MainProcessCode.DataSource = ds_Process;
			dl_MainProcessCode.DataTextField = ds_Process.Tables[0].Columns[0].ToString();
			dl_MainProcessCode.DataValueField = ds_Process.Tables[0].Columns[1].ToString();
			dl_MainProcessCode.DataBind();
			dl_MainProcessCode.Items.Insert(0, "-선택-") ;
			dl_MainProcessCode.Items[0].Value = "";
				
			conn.Close();

		}










        
		//등록버튼
		private void bt_Registration_Click(object sender, System.EventArgs e)
		{
			
			
			if(tb_WCName.Text == "0" || dl_MainProcessCode.SelectedIndex ==0 || tb_RetentionStaff.Text == "0" || tb_RetentionStaff.Text == ""|| tb_OperationTime.Text == "")
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('필수항목을 입력해주세요');");
				Response.Write("</script>");
			}
			else
			{


				if(Page.IsValid == true)
				{
					string m_RequirePage = "WCInfo";
					string m_RequireAction = "Registration";
					string m_TableName = "WCI_MT";
					int m_Idx = (int)PageName.WCInfo;
					string m_User = Session["ID"].ToString();
					string m_RecodeIdx = lb_WCInfo.Text;
			
			
					ArrayList InputList = new ArrayList();

					InputList.Add(tb_WCName.Text);
					InputList.Add(dl_MainProcessCode.SelectedItem.Value);
					InputList.Add(Decimal.Parse(tb_RetentionStaff.Text));
					InputList.Add(dl_State.SelectedItem.Value);
					InputList.Add(dl_CapacityDistinction.SelectedItem.Value);
					InputList.Add(Decimal.Parse(tb_OperationTime.Text));
					InputList.Add(Decimal.Parse(tb_ElectricCapacity.Text));
					InputList.Add(Decimal.Parse(tb_UnitTimeUseCost.Text));
					InputList.Add(Decimal.Parse(tb_Sorting.Text));

					MasterInfoRecordRUD rud = new MasterInfoRecordRUD(m_RequirePage, m_RequireAction, m_TableName, m_Idx, m_User, InputList,"낱개입력");
					rud.ActivateRUD();
					databind();

					bt_Delete.Enabled = false;
					bt_Update.Enabled = false;
					bt_Registration.Enabled = true;
				}
			}
		}

		private void wc_WCChoice_SelectedRowChanged(object sender, Infragistics.WebUI.WebCombo.SelectedRowChangedEventArgs e)
		{
			if(Convert.ToString(wc_WCChoice.DataValue) != "")
			{
				lb_WCInfo.Text = Convert.ToString(wc_WCChoice.DataValue);
			
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				conn.Open();
				string str = "Select * From WCI_MT Where RecodingState = 1 and WCInfoIndex = @index ";
				SqlCommand comm = new SqlCommand(str,conn);
				comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(lb_WCInfo.Text);

				SqlDataAdapter da = new SqlDataAdapter(comm);
				DataSet ds = new DataSet();
				da.Fill(ds);

				if(ds.Tables[0].Rows.Count !=0)
				{
					tb_WCName.Text = ds.Tables[0].Rows[0]["WCName"].ToString();
			
					for (int i=0 ; i < dl_MainProcessCode.Items.Count ; i++)
					{
						if( dl_MainProcessCode.Items[i].Value == ds.Tables[0].Rows[0]["MainProcessCode"].ToString())
						{
							dl_MainProcessCode.SelectedIndex = i;				
						}
					}
			
					tb_RetentionStaff.Text = ds.Tables[0].Rows[0]["RetentionStaff"].ToString();//상태
					for (int i=0 ; i < dl_State.Items.Count ; i++)
					{
						if( ds.Tables[0].Rows[i]["State"].ToString() == "False")
						{
							dl_State.SelectedIndex = 1;//가동중지중
							break;
						}
						else
						{
							dl_State.SelectedIndex = 0;//가동중
							break;
						}
					}
					for (int i=0 ; i < dl_CapacityDistinction.Items.Count ; i++)	//Capa도면
					{
						if( ds.Tables[0].Rows[i]["CapacityDistinction"].ToString() == "False")
						{
							dl_CapacityDistinction.SelectedIndex = 0;//작업시간
							break;
						}
						else
						{
							dl_CapacityDistinction.SelectedIndex = 1;//작업시간 * 인원
							break;
						}

					}
					tb_OperationTime.Text = ds.Tables[0].Rows[0]["OperationTime"].ToString();
					tb_ElectricCapacity.Text = ds.Tables[0].Rows[0]["ElectricCapacity"].ToString();
					tb_UnitTimeUseCost.Text = ds.Tables[0].Rows[0]["UnitTimeUseCost"].ToString();
					lb_Index.Text = ds.Tables[0].Rows[0]["WCInfoIndex"].ToString();
					tb_Sorting.Text =  ds.Tables[0].Rows[0]["Sorting"].ToString();
				}

				bt_Delete.Enabled = true;
				bt_Update.Enabled = true;
				bt_Registration.Enabled = false;

				conn.Close();
			}

		}

		private void bt_Update_Click(object sender, System.EventArgs e)
		{
			if(lb_WCInfo.Text == "0")
			{
				RegisterStartupScript("","<script>alert('등록후 바로 수정은 불가능 합니다!');</script>");
			}
			else
			{
				string m_RequirePage = "WCInfo";
				string m_RequireAction = "Update";
				string m_TableName = "WCI_MT";
				int m_Idx = (int)PageName.WCInfo;
				string m_User = Session["ID"].ToString();
				string m_RecodeIdx = lb_WCInfo.Text;
			
			
				ArrayList InputList = new ArrayList();

				InputList.Add(tb_WCName.Text);
				InputList.Add(dl_MainProcessCode.SelectedItem.Value);
				InputList.Add(tb_RetentionStaff.Text);
				InputList.Add(dl_State.SelectedItem.Value);
				InputList.Add(dl_CapacityDistinction.SelectedItem.Value);
				InputList.Add(tb_OperationTime.Text);
				InputList.Add(tb_ElectricCapacity.Text);
				InputList.Add(tb_UnitTimeUseCost.Text);
				InputList.Add(Decimal.Parse(tb_Sorting.Text));
				InputList.Add(lb_WCInfo.Text);

		

				MasterInfoRecordRUD rud = new MasterInfoRecordRUD(m_RequirePage, m_RequireAction, m_TableName, m_Idx, m_User, InputList,"낱개입력");
				rud.ActivateRUD();

				bt_Delete.Enabled = false;
				bt_Update.Enabled = false;
				bt_Registration.Enabled = true;
			}
							
		}

		private void bt_Delete_Click(object sender, System.EventArgs e)
		{
			if(lb_WCInfo.Text == "0")
			{
				RegisterStartupScript("","<script>alert('등록후 바로 삭제는 불가능 합니다!');</script>");
			}
			else
			{
				string m_RequirePage = "WCInfo";
				string m_RequireAction = "Delete";
				string m_TableName = "WCI_MT";
				int m_Idx = (int)PageName.WCInfo;
				string m_User = Session["ID"].ToString();
				string m_RecodeIdx = lb_WCInfo.Text;
			
			
				ArrayList InputList = new ArrayList();

				InputList.Add(tb_WCName.Text);
				InputList.Add(dl_MainProcessCode.SelectedItem.Value);
				InputList.Add(tb_RetentionStaff.Text);
				InputList.Add(dl_State.SelectedItem.Value);
				InputList.Add(dl_CapacityDistinction.SelectedItem.Value);
				InputList.Add(tb_OperationTime.Text);
				InputList.Add(tb_ElectricCapacity.Text);
				InputList.Add(tb_UnitTimeUseCost.Text);
				InputList.Add(Decimal.Parse(tb_Sorting.Text));
				InputList.Add(lb_WCInfo.Text);

				MasterInfoRecordRUD rud = new MasterInfoRecordRUD(m_RequirePage, m_RequireAction, m_TableName, m_Idx, m_User, InputList,"낱개입력");
				rud.ActivateRUD();

				clear();
				databind();

				bt_Delete.Enabled = false;
				bt_Update.Enabled = false;
				bt_Registration.Enabled = true;
			}
			
		}

		private void bt_Clear_Click(object sender, System.EventArgs e)
		{
			clear();
			databind();

			bt_Delete.Enabled = false;
			bt_Update.Enabled = false;
			bt_Registration.Enabled = true;
		}

		private void clear()
		{
			tb_WCName.Text = "";
			dl_MainProcessCode.SelectedIndex = 0;
			tb_RetentionStaff.Text = "0";
			dl_State.SelectedIndex = 0;
			dl_CapacityDistinction.SelectedIndex = 0;
			tb_OperationTime.Text = "0";
			tb_ElectricCapacity.Text = "0";
			tb_UnitTimeUseCost.Text = "0";
			tb_Sorting.Text = "0";
			lb_WCInfo.Text = "";
			lb_Index.Text = "0";
			wc_WCChoice.DisplayValue = "";
			wc_WCChoice.DataValue = "";
		}
		private void databind()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string str = "Select WCName, WCInfoIndex From WCI_MT Where RecodingState = 1";
			SqlCommand comm = new SqlCommand(str,conn);
			SqlDataAdapter da = new SqlDataAdapter(comm) ;
			DataSet ds = new DataSet() ;
			da.Fill(ds);
				
			wc_WCChoice.DataSource = ds;
			wc_WCChoice.DataTextField = ds.Tables[0].Columns[0].ToString();
			wc_WCChoice.DataValueField = ds.Tables[0].Columns[1].ToString();
			wc_WCChoice.DataBind();
		}

		/// <summary>
		/// 참고보기
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Referance_Click(object sender, System.EventArgs e)
		{
			if(lb_WCInfo.Text == "")
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('항목을 선택후 버튼을 눌러주세요');");
				Response.Write("</script>");
			}
			else
			{

				string m_TableName = "작업장정보";
				int m_RecodeIdx = int.Parse(lb_WCInfo.Text);

				Reference rf = new Reference(m_TableName, m_RecodeIdx);
				UltraWebGrid1.DataSource = rf.reference();
				UltraWebGrid1.DataBind();
			}		
		}

		

		private void LinkButton1_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script language=javascript>");
			Response.Write("window.open('./Popup/WCName.aspx','WCName','width=830,height=500px');");
			Response.Write("</script>");
		}

		private void UltraWebGrid1_DblClick(object sender, Infragistics.WebUI.UltraWebGrid.ClickEventArgs e)
		{
			string table = lb_ReferenceTable.Value;
			string index = lb_ReferenceIndex.Value;

			if(table == "생산품질")
				table = "T_Product";
			else if(table =="개발실")
				table = "T_Development";
			else
				table = "T_Materials";
			
			RegisterStartupScript("","<script language=javascript>window.open('./Popup/View.aspx?table=" + table + "&index=" + index + "','Reference'," + "'width=510" + "," + "height=435px');</script>");
		}
	}
}
