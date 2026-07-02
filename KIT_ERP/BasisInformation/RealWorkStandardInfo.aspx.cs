using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Data.SqlClient;
namespace KIT_ERP.BasisInformation
{
	/// <summary>
	/// RealWorkStandardInfo에 대한 요약 설명입니다.
	/// </summary>
	public class RealWorkStandardInfo : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		protected System.Web.UI.WebControls.LinkButton LinkButton2;
		protected System.Web.UI.WebControls.TextBox tb_PropertyClassification;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid Ultrawebgrid3;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.DropDownList dl_WCName;
		protected System.Web.UI.WebControls.TextBox tb_PriorityOrder;
		protected System.Web.UI.WebControls.DropDownList dl_MainWorker;
		protected System.Web.UI.WebControls.TextBox tb_Cavity;
		protected System.Web.UI.WebControls.DropDownList dl_UseTool1;
		protected System.Web.UI.WebControls.DropDownList dl_UseJig1;
		protected System.Web.UI.WebControls.DropDownList dl_UseTool2;
		protected System.Web.UI.WebControls.DropDownList dl_UseJig2;
		protected System.Web.UI.WebControls.DropDownList dl_UseTool3;
		protected System.Web.UI.WebControls.DropDownList dl_UseJig3;
		protected System.Web.UI.WebControls.TextBox tb_SetupTime;
		protected System.Web.UI.WebControls.TextBox tb_StandardTime;
		protected System.Web.UI.WebControls.TextBox tb_RealProcessingTime;
		protected System.Web.UI.WebControls.TextBox tb_SpaceTime;
		protected System.Web.UI.WebControls.TextBox tb_WaitTime;
		protected System.Web.UI.WebControls.TextBox tb_LotSize;
		protected System.Web.UI.WebControls.Button bt_RelationView;
		protected System.Web.UI.WebControls.Button bt_Clear;
		protected System.Web.UI.WebControls.Button bt_Update;
		protected System.Web.UI.WebControls.Button bt_Delete;
		protected System.Web.UI.WebControls.Button bt_Registration;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid Ultrawebgrid2;
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected System.Web.UI.WebControls.Button pop;
		protected System.Web.UI.WebControls.Button bt_WorkStandard;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Index;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_ReferenceTable;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_ReferenceIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdProcessNum;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdItemNum;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdProcess;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_RowIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_ItemIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Item;
		protected System.Web.UI.HtmlControls.HtmlInputHidden ProcessSequenceInfoIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_ProcessCode1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_ProcessCode;

		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			ItemSearchControl1.Products = true;				// 제품
			ItemSearchControl1.HalfFinishedProducts = true;	// 반제품
			ItemSearchControl1.IsDoPost = true;


			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(!Page.IsPostBack)
			{
				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 기준정보 > 작업표준정보';</script>");

				bt_Registration.Attributes.Add("onClick", "return OK('입력한 항목을 등록');");
				bt_Update.Attributes.Add("onClick", "return OK('선택한 항목을 수정');");
				bt_Delete.Attributes.Add("onClick", "return OK('선택한 항목을 삭제');");

				bt_Clear.Attributes.Add("onClick", "ResetTextBox();");


				tb_PriorityOrder.Attributes.Add("OnKeyDown", "OnKeyDown_Currency(this);");
				tb_PriorityOrder.Attributes.Add("OnFocus", "OnFocus_Obj(this);");

				tb_Cavity.Attributes.Add("OnKeyDown", "OnKeyDown_Currency(this);");
				tb_Cavity.Attributes.Add("OnFocus", "OnFocus_Obj(this);");

				tb_SetupTime.Attributes.Add("OnKeyDown", "OnKeyDown_Currency(this);");
				tb_SetupTime.Attributes.Add("OnFocus", "OnFocus_Obj(this);");

				tb_StandardTime.Attributes.Add("OnKeyDown", "OnKeyDown_Currency(this);");
				tb_StandardTime.Attributes.Add("OnFocus", "OnFocus_Obj(this);");

				tb_RealProcessingTime.Attributes.Add("OnKeyDown", "OnKeyDown_Currency(this);");
				tb_RealProcessingTime.Attributes.Add("OnFocus", "OnFocus_Obj(this);");

				tb_SpaceTime.Attributes.Add("OnKeyDown", "OnKeyDown_Currency(this);");
				tb_SpaceTime.Attributes.Add("OnFocus", "OnFocus_Obj(this);");

				tb_WaitTime.Attributes.Add("OnKeyDown", "OnKeyDown_Currency(this);");
				tb_WaitTime.Attributes.Add("OnFocus", "OnFocus_Obj(this);");

				tb_LotSize.Attributes.Add("OnKeyDown", "OnKeyDown_Currency(this);");
				tb_LotSize.Attributes.Add("OnFocus", "OnFocus_Obj(this);");

				page_Load();
			}
		}

		private void page_Load()
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
			//	**  주작업자 드롭다운리스트... 데이타바인딩... **
			//	*************************************************
			// 사용자정보테이블에서에서 작업자명과 작업자ID를 가지고옴
			string str_MainWorker = "Select Name, ID from UI_MT where RecodingState = 1";
			SqlCommand comm_MainWorker = new SqlCommand(str_MainWorker,conn);
			SqlDataAdapter da_MainWorker = new SqlDataAdapter(comm_MainWorker) ;
			DataSet ds_MainWorker = new DataSet() ;
			da_MainWorker.Fill(ds_MainWorker);
				
			dl_MainWorker.DataSource = ds_MainWorker;
			dl_MainWorker.DataTextField = ds_MainWorker.Tables[0].Columns[0].ToString();
			dl_MainWorker.DataValueField = ds_MainWorker.Tables[0].Columns[1].ToString();
			dl_MainWorker.DataBind();
			dl_MainWorker.Items.Insert(0, "-선택-") ;
			dl_MainWorker.Items[0].Value = "";
			


			//	*************************************************
			//	**  사용공구1 드롭다운리스트... 데이타바인딩... **
			//	*************************************************
			// 설비정보에서 설비분류가 공구인것을 가지고 옴.
			string str_Tool1 = "Select EquipmentNum, EquipmentName from EI_MT where RecodingState = 1 and EquipmentClassification = '07200010'" ;
			SqlCommand comm_Tool1 = new SqlCommand(str_Tool1,conn);
			SqlDataAdapter da_Tool1 = new SqlDataAdapter(comm_Tool1) ;
			DataSet ds_Tool1 = new DataSet() ;
			da_Tool1.Fill(ds_Tool1);
				
			dl_UseTool1.DataSource = ds_Tool1;
			dl_UseTool1.DataTextField = ds_Tool1.Tables[0].Columns[0].ToString();
			dl_UseTool1.DataValueField = ds_Tool1.Tables[0].Columns[0].ToString();
			dl_UseTool1.DataBind();
			dl_UseTool1.Items.Insert(0, "-선택-") ;
			dl_UseTool1.Items[0].Value = "";

			//	*************************************************
			//	**  사용공구2 드롭다운리스트... 데이타바인딩... **
			//	*************************************************
			// 설비정보에서 설비분류가 공구인것을 가지고 옴.
			string str_Tool2 = "Select EquipmentNum, EquipmentName from EI_MT where RecodingState = 1 and EquipmentClassification = '07200010'" ;
			SqlCommand comm_Tool2 = new SqlCommand(str_Tool2,conn);
			SqlDataAdapter da_Tool2 = new SqlDataAdapter(comm_Tool2) ;
			DataSet ds_Tool2 = new DataSet() ;
			da_Tool2.Fill(ds_Tool2);
				
			dl_UseTool2.DataSource = ds_Tool2;
			dl_UseTool2.DataTextField = ds_Tool2.Tables[0].Columns[0].ToString();
			dl_UseTool2.DataValueField = ds_Tool2.Tables[0].Columns[0].ToString();
			dl_UseTool2.DataBind();
			dl_UseTool2.Items.Insert(0, "-선택-") ;
			dl_UseTool2.Items[0].Value = "";

			//	*************************************************
			//	**  사용공구3 드롭다운리스트... 데이타바인딩... **
			//	*************************************************
			// 설비정보에서 설비분류가 공구인것을 가지고 옴.
			string str_Tool3 = "Select EquipmentNum, EquipmentName from EI_MT where RecodingState = 1 and EquipmentClassification = '07200010'" ;
			SqlCommand comm_Tool3 = new SqlCommand(str_Tool3,conn);
			SqlDataAdapter da_Tool3 = new SqlDataAdapter(comm_Tool3) ;
			DataSet ds_Tool3 = new DataSet() ;
			da_Tool3.Fill(ds_Tool3);
				
			dl_UseTool3.DataSource = ds_Tool3;
			dl_UseTool3.DataTextField = ds_Tool3.Tables[0].Columns[0].ToString();
			dl_UseTool3.DataValueField = ds_Tool3.Tables[0].Columns[0].ToString();
			dl_UseTool3.DataBind();
			dl_UseTool3.Items.Insert(0, "-선택-") ;
			dl_UseTool3.Items[0].Value = "";

			//	*************************************************
			//	**  사용치구1 드롭다운리스트... 데이타바인딩... **
			//	*************************************************
			// 설비정보에서 설비분류가 치구인것을 가지고 옴.
			string str_Jig1 = "Select EquipmentNum, EquipmentName from EI_MT where RecodingState = 1 and EquipmentClassification = '07200020'" ;
			SqlCommand comm_Jig1 = new SqlCommand(str_Jig1,conn);
			SqlDataAdapter da_Jig1 = new SqlDataAdapter(comm_Jig1) ;
			DataSet ds_Jig1 = new DataSet() ;
			da_Jig1.Fill(ds_Jig1);
				
			dl_UseJig1.DataSource = ds_Jig1;
			dl_UseJig1.DataTextField = ds_Jig1.Tables[0].Columns[0].ToString();
			dl_UseJig1.DataValueField = ds_Jig1.Tables[0].Columns[0].ToString();
			dl_UseJig1.DataBind();
			dl_UseJig1.Items.Insert(0, "-선택-") ;
			dl_UseJig1.Items[0].Value = "";

			//	*************************************************
			//	**  사용치구2 드롭다운리스트... 데이타바인딩... **
			//	*************************************************
			// 설비정보에서 설비분류가 치구인것을 가지고 옴.
			string str_Jig2 = "Select EquipmentNum, EquipmentName from EI_MT where RecodingState = 1 and EquipmentClassification = '07200020'" ;
			SqlCommand comm_Jig2 = new SqlCommand(str_Jig2,conn);
			SqlDataAdapter da_Jig2 = new SqlDataAdapter(comm_Jig2) ;
			DataSet ds_Jig2 = new DataSet() ;
			da_Jig2.Fill(ds_Jig2);
				
			dl_UseJig2.DataSource = ds_Jig2;
			dl_UseJig2.DataTextField = ds_Jig2.Tables[0].Columns[0].ToString();
			dl_UseJig2.DataValueField = ds_Jig2.Tables[0].Columns[0].ToString();
			dl_UseJig2.DataBind();
			dl_UseJig2.Items.Insert(0, "-선택-") ;
			dl_UseJig2.Items[0].Value = "";

			//	*************************************************
			//	**  사용치구3 드롭다운리스트... 데이타바인딩... **
			//	*************************************************
			// 설비정보에서 설비분류가 치구인것을 가지고 옴.
			string str_Jig3 = "Select EquipmentNum, EquipmentName from EI_MT where RecodingState = 1 and EquipmentClassification = '07200020'" ;
			SqlCommand comm_Jig3 = new SqlCommand(str_Jig3,conn);
			SqlDataAdapter da_Jig3 = new SqlDataAdapter(comm_Jig3) ;
			DataSet ds_Jig3 = new DataSet() ;
			da_Jig3.Fill(ds_Jig3);
				
			dl_UseJig3.DataSource = ds_Jig3;
			dl_UseJig3.DataTextField = ds_Jig3.Tables[0].Columns[0].ToString();
			dl_UseJig3.DataValueField = ds_Jig3.Tables[0].Columns[0].ToString();
			dl_UseJig3.DataBind();
			dl_UseJig3.Items.Insert(0, "-선택-") ;
			dl_UseJig3.Items[0].Value = "";

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
			this.LinkButton2.Click += new System.EventHandler(this.LinkButton2_Click);
			this.Ultrawebgrid3.DblClick += new Infragistics.WebUI.UltraWebGrid.ClickEventHandler(this.Ultrawebgrid3_DblClick);
			this.bt_RelationView.Click += new System.EventHandler(this.bt_RelationView_Click);
			this.bt_Clear.Click += new System.EventHandler(this.bt_Clear_Click);
			this.bt_Update.Click += new System.EventHandler(this.bt_Update_Click);
			this.bt_Delete.Click += new System.EventHandler(this.bt_Delete_Click);
			this.bt_Registration.Click += new System.EventHandler(this.bt_Registration_Click);
			this.Ultrawebgrid2.DblClick += new Infragistics.WebUI.UltraWebGrid.ClickEventHandler(this.Ultrawebgrid2_DblClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void bt_Update_Click(object sender, System.EventArgs e)
		{
			if(lb_Index.Value == "0")
			{
				RegisterStartupScript("","<script>alert('등록후 바로 수정은 불가능 합니다!');</script>");
			}
			else
			{
				//lb_ProcessCode.Value = lb_ProcessCode1.Value;
		
				ArrayList arr = new ArrayList();

				arr.Add(ItemSearchControl1.ItemNum);
				arr.Add(hdProcess.Value);
				arr.Add(lb_ProcessCode.Value);
				arr.Add(dl_WCName.SelectedItem.Text);
				if(tb_PriorityOrder.Text.Trim() == "")
					arr.Add(0);
				else
					arr.Add(tb_PriorityOrder.Text);
				arr.Add(dl_MainWorker.SelectedItem.Value);
				arr.Add(dl_MainWorker.SelectedItem.Text);
				arr.Add(dl_UseTool1.SelectedItem.Value);
				arr.Add(dl_UseJig1.SelectedItem.Value);
				arr.Add(dl_UseTool2.SelectedItem.Value);
				arr.Add(dl_UseJig2.SelectedItem.Value);
				arr.Add(dl_UseTool3.SelectedItem.Value);
				arr.Add(dl_UseJig3.SelectedItem.Value);

				if(tb_SetupTime.Text.Trim() == "")
					arr.Add(0);
				else
					arr.Add(tb_SetupTime.Text);
				if(tb_RealProcessingTime.Text.Trim() == "")
					arr.Add(0);
				else
					arr.Add(tb_RealProcessingTime.Text);
				if(tb_SpaceTime.Text.Trim() == "")
					arr.Add(0);
				else
					arr.Add(tb_SpaceTime.Text);
				if(tb_StandardTime.Text.Trim() == "")
					arr.Add(0);
				else
					arr.Add(tb_StandardTime.Text);
			
				if(tb_WaitTime.Text.Trim() == "")
					arr.Add(0);
				else
					arr.Add(tb_WaitTime.Text);

				if(tb_LotSize.Text.Trim() == "")
					arr.Add(0);
				else
					arr.Add(tb_LotSize.Text);

				if(tb_Cavity.Text.Trim() == "")
					arr.Add(0);
				else
					arr.Add(tb_Cavity.Text);

				arr.Add(lb_Index.Value);		//작업표준정보 인덱스
				

				MasterInfoRecordRUD rud = new MasterInfoRecordRUD("RealWorkStandardInfo","Update","RWSI_MT",(int)PageName.RealWorkStandardInfo,Session["ID"].ToString(),arr,"낱개입력");
				rud.ActivateRUD();

				Data_Bind();
			}
		}

		/// <summary>
		/// 작업표준정보 테이블을 웹그리드에 바인딩시키는 함수
		/// </summary>
		private void Data_Bind()
		{
			//m_Grid.Rows[rowcount].Cells.FromKey("SuitabilityQuantity").Value.ToString()
			View view = new View(hdItemNum.Value.ToString(),hdProcessNum.Value.ToString(),"RealWorkStandardInfo");
			UltraWebGrid1.DataSource = view.ItemView();
			UltraWebGrid1.DataBind();
		}

		private void LinkButton2_Click(object sender, System.EventArgs e)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string str = @"Select RPSI_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName,II_MT.PropertyClassification, RPSI_MT.ProcessSequenceNum, RPSI_MT.ProcessCode, PUC_MT.SmallClassificationName
				From RPSI_MT inner join II_MT on RPSI_MT.ItemNum = II_MT.ItemNum 
				inner join PUC_MT on RPSI_MT.ProcessCode = PUC_MT.SmallClassificationCode 
				Where RPSI_MT.RecodingState =  1 and  II_MT.RecodingState = 1 and RPSI_MT.ItemNum = @itemnum and WorkDistinction != '외주' order by RPSI_MT.ProcessSequenceNum";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@itemnum",SqlDbType.NVarChar).Value = ItemSearchControl1.ItemNum ;

			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			da.Fill(ds);

			Ultrawebgrid3.DataSource = ds;
			Ultrawebgrid3.DataBind();
			
			foreach(DataRow dr in ds.Tables[0].Rows)
			{
				tb_PropertyClassification.Text = dr["PropertyClassification"].ToString();
				break;
			}

			str = @"Select * From II_MT Where ItemNum = ''";
			SqlCommand comm1 = new SqlCommand(str,conn);
			SqlDataAdapter da1 = new SqlDataAdapter(comm1);
			DataSet ds1 = new DataSet();
			da1.Fill(ds1);
			UltraWebGrid1.DataSource = ds1;
			UltraWebGrid1.DataBind();

			conn.Close();
		}

		private void bt_Registration_Click(object sender, System.EventArgs e)
		{
			//lb_ProcessCode.Value = lb_ProcessCode1.Value;

			ArrayList arr = new ArrayList();

			arr.Add(ItemSearchControl1.ItemNum);
			arr.Add(hdProcessNum.Value);
			arr.Add(lb_ProcessCode.Value);
			arr.Add(dl_WCName.SelectedItem.Text);
			if(tb_PriorityOrder.Text.Trim() == "")
				arr.Add(0);
			else
				arr.Add(tb_PriorityOrder.Text);
			arr.Add(dl_MainWorker.SelectedItem.Value);
			arr.Add(dl_MainWorker.SelectedItem.Text);
			arr.Add(dl_UseTool1.SelectedItem.Value);
			arr.Add(dl_UseJig1.SelectedItem.Value);
			arr.Add(dl_UseTool2.SelectedItem.Value);
			arr.Add(dl_UseJig2.SelectedItem.Value);
			arr.Add(dl_UseTool3.SelectedItem.Value);
			arr.Add(dl_UseJig3.SelectedItem.Value);

			if(tb_SetupTime.Text.Trim() == "")
				arr.Add(0);
			else
				arr.Add(tb_SetupTime.Text);
			if(tb_RealProcessingTime.Text.Trim() == "")
				arr.Add(0);
			else
				arr.Add(tb_RealProcessingTime.Text);
			if(tb_SpaceTime.Text.Trim() == "")
				arr.Add(0);
			else
				arr.Add(tb_SpaceTime.Text);
			if(tb_StandardTime.Text.Trim() == "")
				arr.Add(0);
			else
				arr.Add(tb_StandardTime.Text);
			
			if(tb_WaitTime.Text.Trim() == "")
				arr.Add(0);
			else
				arr.Add(tb_WaitTime.Text);

			if(tb_LotSize.Text.Trim() == "")
				arr.Add(0);
			else
				arr.Add(tb_LotSize.Text);

			if(tb_Cavity.Text.Trim() == "")
				arr.Add(0);
			else
				arr.Add(tb_Cavity.Text);



			MasterInfoRecordRUD rud = new MasterInfoRecordRUD("RealWorkStandardInfo","Registration","RWSI_MT",(int)PageName.RealWorkStandardInfo,Session["ID"].ToString(),arr,"낱개입력");
			rud.ActivateRUD();

			Data_Bind();
		}

		private void bt_Delete_Click(object sender, System.EventArgs e)
		{
			if(lb_Index.Value == "0")
			{
				RegisterStartupScript("","<script>alert('등록후 바로 삭제는 불가능 합니다!');</script>");
			}
			else
			{
				//lb_ProcessCode.Value = lb_ProcessCode1.Value;

				ArrayList arr = new ArrayList();

				arr.Add(ItemSearchControl1.ItemNum);
				arr.Add(hdProcess.Value);
				arr.Add(lb_ProcessCode.Value);
				arr.Add(dl_WCName.SelectedItem.Text);
				arr.Add(tb_PriorityOrder.Text);
				arr.Add(dl_MainWorker.SelectedItem.Value);
				arr.Add(dl_MainWorker.SelectedItem.Text);
				arr.Add(dl_UseTool1.SelectedItem.Value);
				arr.Add(dl_UseJig1.SelectedItem.Value);
				arr.Add(dl_UseTool2.SelectedItem.Value);
				arr.Add(dl_UseJig2.SelectedItem.Value);
				arr.Add(dl_UseTool3.SelectedItem.Value);
				arr.Add(dl_UseJig3.SelectedItem.Value);
				arr.Add(tb_SetupTime.Text);
				arr.Add(tb_RealProcessingTime.Text);
				arr.Add(tb_SpaceTime.Text);
				arr.Add(tb_StandardTime.Text);
				arr.Add(tb_WaitTime.Text);
				arr.Add(tb_LotSize.Text);
				arr.Add(tb_Cavity.Text);
				arr.Add(lb_Index.Value);		//작업표준정보 인덱스
				//arr.Add(tb_Index.Text);		//작업표준정보 인덱스


				MasterInfoRecordRUD rud = new MasterInfoRecordRUD("RealWorkStandardInfo","Delete","RWSI_MT",(int)PageName.RealWorkStandardInfo,Session["ID"].ToString(),arr,"낱개입력");
				rud.ActivateRUD();

				Data_Bind();

				Data_Clear();
			}
		}

		private void bt_Clear_Click(object sender, System.EventArgs e)
		{
			lb_ProcessCode.Value = "";
			lb_ProcessCode1.Value = "";

			hdProcess.Value = "0";
			
			// 아랫부분 클리어
			Data_Clear();

			View view = new View("","0","RealWorkStandardInfo");
			UltraWebGrid1.DataSource = view.ItemView();
			UltraWebGrid1.DataBind();
			Ultrawebgrid3.DataSource= view.ItemView();
			Ultrawebgrid3.DataBind();
		}

		private void Data_Clear()
		{
			dl_WCName.SelectedIndex = 0;
			tb_PriorityOrder.Text = "";
			dl_MainWorker.SelectedIndex = 0;
			dl_UseJig1.SelectedIndex = 0;
			dl_UseJig2.SelectedIndex = 0;
			dl_UseJig3.SelectedIndex = 0;
			dl_UseTool1.SelectedIndex = 0;
			dl_UseTool2.SelectedIndex = 0;
			dl_UseTool3.SelectedIndex = 0;
			tb_Cavity.Text ="0";
			tb_RealProcessingTime.Text = "0";
			tb_SetupTime.Text = "0";
			tb_SpaceTime.Text ="0";
			tb_WaitTime.Text = "0";
			tb_StandardTime.Text ="0";
			tb_LotSize.Text ="0";		

		}

		private void LinkButton1_Click(object sender, System.EventArgs e)
		{
			RegisterStartupScript("","<script>window.open('./Popup/RealWorkStandard.aspx','WorkStandard'," + "'width=830" + "," + "height=500px');</script>");
		}

		private void bt_RelationView_Click(object sender, System.EventArgs e)
		{
			if(lb_Index.Value == "0")
			{
				RegisterStartupScript("","<script>alert('항목을 선택후 버튼을 눌러주세요');</script>");
			}
			else
			{

				string m_TableName = "진작업표준정보";
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
			
			//RegisterStartupScript("","<script>window.open('./Popup/View.aspx?table=" + table + "&index=" + index + "','Reference'," + "'width=510" + "," + "height=435px');</script>");
			RegisterStartupScript("","<script>window.open('./Popup/View1.aspx?table=" + table + "&index=" + index + "','Reference'," + "'width=818" + "," + "height=610px');</script>");

		}

		

		private void Ultrawebgrid3_DblClick(object sender, Infragistics.WebUI.UltraWebGrid.ClickEventArgs e)
		{
			hdItemNum.Value = ItemSearchControl1.ItemNum ;

			Data_Bind();
		}
	}
}
