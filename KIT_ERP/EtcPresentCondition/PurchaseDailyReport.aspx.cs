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
using Infragistics.WebUI.UltraWebGrid;

namespace KIT_ERP.EtcPresentCondition
{
	/// <summary>
	/// PurchaseDailyReport에 대한 요약 설명입니다.
	/// </summary>
	public class PurchaseDailyReport : System.Web.UI.Page
	{
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected System.Web.UI.WebControls.Button Button1;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Button btSearch;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcToDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcFromDate;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;
		protected System.Web.UI.WebControls.DropDownList ddlDivision;
		protected System.Web.UI.WebControls.DropDownList ddlItemGroup1;
		protected System.Web.UI.WebControls.DropDownList ddlItemGroup2;
		protected System.Web.UI.WebControls.DropDownList ddlItemGroup3;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcMinDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcMaxDate;
		protected System.Web.UI.WebControls.DropDownList ddlMon;
		protected System.Web.UI.WebControls.DropDownList ddlYear;
		protected System.Web.UI.WebControls.DropDownList ddlRecognition;
		DataSet ds = new DataSet();
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			ItemSearchControl1.RawMaterials = true;
			ItemSearchControl1.HalfFinishedProducts = true;
			ItemSearchControl1.Products = true;
			ItemSearchControl1.Commodity = true;
			CSC1.UnitCostDistinction="구매외주";

