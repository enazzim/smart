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
using Infragistics.WebUI.WebCombo;
using Infragistics.WebUI.UltraWebGrid;

namespace KIT_ERP.BusinessManagement
{
	/// <summary>
	/// ReceiveingProgressLook에 대한 요약 설명입니다.
	/// </summary>
	public class ReceiveingProgressLook : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label searchTitle;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid2;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdc_FormDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdc_ToDate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_FromDate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_ItemNum;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Index;
		protected System.Web.UI.WebControls.LinkButton lnk_Update;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_RowIndex;
		protected System.Web.UI.WebControls.LinkButton lnk_Search;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Index;
		protected System.Web.UI.WebControls.Label Label2;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid3;
		protected Infragistics.WebUI.WebCombo.WebCombo wc_ItemName;
		protected Infragistics.WebUI.WebCombo.WebCombo wc_Company;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected System.Web.UI.WebControls.Button bt_Clear;
		protected System.Web.UI.WebControls.Button bt_Look;

		private int index;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			ItemSearchControl1.Commodity = true; //상품 바인딩
			ItemSearchControl1.Phantom = true; //팬텀 바인딩
			ItemSearchControl1.Products = true; //제품 바인딩	

			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}
			if(!Page.IsPostBack)
			{
				

				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 영업관리 > 수주진행보기';</script>");

				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				conn.Open();

				string str = "SELECT CompanyName, PresidentName, BusinessRegistrationNum, CompanyInfoIndex FROM CI_MT where RecodingState = 1 and ReceiveingOrderCompany = 1 order by CompanyName";
				SqlCommand comm = new SqlCommand(str,conn);
				SqlDataAdapter da = new SqlDataAdapter(comm) ;
				DataSet ds = new DataSet() ;
				da.Fill(ds);

				wc_Company.DataSource = ds;
				wc_Company.DataTextField = ds.Tables[0].Columns[0].ToString();
				wc_Company.DataValueField = ds.Tables[0].Columns[0].ToString();
				wc_Company.DataBind();

				string str1 = "Select ItemNum , ItemDrawNum , ItemName, ItemInfoIndex From II_MT Where RecodingState = 1 and (PropertyClassification = '제품' or PropertyClassification = '팬텀' or PropertyClassification = '상품')";
				SqlCommand comm1 = new SqlCommand(str1,conn);
				SqlDataAdapter da1 = new SqlDataAdapter(comm1) ;
				DataSet ds1 = new DataSet() ;
				da1.Fill(ds1);

				wc_ItemName.DataSource = ds1;
				wc_ItemName.DataTextField = ds1.Tables[0].Columns[2].ToString();
				wc_ItemName.DataValueField = ds1.Tables[0].Columns[2].ToString();
				wc_ItemName.DataBind();

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
			this.bt_Clear.Click += new System.EventHandler(this.bt_Clear_Click);
			this.bt_Look.Click += new System.EventHandler(this.bt_Look_Click);
			this.lnk_Search.Click += new System.EventHandler(this.lnk_Search_Click);
			this.lnk_Update.Click += new System.EventHandler(this.LinkButton1_Click);
			this.UltraWebGrid1.InitializeLayout += new Infragistics.WebUI.UltraWebGrid.InitializeLayoutEventHandler(this.UltraWebGrid1_InitializeLayout);
			this.UltraWebGrid2.DataBinding += new System.EventHandler(this.UltraWebGrid2_DataBinding);
			this.UltraWebGrid3.DataBinding += new System.EventHandler(this.UltraWebGrid3_DataBinding);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		/// <summary>
		/// 검색버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Look_Click(object sender, System.EventArgs e)
		{

			string Item;//품목명
			string Com;//거래처명
			string From;//시작일
			string To;//종료일

			if(ItemSearchControl1.ItemName == null)
				Item ="";
			else
				Item = ItemSearchControl1.ItemName.ToString().Trim();

			if(wc_Company.DataValue == null)
				Com ="";
			else
				Com = Convert.ToString(wc_Company.DataValue);

			if(wdc_FormDate.Value == null || wdc_FormDate.Value.ToString().Trim() == "" )
				From ="1900-01-01";
			else
				From = Convert.ToString(wdc_FormDate.Value).Substring(0,10).Trim();

			if(wdc_ToDate.Value == null || wdc_ToDate.Value.ToString().Trim() == "" )
				To ="2076-06-06";
			else
				To = Convert.ToString(wdc_ToDate.Value).Substring(0,10).Trim();


			Response.Write("<script language=javascript>");
			Response.Write("window.open('./Popup/Search.aspx?Item="+Item+"&Com="+Com+"&From="+From+"&To="+To+"','ReceiveingProgressLook'," + "'width=900" + "," + "height=500px');");
			Response.Write("</script>");
		}

		/// <summary>
		/// 팝업창에서 수주원장 인덱스를 가지고 생산의뢰, 생산계획, 자재소요, 원자재구매, WC작업계획, 납품까지 확인할수 있도록 한다.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LinkButton1_Click(object sender, System.EventArgs e)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string str  ="Select * From RO_HT where ReceivingOrderHistoryIndex = @index";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(lb_Index.Value);
			SqlDataReader dr = comm.ExecuteReader();

			UltraWebGrid1.DataSource = dr;
			UltraWebGrid1.DataBind();
			conn.Close();
		}

		private void lnk_Search_Click(object sender, System.EventArgs e)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			index = int.Parse(UltraWebGrid1.Rows[0].Cells.FromKey("ReceivingOrderHistoryIndex").Value.ToString());

			//수주품목중 제품,상품구분을 해서 바인딩시킨다
			string Classification="";
			string str_Classification = "Select PropertyClassification From RO_HT Where ReceivingOrderHistoryIndex = @index";
			SqlCommand comm_Classification = new SqlCommand(str_Classification,conn);
			comm_Classification.Parameters.Add("@index",SqlDbType.Int).Value = index;
			SqlDataReader dr_Classification = comm_Classification.ExecuteReader();
			while(dr_Classification.Read())
			{
				Classification = dr_Classification["PropertyClassification"].ToString();
			}
			dr_Classification.Close();

			if(Classification.ToString() == "제품")
			{
				UltraWebGrid2.Visible = true;
				UltraWebGrid3.Visible = false;

				UltraWebGrid2.DataBind();
			}
			else
			{
				UltraWebGrid2.Visible = false;
				UltraWebGrid3.Visible = true;

				UltraWebGrid3.DataBind();
			}
			conn.Close();
		}

		private void UltraWebGrid2_DataBinding(object sender, System.EventArgs e)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			DataSet ds = new DataSet();
			SqlDataAdapter da = new SqlDataAdapter("Select ItemNum, ItemDrawNum, ItemName, CompanyName,PropertyClassification, case ProductionRequestDivision when 1 then '예' else '아니오' end as ProductionRequestDivision, ReceivingOrderDate, ApplyUnitCost, DeliveryRequestQuantity1, DeliveryRequestDate1, DeliveryRequestQuantity2, DeliveryRequestDate2, DeliveryRequestQuantity3, DeliveryRequestDate3, DeliveryRequestQuantity4, DeliveryRequestDate4, DeliveryRequestQuantity5, DeliveryRequestDate5, TotalReceiveingOrderQuantity, TotalCost, OutStorehouseQuantity, SuitabilityQuantity, UnInspectionQuantity, RemainderQuantity, OrderNum, DeliveryPlace, VolumNum, ProgressCondition, RegistrationPerson, RegistrationPersonID, RegistrationDate, UpdatingPerson, UpdatingPersonID, UpdatingDate, ReceivingOrderHistoryIndex From RO_HT Where ReceivingOrderHistoryIndex = @index "+
				" Select * From PR_HT where ReceivingOrderHistoryIndex = @index "+
				" Select ProductionPlanHistoryIndex, ItemNum, ItemDrawNum, ItemName, ProductionPlanHistorySourceCode, ProductionPlanHistorySource, ProductionPlanQuantity, ProductionBeginDate, case RowMaterialCalculation when 1 then '산출' else '미산출' end as RowMaterialCalculation, VolumNum, MaterialRequirementVolumNum, ProgressCondition, RegistrationPerson, RegistrationPersonID, RegistrationDate, UpdatingPerson, UpdatingPersonID, UpdatingDate, HistoryIndex, HistorySection  From PP_HT where HistoryIndex = @index1 and HistorySection = '생산의뢰'"+
				" Select * From WDR_HT where ProductionPlanHistoryIndex = @index2 "+
				" Select * From BR_HT where HistoryIndex = @index and HistorySection = '수주' ",conn);
			
			
			//수주원장번호

			da.SelectCommand.Parameters.Add("@index",SqlDbType.Int).Value = index;
			
			//생산계획원장에서 생산의뢰원장번호를 통해 생산계획원장번호 찾기위한 것
			int PR_Index=0;	
			int PP_Index=0;
			string str = "Select * From PR_HT where ReceivingOrderHistoryIndex = @index";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@index",SqlDbType.Int).Value = index;
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				PR_Index = int.Parse(dr["ProductionRequestHistoryIndex"].ToString());
			}
			dr.Close();
			da.SelectCommand.Parameters.Add("@index1",SqlDbType.Int).Value = PR_Index;

			//			PP_Index = int.Parse(dr["ProductionPlanHistoryIndex"].ToString());
		
			//원자재구매의뢰원장에서 생산계획원장번호를 찾기위한것
			string str1 = "Select * From PP_HT where HistoryIndex = @index and HistorySection = '생산의뢰' ";
			SqlCommand comm1 = new SqlCommand(str1,conn);
			comm1.Parameters.Add("@index",SqlDbType.Int).Value = PR_Index;
			SqlDataReader dr1 = comm1.ExecuteReader();
			while(dr1.Read())
			{
				PP_Index = int.Parse(dr1["ProductionPlanHistoryIndex"].ToString());
			}
			dr1.Close();
				
			da.SelectCommand.Parameters.Add("@index2",SqlDbType.Int).Value = PP_Index;

			da.Fill(ds);
	

			ds.Relations.Add(ds.Tables[0].Columns["ItemNum"],
				ds.Tables[1].Columns["ItemNum"]);
			ds.Relations.Add(ds.Tables[1].Columns["ItemNum"],
				ds.Tables[2].Columns["ItemNum"]);
			ds.Relations.Add(ds.Tables[2].Columns["ProductionPlanHistoryIndex"],
				ds.Tables[3].Columns["ProductionPlanHistoryIndex"]);
			ds.Relations.Add(ds.Tables[2].Columns["ProductionPlanHistoryIndex"],
				ds.Tables[4].Columns["HistoryIndex"]);
			
			this.UltraWebGrid2.DataSource = ds.Tables[0].DefaultView;

			
			
			conn.Close();
		}

		private void UltraWebGrid3_DataBinding(object sender, System.EventArgs e)
		{

			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			DataSet ds = new DataSet();

			SqlDataAdapter da = new SqlDataAdapter("Select ItemNum, ItemDrawNum, ItemName, CompanyName,PropertyClassification, case ProductionRequestDivision when 1 then '예' else '아니오' end as ProductionRequestDivision, ReceivingOrderDate, ApplyUnitCost, DeliveryRequestQuantity1, DeliveryRequestDate1, DeliveryRequestQuantity2, DeliveryRequestDate2, DeliveryRequestQuantity3, DeliveryRequestDate3, DeliveryRequestQuantity4, DeliveryRequestDate4, DeliveryRequestQuantity5, DeliveryRequestDate5, TotalReceiveingOrderQuantity, TotalCost, OutStorehouseQuantity, SuitabilityQuantity, UnInspectionQuantity, RemainderQuantity, OrderNum, DeliveryPlace, VolumNum, ProgressCondition, RegistrationPerson, RegistrationPersonID, RegistrationDate, UpdatingPerson, UpdatingPersonID, UpdatingDate, ReceivingOrderHistoryIndex From RO_HT Where ReceivingOrderHistoryIndex = @index "+
				" Select * From BR_HT where HistoryIndex = @index and HistorySection = '수주' ",conn);
			//수주원장번호
			da.SelectCommand.Parameters.Add("@index",SqlDbType.Int).Value = index;
				
			da.Fill(ds);

			ds.Relations.Add(ds.Tables[0].Columns["ItemNum"],
				ds.Tables[1].Columns["ItemNum"]);

			this.UltraWebGrid3.DataSource = ds.Tables[0].DefaultView;

			conn.Close();
		
		}

		private void UltraWebGrid1_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
		{
		
		}

		private void bt_Clear_Click(object sender, System.EventArgs e)
		{
			wc_ItemName.DataValue = "";
			wc_Company.DataValue = "";
			wdc_FormDate.Value = "";
			wdc_ToDate.Value = "";
		}

	}
}
