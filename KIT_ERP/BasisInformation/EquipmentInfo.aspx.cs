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
	/// EquipmentInfo에 대한 요약 설명입니다.
	/// </summary>
	public class EquipmentInfo : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lb_EquipmentNum;
		protected System.Web.UI.WebControls.Label lb_EquipmentName;
		protected System.Web.UI.WebControls.Label lb_EquipmentClassification;
		protected System.Web.UI.WebControls.Label lb_Capacity;
		protected System.Web.UI.WebControls.Label lb_ElectricCapacity;
		protected System.Web.UI.WebControls.Label lb_UnitTimeUseCost;
		protected System.Web.UI.WebControls.Label lb_Standard;
		protected System.Web.UI.WebControls.Label lb_Unit;
		protected System.Web.UI.WebControls.Label lb_WCName;
		protected System.Web.UI.WebControls.Label lb_InjectionStaffNum;
		protected System.Web.UI.WebControls.Label lb_Location;
		protected System.Web.UI.WebControls.Label lb_OccupancyArea;
		protected System.Web.UI.WebControls.Label lb_BuyingDate;
		protected System.Web.UI.WebControls.Label lb_BuyingCost;
		protected System.Web.UI.WebControls.Label lb_InstrumentNum;
		protected System.Web.UI.WebControls.Label lb_EquipmentState;
		protected System.Web.UI.WebControls.Label lb_CheckDate;
		protected System.Web.UI.WebControls.Label lb_ValidPeriod;
		protected System.Web.UI.WebControls.Label lb_CheckAgency;
		protected System.Web.UI.WebControls.Label lb_ReadyTime;
		protected System.Web.UI.WebControls.Label lb_ValidityYear;
		protected System.Web.UI.WebControls.Label lb_DesignShot;
		protected System.Web.UI.WebControls.Label lb_TotalShot;
		protected System.Web.UI.WebControls.Label lb_WorkShot;
		protected System.Web.UI.WebControls.Label lb_FirstShot;
		protected System.Web.UI.WebControls.Label lb_ManagePeriod1;
		protected System.Web.UI.WebControls.Label lb_ManagePeriod3;
		protected System.Web.UI.WebControls.Label lb_ManagePeriod2;
		protected System.Web.UI.WebControls.Label lb_ManagePeriod4;
		protected System.Web.UI.WebControls.Label lb_ManageDate1;
		protected System.Web.UI.WebControls.Label lb_ManageDate3;
		protected System.Web.UI.WebControls.Label lb_ManageDate2;
		protected System.Web.UI.WebControls.Label lb_ManageDate4;
		protected System.Web.UI.WebControls.Label lb_Cavity;
		protected System.Web.UI.WebControls.Label lb_EquipmentChoice;
		protected Infragistics.WebUI.WebCombo.WebCombo wc_EquipmentChoice;
		protected System.Web.UI.WebControls.Label lb_EquipmentIndex;
		protected System.Web.UI.WebControls.TextBox tb_EquipmentNum;
		protected System.Web.UI.WebControls.TextBox tb_EquipmentName;
		protected System.Web.UI.WebControls.DropDownList dl_EquipmentClassification;
		protected System.Web.UI.WebControls.TextBox tb_Standard;
		protected System.Web.UI.WebControls.TextBox tb_Capacity;
		protected System.Web.UI.WebControls.TextBox tb_ElectricCapacity;
		protected System.Web.UI.WebControls.TextBox tb_UnitTimeUseCost;
		protected System.Web.UI.WebControls.DropDownList dl_Unit;
		protected System.Web.UI.WebControls.DropDownList dl_WCName;
		protected System.Web.UI.WebControls.TextBox tb_InjectionStaffNum;
		protected System.Web.UI.WebControls.DropDownList dl_Location;
		protected System.Web.UI.WebControls.TextBox tb_OccupancyArea;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdc_BuyingDate;
		protected System.Web.UI.WebControls.TextBox tb_BuyingCost;
		protected System.Web.UI.WebControls.TextBox tb_InstrumentNum;
		protected System.Web.UI.WebControls.DropDownList dl_EquipmentState;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdc_CheckDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdc_ValidPeriod;
		protected System.Web.UI.WebControls.TextBox tb_CheckAgency;
		protected System.Web.UI.WebControls.TextBox tb_ReadyTime;
		protected System.Web.UI.WebControls.TextBox tb_Cavity;
		protected System.Web.UI.WebControls.TextBox tb_ValidityYear;
		protected System.Web.UI.WebControls.TextBox tb_DesignShot;
		protected System.Web.UI.WebControls.TextBox tb_FirstShot;
		protected System.Web.UI.WebControls.TextBox tb_TotalShot;
		protected System.Web.UI.WebControls.TextBox tb_WorkShot;
		protected System.Web.UI.WebControls.TextBox tb_ManagePeriod1;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdc_ManageDate1;
		protected System.Web.UI.WebControls.TextBox tb_ManagePeriod2;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdc_ManageDate2;
		protected System.Web.UI.WebControls.TextBox tb_ManagePeriod3;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdc_ManageDate3;
		protected System.Web.UI.WebControls.TextBox tb_ManagePeriod4;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdc_ManageDate4;
		protected System.Web.UI.WebControls.Button bt_RelationView;
		protected System.Web.UI.WebControls.Label lb_Index;
		protected System.Web.UI.WebControls.Button bt_Clear;
		protected System.Web.UI.WebControls.Button bt_Update;
		protected System.Web.UI.WebControls.Button bt_Delete;
		protected System.Web.UI.WebControls.Button bt_Registration;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_ReferenceIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_ReferenceTable;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		 
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}
			
			

			if( !IsPostBack )
			{
				////Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 기준정보 > 설비정보';</script>");
			
				// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
				bt_Registration.Attributes.Add("onClick", "return OK('입력한 항목을 등록');");
				bt_Update.Attributes.Add("onClick", "return OK('선택한 항목을 수정');");
				bt_Delete.Attributes.Add("onClick", "return OK('선택한 항목을 삭제');");

				tb_Capacity.Attributes.Add("OnKeyDown", "OnKeyDown_Currency(this);");
				tb_Capacity.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				
				tb_ElectricCapacity.Attributes.Add("OnKeyDown", "OnKeyDown_Currency(this);");
				tb_ElectricCapacity.Attributes.Add("OnFocus", "OnFocus_Obj(this);");

				tb_UnitTimeUseCost.Attributes.Add("OnKeyDown", "OnKeyDown_Currency(this);");
				tb_UnitTimeUseCost.Attributes.Add("OnFocus", "OnFocus_Obj(this);");

				tb_OccupancyArea.Attributes.Add("OnKeyDown", "OnKeyDown_Currency(this);");
				tb_OccupancyArea.Attributes.Add("OnFocus", "OnFocus_Obj(this);");

				tb_BuyingCost.Attributes.Add("OnKeyDown", "OnKeyDown_Currency(this);");
				tb_BuyingCost.Attributes.Add("OnFocus", "OnFocus_Obj(this);");

				tb_ReadyTime.Attributes.Add("OnKeyDown", "OnKeyDown_Currency(this);");
				tb_ReadyTime.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				
				tb_Cavity.Attributes.Add("OnKeyDown", "OnKeyDown_Currency(this);");
				tb_Cavity.Attributes.Add("OnFocus", "OnFocus_Obj(this);");

				//숫자만 입력
				tb_ValidityYear.Attributes.Add("OnKeyDown", "OnKeyDown_Currency(this);");	
				// focus를 얻으면 자동으로 select()
				tb_ValidityYear.Attributes.Add("OnFocus", "OnFocus_Obj(this);");				
				
				tb_DesignShot.Attributes.Add("OnKeyDown", "OnKeyDown_Currency(this);");
				tb_DesignShot.Attributes.Add("OnFocus", "OnFocus_Obj(this);");

				tb_FirstShot.Attributes.Add("OnKeyDown", "OnKeyDown_Currency(this);");
				tb_FirstShot.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				
				tb_TotalShot.Attributes.Add("OnKeyDown", "OnKeyDown_Currency(this);");
				tb_TotalShot.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				
				tb_WorkShot.Attributes.Add("OnKeyDown", "OnKeyDown_Currency(this);");
				tb_WorkShot.Attributes.Add("OnFocus", "OnFocus_Obj(this);");

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
			this.wc_EquipmentChoice.SelectedRowChanged += new Infragistics.WebUI.WebCombo.SelectedRowChangedEventHandler(this.wc_EquipmentChoice_SelectedRowChanged);
			this.LinkButton1.Click += new System.EventHandler(this.LinkButton1_Click);
			this.bt_RelationView.Click += new System.EventHandler(this.bt_RelationView_Click);
			this.bt_Clear.Click += new System.EventHandler(this.bt_Clear_Click);
			this.bt_Update.Click += new System.EventHandler(this.bt_Update_Click);
			this.bt_Delete.Click += new System.EventHandler(this.bt_Delete_Click);
			this.bt_Registration.Click += new System.EventHandler(this.bt_Registration_Click);
			this.UltraWebGrid1.DblClick += new Infragistics.WebUI.UltraWebGrid.ClickEventHandler(this.UltraWebGrid1_DblClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void page_Load()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "SELECT EquipmentNum, EquipmentName, InstrumentNum, EquipmentInfoIndex FROM EI_MT Where RecodingState=1 order by EquipmentNum";
			SqlCommand comm =  new SqlCommand(str,conn);
			SqlDataAdapter da = new SqlDataAdapter(comm) ;
			DataSet ds = new DataSet() ;
			da.Fill(ds);

			wc_EquipmentChoice.DataSource = ds;
			wc_EquipmentChoice.DataTextField = ds.Tables[0].Columns[1].ToString();
			wc_EquipmentChoice.DataValueField = ds.Tables[0].Columns[3].ToString();
			wc_EquipmentChoice.DataBind();


            
			//	*************************************************
			//	**  WC명 드롭다운리스트... 데이타바인딩...     **  
			//	*************************************************
			// WC정보테이블에서 가져옴
			string str_WCName = "SELECT   WCName, WCInfoIndex FROM WCI_MT where RecodingState = 1";
			SqlCommand comm_WCName =  new SqlCommand(str_WCName,conn);
			SqlDataAdapter da_WCName = new SqlDataAdapter(comm_WCName) ;
			DataSet ds_WCName = new DataSet() ;
			da_WCName.Fill(ds_WCName);

			dl_WCName.DataSource = ds_WCName;
			dl_WCName.DataTextField = ds_WCName.Tables[0].Columns[0].ToString();
			dl_WCName.DataValueField = ds_WCName.Tables[0].Columns[0].ToString();
			dl_WCName.DataBind();
			dl_WCName.Items.Insert(0, "-선택-") ;
			dl_WCName.Items[0].Value = "";




		
			//	*************************************************
			//	**  설비분류 드롭다운리스트... 데이타바인딩... **   dl_EquipmentClassification
			//	*************************************************
			//코드분류표에서 대분류명이 설비분류인 소분류명을 가지고 옴.
			string str_Equipment = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '0720'" ;
			SqlCommand comm_Equipment = new SqlCommand(str_Equipment,conn);
			SqlDataAdapter da_Equipment = new SqlDataAdapter(comm_Equipment) ;
			DataSet ds_Equipment = new DataSet() ;
			da_Equipment.Fill(ds_Equipment);
				
			dl_EquipmentClassification.DataSource = ds_Equipment;
			dl_EquipmentClassification.DataTextField = ds_Equipment.Tables[0].Columns[0].ToString();
			dl_EquipmentClassification.DataValueField = ds_Equipment.Tables[0].Columns[1].ToString();
			dl_EquipmentClassification.DataBind();
			dl_EquipmentClassification.Items.Insert(0, "-선택-") ;
			dl_EquipmentClassification.Items[0].Value = "07200000";


			//	*************************************************
			//	**  단위 드롭다운리스트... 데이타바인딩... **
			//	*************************************************
			//코드분류표에서 대분류명이 단위인 소분류명을 가지고 옴.
			string str_Unit = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '0400' " ;
			SqlCommand comm_Unit = new SqlCommand(str_Unit,conn);
			SqlDataAdapter da_Unit = new SqlDataAdapter(comm_Unit) ;
			DataSet ds_Unit = new DataSet() ;
			da_Unit.Fill(ds_Unit);
				
			dl_Unit.DataSource = ds_Unit;
			dl_Unit.DataTextField = ds_Unit.Tables[0].Columns[0].ToString();
			dl_Unit.DataValueField = ds_Unit.Tables[0].Columns[1].ToString();
			dl_Unit.DataBind();
			dl_Unit.Items.Insert(0, "-선택-") ;
			dl_Unit.Items[0].Value = "";


			//	*************************************************
			//	**  위치 드롭다운리스트... 데이타바인딩... **
			//	*************************************************
			//코드분류표에서 대분류명이 부서인 소분류명을 가지고 옴.
			string str_Loc = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '0800' " ;
			SqlCommand comm_Loc = new SqlCommand(str_Loc,conn);
			SqlDataAdapter da_Loc = new SqlDataAdapter(comm_Loc) ;
			DataSet ds_Loc = new DataSet() ;
			da_Loc.Fill(ds_Loc);
				
			dl_Location.DataSource = ds_Loc;
			dl_Location.DataTextField = ds_Loc.Tables[0].Columns[0].ToString();
			dl_Location.DataValueField = ds_Loc.Tables[0].Columns[1].ToString();
			dl_Location.DataBind();
			dl_Location.Items.Insert(0, "-선택-") ;
			dl_Location.Items[0].Value = "";



			conn.Close();	 
		}

		/// <summary>
		/// 등록버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Registration_Click(object sender, System.EventArgs e)
		{

			if(tb_EquipmentNum.Text == "0" || lb_EquipmentName.Text == "" || dl_EquipmentClassification.SelectedIndex == 0 || dl_WCName.SelectedIndex == 0)
			{
				RegisterStartupScript("","<script>alert('필수항목은 반드시 입력해주세요');</script>");
			}
			else
			{
				if(Page.IsValid == true)
				{
					string m_RequirePage = "EquipmentInfo";
					string m_RequireAction = "Registration";
					string m_TableName = "EI_MT";
					int m_Idx = (int)PageName.EquipmentInfo;
					string m_User = Session["ID"].ToString();
					string m_RecodeIdx = lb_Index.Text;
					
					ArrayList InputList = new ArrayList();
					InputList.Add(tb_EquipmentNum.Text);
					InputList.Add(tb_EquipmentName.Text);
					InputList.Add(dl_EquipmentClassification.SelectedItem.Value);
					InputList.Add(tb_Standard.Text);
					if(tb_Capacity.Text.Trim() == "")
						InputList.Add(1);
					else
					{
						if(decimal.Parse(tb_Capacity.Text) <=1)
							InputList.Add(1);
						else
							InputList.Add(decimal.Parse(tb_Capacity.Text));
					}

					if(tb_ElectricCapacity.Text.Trim() == "")
						InputList.Add(0);
                    else
						InputList.Add(decimal.Parse(tb_ElectricCapacity.Text));

					if(tb_UnitTimeUseCost.Text.Trim() =="")
						InputList.Add(0);
					else
						InputList.Add(decimal.Parse(tb_UnitTimeUseCost.Text));

					InputList.Add(dl_Unit.SelectedItem.Value);
					InputList.Add(dl_WCName.SelectedItem.Value);
					
					if(tb_InjectionStaffNum.Text.Trim() == "")
						InputList.Add(0);
					else
						InputList.Add(decimal.Parse(tb_InjectionStaffNum.Text));

					InputList.Add(dl_Location.SelectedItem.Value);
					
					if(tb_OccupancyArea.Text.Trim()=="")
						InputList.Add(0);
					else
						InputList.Add(decimal.Parse(tb_OccupancyArea.Text));

					if(wdc_BuyingDate.Text.Trim() =="")
						InputList.Add("2076-06-06");
					else
						InputList.Add(wdc_BuyingDate.Text);

					if(tb_BuyingCost.Text.Trim()=="")
						InputList.Add(0);
					else
						InputList.Add(decimal.Parse(tb_BuyingCost.Text));

					InputList.Add(tb_InstrumentNum.Text);
					InputList.Add(dl_EquipmentState.SelectedItem.Value);
					if(wdc_CheckDate.Text.Trim() =="")
						InputList.Add("2076-06-06");
					else
						InputList.Add(wdc_CheckDate.Text);

					if(wdc_ValidPeriod.Text.Trim() =="")
						InputList.Add("2076-06-06");
					else
						InputList.Add(wdc_ValidPeriod.Text);
					InputList.Add(tb_CheckAgency.Text);

					if(tb_ReadyTime.Text.Trim() == "")
						InputList.Add(0);
					else
						InputList.Add(decimal.Parse(tb_ReadyTime.Text));

					if(tb_Cavity.Text.Trim()=="")
						InputList.Add(0);
					else
						InputList.Add(decimal.Parse(tb_Cavity.Text));

					if(tb_ValidityYear.Text.Trim()=="")
						InputList.Add(0);
					else
                        InputList.Add(decimal.Parse(tb_ValidityYear.Text));

					if(tb_DesignShot.Text.Trim()=="")
						InputList.Add(0);
					else
						InputList.Add(decimal.Parse(tb_DesignShot.Text));

					if(tb_FirstShot.Text.Trim()=="")
						InputList.Add(0);
					else
						InputList.Add(decimal.Parse(tb_FirstShot.Text));

					if(tb_TotalShot.Text.Trim()=="")
						InputList.Add(0);
					else
						InputList.Add(decimal.Parse(tb_TotalShot.Text));

					if(tb_WorkShot.Text.Trim()=="")
						InputList.Add(0);
					else
						InputList.Add(decimal.Parse(tb_WorkShot.Text));

					InputList.Add(tb_ManagePeriod1.Text);
					
					if(wdc_ManageDate1.Text.Trim() =="")
						InputList.Add("2076-06-06");
					else
						InputList.Add(wdc_ManageDate1.Text);
					InputList.Add(tb_ManagePeriod2.Text);
					
					if(wdc_ManageDate2.Text.Trim() =="")
						InputList.Add("2076-06-06");
					else
						InputList.Add(wdc_ManageDate2.Text);
					InputList.Add(tb_ManagePeriod3.Text);
					
					if(wdc_ManageDate3.Text.Trim() =="")
						InputList.Add("2076-06-06");
					else
						InputList.Add(wdc_ManageDate3.Text);
					InputList.Add(tb_ManagePeriod4.Text);
					
					
					if(wdc_ManageDate4.Text.Trim() =="")
						InputList.Add("2076-06-06");
					else
						InputList.Add(wdc_ManageDate4.Text);


					
					MasterInfoRecordRUD rud = new MasterInfoRecordRUD(m_RequirePage, m_RequireAction, m_TableName, m_Idx, m_User, InputList,"낱개입력");
					rud.ActivateRUD();


					bt_Delete.Enabled = false;
					bt_Update.Enabled = false;
					bt_Registration.Enabled = true;
				}
				this.databind();
			}
		
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
				
				string m_RequirePage = "EquipmentInfo";
				string m_RequireAction = "Delete";
				string m_TableName = "EI_MT";
				int m_Idx = (int)PageName.EquipmentInfo;
				string m_User = Session["ID"].ToString();
				string m_RecodeIdx = lb_Index.Text;
			
			
				ArrayList InputList = new ArrayList();
				InputList.Add(tb_EquipmentNum.Text);
				InputList.Add(tb_EquipmentName.Text);
				InputList.Add(dl_EquipmentClassification.SelectedItem.Value);
				InputList.Add(tb_Standard.Text);
				if(tb_Capacity.Text.Trim() == "")
					InputList.Add(1);
				else
					InputList.Add(decimal.Parse(tb_Capacity.Text));

				if(tb_ElectricCapacity.Text.Trim() == "")
					InputList.Add(0);
				else
					InputList.Add(decimal.Parse(tb_ElectricCapacity.Text));

				if(tb_UnitTimeUseCost.Text.Trim() =="")
					InputList.Add(0);
				else
					InputList.Add(decimal.Parse(tb_UnitTimeUseCost.Text));

				InputList.Add(dl_Unit.SelectedItem.Value);
				InputList.Add(dl_WCName.SelectedItem.Value);
					
				if(tb_InjectionStaffNum.Text.Trim() == "")
					InputList.Add(0);
				else
					InputList.Add(decimal.Parse(tb_InjectionStaffNum.Text));

				InputList.Add(dl_Location.SelectedItem.Value);
					
				if(tb_OccupancyArea.Text.Trim()=="")
					InputList.Add(0);
				else
					InputList.Add(decimal.Parse(tb_OccupancyArea.Text));

				if(wdc_BuyingDate.Text.Trim() =="")
					InputList.Add("1900-01-01");
				else
					InputList.Add(wdc_BuyingDate.Text);

				if(tb_BuyingCost.Text.Trim()=="")
					InputList.Add(0);
				else
					InputList.Add(decimal.Parse(tb_BuyingCost.Text));

				InputList.Add(tb_InstrumentNum.Text);
				InputList.Add(dl_EquipmentState.SelectedItem.Value);
				if(wdc_CheckDate.Text.Trim() =="")
					InputList.Add("2076-06-06");
				else
					InputList.Add(wdc_CheckDate.Text);

				if(wdc_ValidPeriod.Text.Trim() =="")
					InputList.Add("2076-06-06");
				else
					InputList.Add(wdc_ValidPeriod.Text);
				InputList.Add(tb_CheckAgency.Text);

				if(tb_ReadyTime.Text.Trim() =="")
					InputList.Add(0);
				else
					InputList.Add(decimal.Parse(tb_ReadyTime.Text));

				if(tb_Cavity.Text.Trim()=="")
					InputList.Add(0);
				else
					InputList.Add(decimal.Parse(tb_Cavity.Text));

				if(tb_ValidityYear.Text.Trim()=="")
					InputList.Add(0);
				else
					InputList.Add(decimal.Parse(tb_ValidityYear.Text));

				if(tb_DesignShot.Text.Trim()=="")
					InputList.Add(0);
				else
					InputList.Add(decimal.Parse(tb_DesignShot.Text));

				if(tb_FirstShot.Text.Trim()=="")
					InputList.Add(0);
				else
					InputList.Add(decimal.Parse(tb_FirstShot.Text));

				if(tb_TotalShot.Text.Trim()=="")
					InputList.Add(0);
				else
					InputList.Add(decimal.Parse(tb_TotalShot.Text));

				if(tb_WorkShot.Text.Trim()=="")
					InputList.Add(0);
				else
					InputList.Add(decimal.Parse(tb_WorkShot.Text));

				InputList.Add(tb_ManagePeriod1.Text);
					
				if(wdc_ManageDate1.Text.Trim() =="")
					InputList.Add("2076-06-06");
				else
					InputList.Add(wdc_ManageDate1.Text);
				InputList.Add(tb_ManagePeriod2.Text);
					
				if(wdc_ManageDate2.Text.Trim() =="")
					InputList.Add("2076-06-06");
				else
					InputList.Add(wdc_ManageDate2.Text);
				InputList.Add(tb_ManagePeriod3.Text);
					
				if(wdc_ManageDate3.Text.Trim() =="")
					InputList.Add("2076-06-06");
				else
					InputList.Add(wdc_ManageDate3.Text);
				InputList.Add(tb_ManagePeriod4.Text);
					
					
				if(wdc_ManageDate4.Text.Trim() =="")
					InputList.Add("2076-06-06");
				else
					InputList.Add(wdc_ManageDate4.Text);

				InputList.Add(lb_EquipmentIndex.Text);

				MasterInfoRecordRUD rud = new MasterInfoRecordRUD(m_RequirePage, m_RequireAction, m_TableName, m_Idx, m_User, InputList,"낱개입력");
				rud.ActivateRUD();

				bt_Delete.Enabled = false;
				bt_Update.Enabled = false;
				bt_Registration.Enabled = true;
				
			}
			this.clear();
			databind();
			
		}

		/// <summary>
		/// 수정버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Update_Click(object sender, System.EventArgs e)
		{

			if(Page.IsValid == true)
			{
				string m_RequirePage = "EquipmentInfo";
				string m_RequireAction = "Update";
				string m_TableName = "EI_MT";
				int m_Idx = (int)PageName.EquipmentInfo;
				string m_User = Session["ID"].ToString();
				string m_RecodeIdx = lb_EquipmentIndex.Text;
			
			
				ArrayList InputList = new ArrayList();
				InputList.Add(tb_EquipmentNum.Text);
				InputList.Add(tb_EquipmentName.Text);
				InputList.Add(dl_EquipmentClassification.SelectedItem.Value);
				InputList.Add(tb_Standard.Text);
				if(tb_Capacity.Text.Trim() == "")
					InputList.Add(1);
				else
				{
					if(decimal.Parse(tb_Capacity.Text) <=1)
						InputList.Add(1);
					else
						InputList.Add(decimal.Parse(tb_Capacity.Text));
				}

				if(tb_ElectricCapacity.Text.Trim() == "")
					InputList.Add(0);
				else
					InputList.Add(decimal.Parse(tb_ElectricCapacity.Text));

				if(tb_UnitTimeUseCost.Text.Trim() =="")
					InputList.Add(0);
				else
					InputList.Add(decimal.Parse(tb_UnitTimeUseCost.Text));

				InputList.Add(dl_Unit.SelectedItem.Value);
				InputList.Add(dl_WCName.SelectedItem.Value);
					
				if(tb_InjectionStaffNum.Text.Trim() == "")
					InputList.Add(0);
				else
					InputList.Add(decimal.Parse(tb_InjectionStaffNum.Text));

				InputList.Add(dl_Location.SelectedItem.Value);
					
				if(tb_OccupancyArea.Text.Trim()=="")
					InputList.Add(0);
				else
					InputList.Add(decimal.Parse(tb_OccupancyArea.Text));

				if(wdc_BuyingDate.Text.Trim() =="")
					InputList.Add("1900-01-01");
				else
					InputList.Add(wdc_BuyingDate.Text);

				if(tb_BuyingCost.Text.Trim()=="")
					InputList.Add(0);
				else
					InputList.Add(decimal.Parse(tb_BuyingCost.Text));

				InputList.Add(tb_InstrumentNum.Text);
				InputList.Add(dl_EquipmentState.SelectedItem.Value);
				if(wdc_CheckDate.Text.Trim() =="")
					InputList.Add("2076-06-06");
				else
					InputList.Add(wdc_CheckDate.Text);

				if(wdc_ValidPeriod.Text.Trim() =="")
					InputList.Add("2076-06-06");
				else
					InputList.Add(wdc_ValidPeriod.Text);
				InputList.Add(tb_CheckAgency.Text);

				if(tb_ReadyTime.Text.Trim() == "")
					InputList.Add(0);
				else
					InputList.Add(decimal.Parse(tb_ReadyTime.Text));

				if(tb_Cavity.Text.Trim()=="")
					InputList.Add(0);
				else
					InputList.Add(decimal.Parse(tb_Cavity.Text));

				if(tb_ValidityYear.Text.Trim()=="")
					InputList.Add(0);
				else
					InputList.Add(decimal.Parse(tb_ValidityYear.Text));

				if(tb_DesignShot.Text.Trim()=="")
					InputList.Add(0);
				else
					InputList.Add(decimal.Parse(tb_DesignShot.Text));

				if(tb_FirstShot.Text.Trim()=="")
					InputList.Add(0);
				else
					InputList.Add(decimal.Parse(tb_FirstShot.Text));

				if(tb_TotalShot.Text.Trim()=="")
					InputList.Add(0);
				else
					InputList.Add(decimal.Parse(tb_TotalShot.Text));

				if(tb_WorkShot.Text.Trim()=="")
					InputList.Add(0);
				else
					InputList.Add(decimal.Parse(tb_WorkShot.Text));

				InputList.Add(tb_ManagePeriod1.Text);
					
				if(wdc_ManageDate1.Text.Trim() =="")
					InputList.Add("2076-06-06");
				else
					InputList.Add(wdc_ManageDate1.Text);
				InputList.Add(tb_ManagePeriod2.Text);
					
				if(wdc_ManageDate2.Text.Trim() =="")
					InputList.Add("2076-06-06");
				else
					InputList.Add(wdc_ManageDate2.Text);
				InputList.Add(tb_ManagePeriod3.Text);
					
				if(wdc_ManageDate3.Text.Trim() =="")
					InputList.Add("2076-06-06");
				else
					InputList.Add(wdc_ManageDate3.Text);
				InputList.Add(tb_ManagePeriod4.Text);
					
					
				if(wdc_ManageDate4.Text.Trim() =="")
					InputList.Add("2076-06-06");
				else
					InputList.Add(wdc_ManageDate4.Text);

				InputList.Add(lb_EquipmentIndex.Text);

				MasterInfoRecordRUD rud = new MasterInfoRecordRUD(m_RequirePage, m_RequireAction, m_TableName, m_Idx, m_User, InputList,"낱개입력");
				rud.ActivateRUD();

				databind();

				bt_Delete.Enabled = false;
				bt_Update.Enabled = false;
				bt_Registration.Enabled = true;
			}
		}

		/// <summary>
		/// 초기화버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Clear_Click(object sender, System.EventArgs e)
		{
			this.clear();
			bt_Delete.Enabled = false;
			bt_Update.Enabled = false;
			bt_Registration.Enabled = true;
		}


		/// <summary>
		/// 설비웹콤보 선택
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void wc_EquipmentChoice_SelectedRowChanged(object sender, Infragistics.WebUI.WebCombo.SelectedRowChangedEventArgs e)
		{
			if(!Convert.IsDBNull(wc_EquipmentChoice.DataValue))
			{
				lb_EquipmentIndex.Text = Convert.ToString(wc_EquipmentChoice.DataValue);

				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				conn.Open();
				string str = "SELECT * FROM EI_MT Where RecodingState=1 and EquipmentInfoIndex = @index";
				SqlCommand comm = new SqlCommand(str,conn);
				if(lb_EquipmentIndex.Text == "")
					lb_EquipmentIndex.Text = "-1";
				comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(lb_EquipmentIndex.Text);
				SqlDataReader dr = comm.ExecuteReader();
				while(dr.Read())
				{
					tb_EquipmentNum.Text = dr["EquipmentNum"].ToString();
					tb_EquipmentName.Text = dr["EquipmentName"].ToString();

					for (int i=0 ; i < dl_EquipmentClassification.Items.Count ; i++)
					{
						if( dl_EquipmentClassification.Items[i].Value == dr["EquipmentClassification"].ToString())
						{
							dl_EquipmentClassification.SelectedIndex = i;				
						}
					}
					tb_Standard.Text = dr["Standard"].ToString();
					tb_Capacity.Text = dr["Capacity"].ToString();
					tb_ElectricCapacity.Text = dr["ElectricCapacity"].ToString();
					tb_UnitTimeUseCost.Text = dr["UnitTimeUseCost"].ToString();
				
					for (int i=0 ; i < dl_Unit.Items.Count ; i++)
					{
						if( dl_Unit.Items[i].Value == dr["Unit"].ToString())
						{
							dl_Unit.SelectedIndex = i;				
						}
					}

					for (int i=0 ; i < dl_WCName.Items.Count ; i++)
					{
						if( dl_WCName.Items[i].Value == dr["WCName"].ToString())
						{
							dl_WCName.SelectedIndex = i;

						}
					}

					tb_InjectionStaffNum.Text = dr["InjectionStaffNum"].ToString();

					for (int i=0 ; i < dl_Location.Items.Count ; i++)
					{
						if( dl_Location.Items[i].Value == dr["Location"].ToString())
						{
							dl_Location.SelectedIndex = i;				
						}
					}

					tb_OccupancyArea.Text = dr["OccupancyArea"].ToString();
					if(DateTime.Parse(dr["BuyingDate"].ToString()).ToShortDateString() == "1900-01-01" || DateTime.Parse(dr["BuyingDate"].ToString()).ToShortDateString() == "2076-06-06")
						wdc_BuyingDate.Value = "";
					else
						wdc_BuyingDate.Value = dr["BuyingDate"].ToString();
					tb_BuyingCost.Text = dr["BuyingCost"].ToString();
					tb_InstrumentNum.Text = dr["InstrumentNum"].ToString();


					for (int i=0 ; i < dl_EquipmentState.Items.Count ; i++)
					{
						if( dl_EquipmentState.Items[i].Value == dr["EquipmentState"].ToString())
						{
							dl_EquipmentState.SelectedIndex = i;				
						}
					}
				
					if(DateTime.Parse(dr["CheckDate"].ToString()).ToShortDateString() == "1900-01-01" || DateTime.Parse(dr["CheckDate"].ToString()).ToShortDateString() == "2076-06-06")
						wdc_CheckDate.Value = "";
					else
						wdc_CheckDate.Value = dr["CheckDate"].ToString();
					
					if(DateTime.Parse(dr["ValidPeriod"].ToString()).ToShortDateString() == "1900-01-01" || DateTime.Parse(dr["ValidPeriod"].ToString()).ToShortDateString() == "2076-06-06")
						wdc_ValidPeriod.Value = "";
					else
						wdc_ValidPeriod.Value = dr["ValidPeriod"].ToString();
					tb_CheckAgency.Text = dr["CheckAgency"].ToString();
					tb_ReadyTime.Text = dr["ReadyTime"].ToString();
					tb_Cavity.Text = dr["Cavity"].ToString();
					tb_ValidityYear.Text = dr["ValidityYear"].ToString();
					tb_DesignShot.Text = dr["DesignShot"].ToString();
					tb_FirstShot.Text = dr["FirstShot"].ToString();
					tb_TotalShot.Text = dr["TotalShot"].ToString();
					tb_WorkShot.Text = dr["WorkShot"].ToString();
					tb_ManagePeriod1.Text = dr["ManagePeriod1"].ToString();
					tb_ManagePeriod2.Text = dr["ManagePeriod2"].ToString();
					tb_ManagePeriod3.Text = dr["ManagePeriod3"].ToString();
					tb_ManagePeriod4.Text = dr["ManagePeriod4"].ToString();

					if(DateTime.Parse(dr["ManageDate1"].ToString()).ToShortDateString() == "1900-01-01" || DateTime.Parse(dr["ManageDate1"].ToString()).ToShortDateString() == "2076-06-06")
						wdc_ManageDate1.Value = "";
					else
						wdc_ManageDate1.Value = dr["ManageDate1"].ToString();
					if(DateTime.Parse(dr["ManageDate2"].ToString()).ToShortDateString() == "1900-01-01" || DateTime.Parse(dr["ManageDate2"].ToString()).ToShortDateString() == "2076-06-06")
						wdc_ManageDate2.Value = "";
					else
						wdc_ManageDate2.Value = dr["ManageDate2"].ToString();
					if(DateTime.Parse(dr["ManageDate3"].ToString()).ToShortDateString() == "1900-01-01" || DateTime.Parse(dr["ManageDate3"].ToString()).ToShortDateString() == "2076-06-06")
						wdc_ManageDate3.Value = "";
					else
						wdc_ManageDate3.Value = dr["ManageDate3"].ToString();
					if(DateTime.Parse(dr["ManageDate4"].ToString()).ToShortDateString() == "1900-01-01" || DateTime.Parse(dr["ManageDate4"].ToString()).ToShortDateString() == "2076-06-06")
						wdc_ManageDate4.Value = "";
					else
						wdc_ManageDate4.Value = dr["ManageDate4"].ToString();
					lb_EquipmentIndex.Text = dr["EquipmentInfoIndex"].ToString();

					lb_Index.Text = lb_EquipmentIndex.Text; //참조보기를 위한 인덱스

					bt_Delete.Enabled = true;
					bt_Update.Enabled = true;
					bt_Registration.Enabled = false;

				}
				conn.Close();
			}
		}


		/// <summary>
		/// 참고보기 버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_RelationView_Click(object sender, System.EventArgs e)
		{
			if(lb_Index.Text == "" || lb_Index.Text == "0")
			{
				RegisterStartupScript("","<script>alert('항목을 선택후 버튼을 눌러주세요');</script>");
			}
			else
			{

				
				string m_TableName = "설비정보";
				int m_RecodeIdx = int.Parse(lb_Index.Text);
			

				Reference rf = new Reference(m_TableName, m_RecodeIdx);
				UltraWebGrid1.DataSource = rf.reference().Tables[0];
				UltraWebGrid1.DataBind();
			}
		}

		/// <summary>
		/// 설비 웹콤보 바인딩
		/// </summary>
		private void databind()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "SELECT EquipmentNum, EquipmentName, InstrumentNum, EquipmentInfoIndex FROM EI_MT Where RecodingState=1 order by EquipmentNum";
			SqlCommand comm =  new SqlCommand(str,conn);
			SqlDataAdapter da = new SqlDataAdapter(comm) ;
			DataSet ds = new DataSet() ;
			da.Fill(ds);

			wc_EquipmentChoice.DataSource = ds;
			wc_EquipmentChoice.DataTextField = ds.Tables[0].Columns[1].ToString();
			wc_EquipmentChoice.DataValueField = ds.Tables[0].Columns[3].ToString();
			wc_EquipmentChoice.DataBind();
		}


		/// <summary>
		/// 초기화 함수
		/// </summary>
		private void clear()
		{
			tb_EquipmentNum.Text = "";
			tb_EquipmentName.Text = "";
			dl_EquipmentClassification.SelectedIndex = 0;
			tb_Standard.Text = "";
			tb_Capacity.Text = "0";
			tb_ElectricCapacity.Text = "0";
			tb_UnitTimeUseCost.Text = "0";
			dl_Unit.SelectedIndex = 0;
			dl_WCName.SelectedIndex = 0;
			tb_InjectionStaffNum.Text = "0";
			dl_Location.SelectedIndex = 0;
			tb_OccupancyArea.Text = "0";
			wdc_BuyingDate.Value = " ";
			tb_BuyingCost.Text = "0";
			tb_InstrumentNum.Text = "";
			dl_EquipmentState.SelectedIndex = 0;
			wdc_CheckDate.Value = "";
			wdc_ValidPeriod.Value = "";
			tb_CheckAgency.Text = "";
			tb_ReadyTime.Text = "0";
			tb_Cavity.Text = "1";
			tb_ValidityYear.Text = "0";
			tb_DesignShot.Text = "0";
			tb_FirstShot.Text = "0";
			tb_TotalShot.Text = "0";
			tb_WorkShot.Text = "0";
			tb_ManagePeriod1.Text = "";
			wdc_ManageDate1.Value = " ";
			tb_ManagePeriod2.Text = "";
			wdc_ManageDate2.Value = " ";
			tb_ManagePeriod3.Text = "";
			wdc_ManageDate3.Value = " ";
			tb_ManagePeriod4.Text = "";
			wdc_ManageDate4.Value = " ";
			wc_EquipmentChoice.DisplayValue = "";
			wc_EquipmentChoice.DataValue = "";
			lb_EquipmentIndex.Text = "0";
		}


		/// <summary>
		/// 어깨창 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LinkButton1_Click(object sender, System.EventArgs e)
		{
			RegisterStartupScript("","<script>window.open('./Popup/Equipment.aspx','Equipment','width=830,height=500px');</script>");
		}

		/// <summary>
		/// 참조보기 그리드 더블클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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
			
			//RegisterStartupScript("","<script>window.open('./Popup/View.aspx?table=" + table + "&index=" + index + "','Reference'," + "'width=510" + "," + "height=435px');</script>");
			RegisterStartupScript("","<script>window.open('./Popup/View1.aspx?table=" + table + "&index=" + index + "','Reference'," + "'width=818" + "," + "height=610px');</script>");
		}
		
	}
}