			//wdcFromDate.NullDateLabel = DateTime.Now.Year+"-"+DateTime.Now.Month+"-01";
			//wdcToDate.NullDateLabel = DateTime.Now.ToShortDateString();
			//wdcMinDate.NullDateLabel = DateTime.Now.Year+"-"+DateTime.Now.Month+"-01";
			//wdcMaxDate.NullDateLabel = DateTime.Now.ToShortDateString();
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
				
			}
			if(!Page.IsPostBack)
			{
				this.Load_Bind();	
			}
		}

		private void Load_Bind()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			
			//	*************************************************
			//	**  품목분류1 드롭다운리스트... 데이타바인딩... **		
			//	*************************************************
			//코드분류표에서 대분류명이 품목분류1인 소분류명을 가지고 옴.
			string str_ItemClassification1 = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '0210' order by SmallClassificationName" ;
			SqlCommand comm_ItemClassification1 = new SqlCommand(str_ItemClassification1,conn);
			SqlDataAdapter da_ItemClassification1 = new SqlDataAdapter(comm_ItemClassification1) ;
			DataSet ds_ItemClassification1 = new DataSet() ;
			da_ItemClassification1.Fill(ds_ItemClassification1);
				
			ddlItemGroup1.DataSource = ds_ItemClassification1;
			ddlItemGroup1.DataTextField = ds_ItemClassification1.Tables[0].Columns[0].ToString();
			ddlItemGroup1.DataValueField = ds_ItemClassification1.Tables[0].Columns[1].ToString();
			ddlItemGroup1.DataBind();
			ddlItemGroup1.Items.Insert(0, "-선택하세요-") ;
			ddlItemGroup1.Items[0].Value = "";



			


			//	*************************************************
			//	**  품목분류2 드롭다운리스트... 데이타바인딩... **		
			//	*************************************************
			//코드분류표에서 대분류명이 품목분류2인 소분류명을 가지고 옴.
			string str_ItemClassification2 = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '0220' order by SmallClassificationName" ;
			SqlCommand comm_ItemClassification2 = new SqlCommand(str_ItemClassification2,conn);
			SqlDataAdapter da_ItemClassification2 = new SqlDataAdapter(comm_ItemClassification2) ;
			DataSet ds_ItemClassification2 = new DataSet() ;
			da_ItemClassification2.Fill(ds_ItemClassification2);
				
			ddlItemGroup2.DataSource = ds_ItemClassification2;
			ddlItemGroup2.DataTextField = ds_ItemClassification2.Tables[0].Columns[0].ToString();
			ddlItemGroup2.DataValueField = ds_ItemClassification2.Tables[0].Columns[1].ToString();
			ddlItemGroup2.DataBind();
			ddlItemGroup2.Items.Insert(0, "-선택하세요-") ;
			ddlItemGroup2.Items[0].Value = "";



			//	*************************************************
			//	**  품목분류3 드롭다운리스트... 데이타바인딩... **		
			//	*************************************************
			//코드분류표에서 대분류명이 품목분류3인 소분류명을 가지고 옴.
			string str_ItemClassification3 = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '0230' order by SmallClassificationName" ;
			SqlCommand comm_ItemClassification3 = new SqlCommand(str_ItemClassification3,conn);
			SqlDataAdapter da_ItemClassification3 = new SqlDataAdapter(comm_ItemClassification3) ;
			DataSet ds_ItemClassification3 = new DataSet() ;
			da_ItemClassification3.Fill(ds_ItemClassification3);
				
			ddlItemGroup3.DataSource = ds_ItemClassification3;
			ddlItemGroup3.DataTextField = ds_ItemClassification3.Tables[0].Columns[0].ToString();
			ddlItemGroup3.DataValueField = ds_ItemClassification3.Tables[0].Columns[1].ToString();
			ddlItemGroup3.DataBind();
			ddlItemGroup3.Items.Insert(0, "-선택하세요-") ;
			ddlItemGroup3.Items[0].Value = "";
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
			this.btSearch.Click += new System.EventHandler(this.btSearch_Click);
			this.UltraWebGrid1.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.UltraWebGrid1_PageIndexChanged);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btSearch_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = 1;
			UltraWebGrid1.DataSource = Search();
			UltraWebGrid1.DataBind();

		}

		private DataTable Search()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			SqlDataAdapter adap = new SqlDataAdapter("PurchaseDailyReport",conn);
			adap.SelectCommand.Parameters.Add("@ItemNum",ItemSearchControl1.ItemNum.Trim());
			if(ItemSearchControl1.hdItem.Trim() == "")
			{
				adap.SelectCommand.Parameters.Add("@ItemDrawNum",ItemSearchControl1.ItemDrawNum.Trim());
				adap.SelectCommand.Parameters.Add("@ItemName",ItemSearchControl1.ItemName.Trim());
			}
			else
			{
				adap.SelectCommand.Parameters.Add("@ItemDrawNum","");
				adap.SelectCommand.Parameters.Add("@ItemName","");
			}
			adap.SelectCommand.Parameters.Add("@CompanyName",CSC1.Company.Trim());
			adap.SelectCommand.Parameters.Add("@BusinessRegistrationNum",CSC1.BusinessRegistrationNum.Trim());
			adap.SelectCommand.Parameters.Add("@StartDate",wdcFromDate.Text.Trim());
			adap.SelectCommand.Parameters.Add("@EndDate",wdcToDate.Text.Trim());
			adap.SelectCommand.Parameters.Add("@MinDate",wdcMinDate.Text.Trim());
			adap.SelectCommand.Parameters.Add("@MaxDate",wdcMaxDate.Text.Trim());
			adap.SelectCommand.Parameters.Add("@ItemClassification1",ddlItemGroup1.SelectedValue.Trim());
			adap.SelectCommand.Parameters.Add("@ItemClassification2",ddlItemGroup2.SelectedValue.Trim());
			//adap.SelectCommand.Parameters.Add("@ItemClassification3",ddlItemGroup3.SelectedValue.Trim());			
			adap.SelectCommand.Parameters.Add("@Recognition",ddlRecognition.SelectedValue.Trim());			
			adap.SelectCommand.Parameters.Add("@Year",ddlYear.SelectedItem.Value.Trim());
			adap.SelectCommand.Parameters.Add("@Month",ddlMon.SelectedItem.Value.Trim());
			adap.SelectCommand.Parameters.Add("@Division",ddlDivision.SelectedItem.Value.Trim());
			adap.SelectCommand.CommandType = CommandType.StoredProcedure;
			
			adap.Fill(ds);
			
			for(int a=0 ; a< ds.Tables[0].Rows.Count;a++)
			{
				if(ds.Tables[0].Rows[a]["BusinessRegistrationNum"].ToString() == "소계")
				{
					ds.Tables[0].Rows[a]["MonthTotal"] = MonthCost(ds.Tables[0].Rows[a-1]["BusinessRegistrationNum"].ToString());					
					ds.Tables[0].Rows[a]["YearTotal"] =  YearCost(ds.Tables[0].Rows[a-1]["BusinessRegistrationNum"].ToString());
				}
				if(ds.Tables[0].Rows[a]["CompanyName"].ToString() == "합계")
				{
					ds.Tables[0].Rows[a]["MonthTotal"] = TotalMonthCost();
					ds.Tables[0].Rows[a]["YearTotal"] = TotalYearCost();
				}

			}

			return ds.Tables[0];
		}

		private decimal MonthCost(string num)
		{
			decimal monthcost = 0;
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandType = CommandType.StoredProcedure;
			comm.CommandText = "SPMonthCost";
			comm.Parameters.Add("@Num",num);
			comm.Parameters.Add("@Division",ddlDivision.SelectedItem.Value);
			comm.Parameters.Add("@Year",ddlYear.SelectedItem.Value.Trim());
			comm.Parameters.Add("@Month",ddlMon.SelectedItem.Value.Trim());

			conn.Open();
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				if(dr["TotalCost"] == Convert.DBNull)
					monthcost = 0;
				else
					
					monthcost = decimal.Parse(dr["TotalCost"].ToString());
			}
			conn.Close();
			return monthcost;

		}

		private decimal YearCost(string num)
		{
			decimal yearthcost = 0;
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandType = CommandType.StoredProcedure;
			comm.CommandText = "SPYearCost";
			comm.Parameters.Add("@Num",num);
			comm.Parameters.Add("@Year",ddlYear.SelectedItem.Value.Trim());
			comm.Parameters.Add("@Division",ddlDivision.SelectedItem.Value.Trim());

			conn.Open();
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				if(dr["TotalCost"] == Convert.DBNull)
					yearthcost = 0;
				else
					yearthcost = decimal.Parse(dr["TotalCost"].ToString());
			}
			conn.Close();
			return yearthcost;
		}


		private decimal TotalMonthCost()
		{
			decimal monthcost = 0;
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandType = CommandType.StoredProcedure;
			comm.CommandText = "SPTotalCost";
			comm.Parameters.Add("@Year",ddlYear.SelectedItem.Value.Trim());
			comm.Parameters.Add("@Month",ddlMon.SelectedItem.Value.Trim());
			comm.Parameters.Add("@Division",ddlDivision.SelectedItem.Value);
			comm.Parameters.Add("@Division1","1");
			conn.Open();
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				if(dr["TotalCost"] == Convert.DBNull)
					monthcost = 0;
				else
					monthcost = decimal.Parse(dr["TotalCost"].ToString());
			}
			conn.Close();
			return monthcost;
		}

		private decimal TotalYearCost()
		{
			decimal yearthcost = 0;
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandType = CommandType.StoredProcedure;
			comm.CommandText = "SPTotalCost";
			comm.Parameters.Add("@Division",ddlDivision.SelectedItem.Value);
			comm.Parameters.Add("@Division1","0");
			comm.Parameters.Add("@Year",ddlYear.SelectedItem.Value.Trim());
			comm.Parameters.Add("@Month",ddlMon.SelectedItem.Value.Trim());
			conn.Open();
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				if(dr["TotalCost"] == Convert.DBNull)
					yearthcost = 0;
				else
					yearthcost = decimal.Parse(dr["TotalCost"].ToString());
			}
			conn.Close();
			return yearthcost;
		}

		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			UltraWebGrid1.DataSource = Search();
			UltraWebGrid1.DataBind();
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid uwg = new UltraWebGrid();
			uwg = UltraWebGrid1;
			uwg.DisplayLayout.Pager.AllowPaging = false;
			uwg.DataSource = Search();
			uwg.DataBind();
			UltraWebGridExcelExporter1.Export(uwg);
		}
	}
}
