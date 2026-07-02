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
	/// GoodsBuyingRequest에 대한 요약 설명입니다.
	/// </summary>
	public class GoodsBuyingRequest : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label searchTitle;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Button bt_Register;
		protected System.Web.UI.WebControls.Label Label2;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcBeginDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcEndDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcFromDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcToDate;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.WebControls.Button bt_Search;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			ItemSearchControl1.Commodity = true; //상품 바인딩
			CSC1.UnitCostDistinction = "수주거래처"; 

			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}
			if(!Page.IsPostBack)
			{
				
				
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
			this.bt_Search.Click += new System.EventHandler(this.bt_Search_Click);
			this.UltraWebGrid1.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.UltraWebGrid1_PageIndexChanged);
			this.bt_Register.Click += new System.EventHandler(this.bt_Register_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		

		private void bt_Register_Click(object sender, System.EventArgs e)
		{
			if(UltraWebGrid1.Rows.Count == 0)
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('선택된 항목이 없습니다!');");
				Response.Write("</script>");
			}
			else
			{
				Register reg = new Register(UltraWebGrid1,"GoodsBuyingRequest",Session["ID"].ToString());
				reg.MainRegistration();
			}

			//Search aa = new Search("GoodsBuyingRequest",ItemSearchControl1.ItemNum, ItemSearchControl1.ItemDrawNum,ItemSearchControl1.ItemName, wdcBeginDate,wdcEndDate,CSC1.Company, CSC1.BusinessRegistrationNum);
			//UltraWebGrid1.DataSource = aa.DataSet_search();
			UltraWebGrid1.DataSource = Databind();
			UltraWebGrid1.DataBind();

		}

		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;

			//Search aa = new Search("GoodsBuyingRequest",ItemSearchControl1.ItemNum, ItemSearchControl1.ItemDrawNum,ItemSearchControl1.ItemName, wdcBeginDate,wdcEndDate,CSC1.Company, CSC1.BusinessRegistrationNum);
			//UltraWebGrid1.DataSource = aa.DataSet_search();
			UltraWebGrid1.DataSource = Databind();
			UltraWebGrid1.DataBind();
		}

		private void bt_Search_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = 1;
			
			UltraWebGrid1.DataSource = Databind();
			UltraWebGrid1.DataBind();
		}


		private DataTable Databind()
		{
			DataSet ds = new DataSet();
			Search aa;
			if(ItemSearchControl1.hdItem.Trim() =="")
				aa = new Search("GoodsBuyingRequest",ItemSearchControl1.ItemNum, ItemSearchControl1.ItemDrawNum, ItemSearchControl1.ItemName, wdcBeginDate,wdcEndDate,wdcFromDate, wdcToDate, CSC1.Company, CSC1.BusinessRegistrationNum);
			else
				aa = new Search("GoodsBuyingRequest",ItemSearchControl1.ItemNum, "", "", wdcBeginDate,wdcEndDate,wdcFromDate, wdcToDate, CSC1.Company, CSC1.BusinessRegistrationNum);
			ds = aa.DataSet_search();
			
			DataTable dt = new DataTable();
			DataColumn dc = new DataColumn("ItemNum", typeof(string));//품목번호
			dt.Columns.Add(dc);
			dc = new DataColumn("ItemDrawNum", typeof(string));//도면번호
			dt.Columns.Add(dc);
			dc = new DataColumn("ItemName", typeof(string));//품목명
			dt.Columns.Add(dc);
			dc = new DataColumn("PropertyClassification", typeof(string));//자산분류
			dt.Columns.Add(dc);
			dc = new DataColumn("CompanyName", typeof(string));//거래처
			dt.Columns.Add(dc);			
			dc = new DataColumn("ReceivingOrderDate", typeof(string));//수주일자
			dt.Columns.Add(dc);
			dc = new DataColumn("DeliveryRequestDate1", typeof(string));//납기요구일
			dt.Columns.Add(dc);
			dc = new DataColumn("TotalReceiveingOrderQuantity", typeof(double));//수주량
			dt.Columns.Add(dc);
			dc = new DataColumn("RemainQuantity", typeof(double));//가용재고
			dt.Columns.Add(dc);
			dc = new DataColumn("RequestQuantity", typeof(double));//의뢰량
			dt.Columns.Add(dc);
			//ReceivingOrderHistoryIndex
			dc = new DataColumn("ReceivingOrderHistoryIndex", typeof(int));//수주번호
			dt.Columns.Add(dc);

			foreach(DataRow dr in ds.Tables[0].Rows)
			{
				DataRow drow = dt.NewRow();
				drow["ItemNum"] = dr["ItemNum"].ToString();
				drow["ItemDrawNum"] = dr["ItemDrawNum"].ToString();
				drow["ItemName"] = dr["ItemName"].ToString();
				drow["PropertyClassification"] = dr["PropertyClassification"].ToString();
				drow["CompanyName"] = dr["CompanyName"].ToString();
				drow["ReceivingOrderDate"] = DateTime.Parse(dr["ReceivingOrderDate"].ToString()).ToShortDateString();
				drow["DeliveryRequestDate1"] = DateTime.Parse(dr["DeliveryRequestDate1"].ToString()).ToShortDateString();
				drow["TotalReceiveingOrderQuantity"] = decimal.Parse(dr["TotalReceiveingOrderQuantity"].ToString());
				drow["RemainQuantity"] = RemainQuantity(dr["ItemNum"].ToString().Trim(), decimal.Parse(dr["TotalReceiveingOrderQuantity"].ToString()));

				decimal Quantity = decimal.Parse(drow["TotalReceiveingOrderQuantity"].ToString().Trim()) - decimal.Parse(drow["RemainQuantity"].ToString().Trim());
				if(Quantity > 0)
					drow["RequestQuantity"] = Quantity;
				else
					drow["RequestQuantity"] = 0;

				drow["ReceivingOrderHistoryIndex"] = int.Parse(dr["ReceivingOrderHistoryIndex"].ToString());

				dt.Rows.Add(drow);
			}
			

			return dt;
		}


		private decimal RemainQuantity(string ItemNum, decimal Quantity)
		{
			//총재고
			string str = @"Select
							isnull(Sum(PS_MT.StockQuantity12),0) as RemainQuantity 
							From PS_MT inner join BS_MT on PS_MT.ItemNum = BS_MT.ItemNum
							Where PS_MT.ItemNum = @ItemNum and PS_MT.RecodingState =1 and PS_MT.ProcessCode = '14009999' and PS_MT.Year = @year and BS_MT.Year = @year";
			decimal RemainQuantity = 0;
			decimal total = 0;
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			SqlCommand comm = new SqlCommand();
			comm.CommandText = str;
			comm.Connection = conn;
			conn.Open();
			comm.Parameters.Add("@ItemNum",ItemNum);
			comm.Parameters.Add("@year",DateTime.Now.Year);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				total += decimal.Parse(dr["RemainQuantity"].ToString());
			}
			dr.Close();
			comm.Parameters.Clear();


			str = @"Select isnull(Sum(BS_MT.StockQuantity12),0) as RemainQuantity From BS_MT Where BS_MT.ItemNum = @ItemNum and BS_MT.RecodingState= 1 and [Year] = @year";
			comm.CommandText = str;
			comm.Parameters.Add("@ItemNum",ItemNum);
			comm.Parameters.Add("@year",DateTime.Now.Year);
			SqlDataReader dr1 = comm.ExecuteReader();
			while(dr1.Read())
			{
				total += decimal.Parse(dr1["RemainQuantity"].ToString());
			}
			dr1.Close();
			
			comm.Parameters.Clear();


			str = @"Select RemainderQuantity From RO_HT Where ItemNum = @ItemNum and (ProgressCondition = '대기' or ProgressCondition = '진행')";
			comm.CommandText = str;
			comm.Parameters.Add("@ItemNum",ItemNum);
			SqlDataReader dr2 = comm.ExecuteReader();
			while(dr2.Read())
			{
				RemainQuantity = decimal.Parse(dr2["RemainderQuantity"].ToString()) - Quantity;
			}
			dr2.Close();
			conn.Close();

			comm.Parameters.Clear();


			return (total - RemainQuantity);
		}
	}
}
