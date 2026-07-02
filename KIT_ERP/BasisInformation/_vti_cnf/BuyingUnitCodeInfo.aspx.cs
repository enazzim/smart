using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Configuration;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace KIT_ERP.BasisInformation
{
	/// <summary>
	/// BuyingUnitCostInfo에 대한 요약 설명입니다.
	/// </summary>
	public class BuyingUnitCodeInfo : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lb_ItemNum;
		protected System.Web.UI.WebControls.TextBox tb_ItemNum;
		protected System.Web.UI.WebControls.Label lb_ItemDrawNum;
		protected System.Web.UI.WebControls.TextBox tb_ItemDrawNum;
		protected System.Web.UI.WebControls.Label lb_ItemName;
		protected System.Web.UI.WebControls.TextBox tb_ItemName;
		protected System.Web.UI.WebControls.Label lb_PropertyClassification;
		protected System.Web.UI.WebControls.TextBox tb_PropertyClassification;
		protected System.Web.UI.WebControls.Label lb_Unit;
		protected System.Web.UI.WebControls.TextBox tb_Unit;
		protected System.Web.UI.WebControls.Label lb_Standard;
		protected System.Web.UI.WebControls.TextBox tb_Standard;
		protected System.Web.UI.WebControls.Label lb_Index;
		protected System.Web.UI.WebControls.Button bt_Clear;
		protected System.Web.UI.WebControls.Button bt_Update;
		protected System.Web.UI.WebControls.Button bt_Delete;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid2;
		protected System.Web.UI.WebControls.Label lb_Company;
		protected System.Web.UI.WebControls.Label lb_BeginDate;
		protected System.Web.UI.WebControls.Label lb_EndDate;
		protected System.Web.UI.WebControls.Label lb_StandardUnitCost;
		protected System.Web.UI.WebControls.TextBox tb_StandardUnitCost;
		protected System.Web.UI.WebControls.Label lb_DiscountUnitCost;
		protected System.Web.UI.WebControls.Label lb_OrderRate;
		protected System.Web.UI.WebControls.TextBox tb_OrderRate;
		protected System.Web.UI.WebControls.TextBox tb_DiscountUnitCost;
		protected Infragistics.WebUI.WebCombo.WebCombo wc_Company;
		protected System.Web.UI.WebControls.Label lb_ItemChoice;
		protected System.Web.UI.WebControls.Button bt_BuyingUnitCost;
		protected System.Web.UI.WebControls.Button bt_RelationView;
		protected System.Web.UI.WebControls.Label lb_ItemIndex;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdc_BeginDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdc_EndDate;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		protected Infragistics.WebUI.WebCombo.WebCombo wc_ItemChoice;
		protected System.Web.UI.WebControls.Button bt_Registration;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			bt_Registration.Attributes.Add("onClick", "return OK('입력한 항목을 등록');");
			bt_Update.Attributes.Add("onClick", "return OK('선택한 항목을 수정');");
			bt_Delete.Attributes.Add("onClick", "return OK('선택한 항목을 삭제');");

			if(!Page.IsPostBack)
			{
			
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				conn.Open();
				string str1 = "Select ItemNum as '품목번호', ItemDrawNum as '도면번호', ItemName as '품목명', ItemInfoIndex as '번호' From II_MT Where RecodingState = 1 and (PropertyClassification = '원자재' or PropertyClassification = '상품')";
				SqlCommand comm1= new SqlCommand(str1,conn);
				SqlDataAdapter da1 = new SqlDataAdapter(comm1) ;
				DataSet ds1 = new DataSet() ;
				da1.Fill(ds1);

				wc_ItemChoice.DataSource = ds1;
				wc_ItemChoice.DataTextField = ds1.Tables[0].Columns[2].ToString();
				wc_ItemChoice.DataValueField = ds1.Tables[0].Columns[3].ToString();
				wc_ItemChoice.DataBind();

				string str = "SELECT CompanyName AS 회사명, PresidentName AS 대표자명, BusinessRegistrationNum AS 사업자번호, CompanyInfoIndex AS 번호 FROM CI_MT where RecodingState = 1 and BuyingCompany = 1";
				SqlCommand comm =  new SqlCommand(str,conn);
				SqlDataAdapter da = new SqlDataAdapter(comm) ;
				DataSet ds = new DataSet() ;
				da.Fill(ds);

				wc_Company.DataSource = ds;
				wc_Company.DataTextField = ds.Tables[0].Columns[0].ToString();
				wc_Company.DataValueField = ds.Tables[0].Columns[3].ToString();
				wc_Company.DataBind();
			


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
			this.wc_ItemChoice.SelectedRowChanged += new Infragistics.WebUI.WebCombo.SelectedRowChangedEventHandler(this.wc_ItemChoice_SelectedRowChanged);
			this.LinkButton1.Click += new System.EventHandler(this.LinkButton1_Click);
			this.bt_BuyingUnitCost.Click += new System.EventHandler(this.bt_BuyingUnitCost_Click);
			this.UltraWebGrid1.SelectedRowsChange += new Infragistics.WebUI.UltraWebGrid.SelectedRowsChangeEventHandler(this.UltraWebGrid1_SelectedRowsChange);
			this.bt_RelationView.Click += new System.EventHandler(this.bt_RelationView_Click);
			this.bt_Clear.Click += new System.EventHandler(this.bt_Clear_Click);
			this.bt_Update.Click += new System.EventHandler(this.bt_Update_Click);
			this.bt_Delete.Click += new System.EventHandler(this.bt_Delete_Click);
			this.bt_Registration.Click += new System.EventHandler(this.bt_Registration_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void bt_BuyingUnitCost_Click(object sender, System.EventArgs e)
		{
			if(tb_ItemNum.Text =="")
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('품목을 선택해주세요');");
				Response.Write("</script>");
			}
			else
			{
				this.DataView();
			}
		}
		private void DataView()
		{
			int m_itemindex = int.Parse(lb_ItemIndex.Text);
			string m_table = "UCI_MT";
			string m_page = "BuyingUnitCodeInfo";

			
			View view = new View(m_itemindex, m_table, m_page);
			UltraWebGrid1.DataSource = view.ItemView();
			UltraWebGrid1.DataBind();
			
		}

		private void wc_ItemChoice_SelectedRowChanged(object sender, Infragistics.WebUI.WebCombo.SelectedRowChangedEventArgs e)
		{
			lb_ItemIndex.Text = Convert.ToString(wc_ItemChoice.DataValue);
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select * From II_MT Where (PropertyClassification = '원자재' or PropertyClassification = '상품') and RecodingState = 1 and ItemInfoIndex = @index ";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@index",SqlDbType.Int).Value = lb_ItemIndex.Text;
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				tb_ItemNum.Text = dr["ItemNum"].ToString();
				tb_ItemDrawNum.Text = dr["ItemDrawNum"].ToString();
				tb_ItemName.Text = dr["ItemName"].ToString();
				tb_PropertyClassification.Text = dr["PropertyClassification"].ToString();
				tb_Unit.Text = dr["Unit"].ToString();
				tb_Standard.Text = dr["Standard"].ToString();
			}
			dr.Close();

			
			string str1 = "Select SmallClassificationName from PUC_MT where RecodingState = 1 and SmallClassificationCode = @code" ;
			SqlCommand comm1 = new SqlCommand(str1,conn);
			comm1.Parameters.Add("@code",SqlDbType.VarChar).Value = tb_Unit.Text;
			SqlDataReader dr1 = comm1.ExecuteReader();
			while(dr1.Read())
			{
				tb_Unit.Text = dr1["SmallClassificationName"].ToString();
			}
			conn.Close();
		}

		private void bt_Clear_Click(object sender, System.EventArgs e)
		{

			wc_ItemChoice.DataTextField = "";
			wc_ItemChoice.DataValue = "";
			tb_ItemNum.Text = "";
			tb_ItemDrawNum.Text = "";
			tb_ItemName.Text = "";
			tb_PropertyClassification.Text = "";
			tb_Unit.Text = "";
			tb_Standard.Text = "";

			wc_Company.DataTextField = "";
			wc_Company.DataValue ="";
			tb_OrderRate.Text = "";
			tb_StandardUnitCost.Text = "0";
			tb_DiscountUnitCost.Text = "0";
			wdc_BeginDate.Value = "";
			wdc_EndDate.Value = "";

			lb_Index.Text = "";
			lb_ItemIndex.Text = "";


		}

		private void UltraWebGrid1_SelectedRowsChange(object sender, Infragistics.WebUI.UltraWebGrid.SelectedRowsEventArgs e)
		{
			
		}

		private void bt_Registration_Click(object sender, System.EventArgs e)
		{
			if(tb_ItemNum.Text =="" || wc_Company.DisplayValue =="" || tb_OrderRate.Text =="" || tb_StandardUnitCost.Text == "" || wdc_BeginDate.Text == " ")
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('필수항목을 입력해주세요');");
				Response.Write("</script>");
			}
			else
			{
				if(Page.IsValid == true)
				{
					string m_RequirePage = "PageName.BuyingUnitCodeInfo";
					string m_RequireAction = "Registration";
					string m_TableName = "UCI_MT";
					int m_Idx = (int)PageName.BuyingUnitCodeInfo;
					string m_User = "";//Session[UserName].ToString();
					string m_RecodeIdx = lb_Index.Text;

					ArrayList InputList = new ArrayList();

					InputList.Add(lb_ItemIndex.Text);
					InputList.Add("구매단가");
					InputList.Add(wc_Company.DataValue.ToString());
					InputList.Add("14000000");
					InputList.Add("14000000");
					InputList.Add(tb_OrderRate.Text);
					InputList.Add(tb_StandardUnitCost.Text);
					InputList.Add(tb_DiscountUnitCost.Text);
					InputList.Add(wdc_BeginDate.Text);
					if(wdc_EndDate.Text == "")
						wdc_EndDate.Value = "2076-06-06";
					InputList.Add(wdc_EndDate.Text);

					MasterInfoRecordRUD rud = new MasterInfoRecordRUD(m_RequirePage, m_RequireAction, m_TableName, m_Idx, m_User, m_RecodeIdx, InputList);
					rud.ActivateRUD();
				}
				this.DataView();
			}


		}

		private void bt_Update_Click(object sender, System.EventArgs e)
		{
			if(Page.IsValid == true)
			{
				string m_RequirePage = "PageName.BuyingUnitCodeInfo";
				string m_RequireAction = "Update";
				string m_TableName = "UCI_MT";
				int m_Idx = (int)PageName.BuyingUnitCodeInfo;
				string m_User = "";//Session[UserName].ToString();
				string m_RecodeIdx = lb_Index.Text;
			
			
				ArrayList InputList = new ArrayList();

				InputList.Add(lb_ItemIndex.Text);
				InputList.Add("구매단가");
				InputList.Add(wc_Company.DataValue.ToString());
				InputList.Add("14000000");
				InputList.Add("14000000");
				InputList.Add(tb_OrderRate.Text);
				InputList.Add(tb_StandardUnitCost.Text);
				InputList.Add(tb_DiscountUnitCost.Text);
				InputList.Add(wdc_BeginDate.Text);
				InputList.Add(wdc_EndDate.Text);

				MasterInfoRecordRUD rud = new MasterInfoRecordRUD(m_RequirePage, m_RequireAction, m_TableName, m_Idx, m_User, m_RecodeIdx, InputList);
				rud.ActivateRUD();
			}
			this.DataView();
		}

		private void bt_Delete_Click(object sender, System.EventArgs e)
		{
			if(Page.IsValid == true)
			{
				string m_RequirePage = "PageName.BuyingUnitCodeInfo";
				string m_RequireAction = "Delete";
				string m_TableName = "UCI_MT";
				int m_Idx = (int)PageName.BuyingUnitCodeInfo;
				string m_User = "";//Session[UserName].ToString();
				string m_RecodeIdx = lb_Index.Text;
				
				ArrayList InputList = new ArrayList();

				InputList.Add(lb_ItemIndex.Text);
				InputList.Add("구매단가");
				InputList.Add(wc_Company.DataValue.ToString());
				InputList.Add("14000000");
				InputList.Add("14000000");
				InputList.Add(tb_OrderRate.Text);
				InputList.Add(tb_StandardUnitCost.Text);
				InputList.Add(tb_DiscountUnitCost.Text);
				InputList.Add(wdc_BeginDate.Text);
				InputList.Add(wdc_EndDate.Text);

				MasterInfoRecordRUD rud = new MasterInfoRecordRUD(m_RequirePage, m_RequireAction, m_TableName, m_Idx, m_User, m_RecodeIdx, InputList);
				rud.ActivateRUD();
			}

			wc_Company.DisplayValue = "";
			wc_Company.DataValue = "";
			tb_OrderRate.Text = "";
			tb_StandardUnitCost.Text = "0";
			tb_DiscountUnitCost.Text = "0";
			wdc_BeginDate.Value = "";
			wdc_EndDate.Value = "";

			lb_Index.Text = "";

			this.DataView();
		}

		private void bt_RelationView_Click(object sender, System.EventArgs e)
		{
			if(lb_Index.Text == "")
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('항목을 선택후 버튼을 눌러주세요');");
				Response.Write("</script>");
			}
			else
			{

				string m_TableName = "UCI_MT";
				int m_RecodeIdx = int.Parse(lb_Index.Text);
			

				Reference rf = new Reference(m_TableName, m_RecodeIdx);
				rf.reference();
				UltraWebGrid1.DataSource = rf.reference();
				UltraWebGrid1.DataBind();
			}
		}

		private void LinkButton1_Click(object sender, System.EventArgs e)
		{
			string txt = lb_ItemIndex.Text;
			string txtID = "lb_ItemIndex";
			string formID = "BuyingUnitCodeInfo.aspx" ;
			Response.Write("<script language=javascript>");
			Response.Write("window.open('./Popup/UnitCost.aspx?formID=" + formID + "&txtID=" + txtID + "&txt=" + txt + "','UnitCost'," + "'width=900" + "," + "height=500px');");
			Response.Write("</script>");
		}

		
		

		
	}
}
