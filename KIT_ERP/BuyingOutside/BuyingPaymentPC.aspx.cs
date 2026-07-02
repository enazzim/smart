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

using System.Data.SqlClient;
using System.Configuration;
using Infragistics.WebUI.UltraWebGrid;

namespace KIT_ERP.BuyingOutside
{
	/// <summary>
	/// BuyingPaymentPC에 대한 요약 설명입니다.
	/// </summary>
	public class BuyingPaymentPC : System.Web.UI.Page
	{
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected Infragistics.WebUI.WebCombo.WebCombo wcCompany;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcFromDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcToDate;
		protected System.Web.UI.WebControls.Button bt_Search;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Button Button3;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid2;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			ItemSearchControl1.Commodity = true; //상품 바인딩				
			ItemSearchControl1.RawMaterials = true; //원자재 바인딩

			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}
			if(!Page.IsPostBack)
			{
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				
				string str1 = "SELECT CompanyName, PresidentName, BusinessRegistrationNum, CompanyInfoIndex FROM CI_MT where RecodingState = 1 and BuyingCompany = 1 order by CompanyName";
				SqlCommand comm1 =  new SqlCommand(str1,conn);
				SqlDataAdapter da1 = new SqlDataAdapter(comm1) ;
				DataSet ds1 = new DataSet() ;
				da1.Fill(ds1);

				wcCompany.DataSource = ds1;
				wcCompany.DataTextField = ds1.Tables[0].Columns[0].ToString();
				wcCompany.DataValueField = ds1.Tables[0].Columns[2].ToString();
				wcCompany.DataBind();
				
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
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.bt_Search.Click += new System.EventHandler(this.bt_Search_Click);
			this.Button3.Click += new System.EventHandler(this.Button3_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		private void bt_Search_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.Visible = true;
			UltraWebGrid2.Visible = false;
			UltraWebGrid1.DataSource = Search();

			UltraWebGrid1.Columns[4].Format = "###,###,###";
			UltraWebGrid1.Columns[5].Format = "###,###,###";
			UltraWebGrid1.Columns[6].Format = @"\ ###,###,##0";



			UltraWebGrid1.DataBind();		

		}

		private DataTable Search()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			SqlDataAdapter adap = new SqlDataAdapter("SPBuyingPaymentPC",conn);
			adap.SelectCommand.Parameters.Add("@ItemNum",ItemSearchControl1.ItemNum);
			if(ItemSearchControl1.hdItem.Trim() == "")
			{
				adap.SelectCommand.Parameters.Add("@ItemDrawNum",ItemSearchControl1.ItemDrawNum);
				adap.SelectCommand.Parameters.Add("@ItemName",ItemSearchControl1.ItemName);
			}
			else
			{
				adap.SelectCommand.Parameters.Add("@ItemDrawNum","");
				adap.SelectCommand.Parameters.Add("@ItemName","");
			}
			if(wcCompany.DataValue == null)
				adap.SelectCommand.Parameters.Add("@BusinessRegistrationNum","");
			else
				adap.SelectCommand.Parameters.Add("@BusinessRegistrationNum",wcCompany.DataValue.ToString());
			adap.SelectCommand.Parameters.Add("@StartDate",wdcFromDate.Text);
			adap.SelectCommand.Parameters.Add("@EndDate",wdcToDate.Text);
			adap.SelectCommand.CommandType = CommandType.StoredProcedure;
			
			DataSet ds = new DataSet();
			
			adap.Fill(ds);			


			DataTable dt = new DataTable();
			DataColumn dc = new DataColumn("CompanyName", typeof(string));
			dt.Columns.Add(dc);
			dc = new DataColumn("BusinessRegistrationNum", typeof(string));
			dt.Columns.Add(dc);
			dc = new DataColumn("ItemNum", typeof(string));
			dt.Columns.Add(dc);
			dc = new DataColumn("ItemName", typeof(string));
			dt.Columns.Add(dc);
			dc = new DataColumn("PaymentQuantity", typeof(Decimal));
			dt.Columns.Add(dc);
			dc = new DataColumn("UnitCost", typeof(Decimal));
			dt.Columns.Add(dc);
			dc = new DataColumn("TotalPaymentCost", typeof(Decimal));
			dt.Columns.Add(dc);
			
			decimal PaymentQuantity = 0;
			decimal TotalPaymentCost = 0;

			decimal PaymentQuantity1 = PaymentQuantity;
			decimal TotalPaymentCost1 = TotalPaymentCost;


			for(int i = 0 ; i < ds.Tables[0].Rows.Count; i++)
			{
				
				if(i == (ds.Tables[0].Rows.Count-1))
				{
					PaymentQuantity1 += decimal.Parse(ds.Tables[0].Rows[i]["PaymentQuantity"].ToString());
					TotalPaymentCost1 += decimal.Parse(ds.Tables[0].Rows[i]["TotalPaymentCost"].ToString());

					DataRow dr = dt.NewRow();
					dr["CompanyName"] = ds.Tables[0].Rows[i]["CompanyName"].ToString();
					dr["BusinessRegistrationNum"] = ds.Tables[0].Rows[i]["BusinessRegistrationNum"].ToString();
					dr["ItemNum"] = ds.Tables[0].Rows[i]["ItemNum"].ToString();
					dr["ItemName"] = ds.Tables[0].Rows[i]["ItemName"].ToString();
					dr["PaymentQuantity"] = decimal.Parse(ds.Tables[0].Rows[i]["PaymentQuantity"].ToString());
					dr["UnitCost"] = UnitCost(ds.Tables[0].Rows[i]["ItemNum"].ToString(),ds.Tables[0].Rows[i]["BusinessRegistrationNum"].ToString());
					dr["TotalPaymentCost"] = decimal.Parse(ds.Tables[0].Rows[i]["TotalPaymentCost"].ToString());
				
					dt.Rows.Add(dr);

					DataRow dr1 = dt.NewRow();
					dr1["CompanyName"] = Convert.DBNull;
					dr1["BusinessRegistrationNum"] = Convert.DBNull;
					dr1["ItemNum"] = "소계";
					dr1["ItemName"] = Convert.DBNull;
					dr1["PaymentQuantity"] = PaymentQuantity1;
					dr1["UnitCost"] = Convert.DBNull;
					dr1["TotalPaymentCost"] = TotalPaymentCost1;
				
					dt.Rows.Add(dr1);




					PaymentQuantity += decimal.Parse(ds.Tables[0].Rows[i]["PaymentQuantity"].ToString());
					TotalPaymentCost += decimal.Parse(ds.Tables[0].Rows[i]["TotalPaymentCost"].ToString());
					



					DataRow dr2 = dt.NewRow();
					dr2["CompanyName"] = "합계";
					dr2["BusinessRegistrationNum"] = "";
					dr2["ItemNum"] = "";
					dr2["ItemName"] = "";
					dr2["PaymentQuantity"] = PaymentQuantity;
					dr2["UnitCost"] = Convert.DBNull;
					dr2["TotalPaymentCost"] = TotalPaymentCost;
				
					dt.Rows.Add(dr2);
				}
				else
				{
					if(ds.Tables[0].Rows[i]["BusinessRegistrationNum"].ToString() == ds.Tables[0].Rows[i+1]["BusinessRegistrationNum"].ToString())
					{
						
						PaymentQuantity1 += decimal.Parse(ds.Tables[0].Rows[i]["PaymentQuantity"].ToString());
						TotalPaymentCost1 += decimal.Parse(ds.Tables[0].Rows[i]["TotalPaymentCost"].ToString());

						DataRow dr = dt.NewRow();
						dr["CompanyName"] = ds.Tables[0].Rows[i]["CompanyName"].ToString();
						dr["BusinessRegistrationNum"] = ds.Tables[0].Rows[i]["BusinessRegistrationNum"].ToString();
						dr["ItemNum"] = ds.Tables[0].Rows[i]["ItemNum"].ToString();
						dr["ItemName"] = ds.Tables[0].Rows[i]["ItemName"].ToString();
						dr["PaymentQuantity"] = decimal.Parse(ds.Tables[0].Rows[i]["PaymentQuantity"].ToString());
						dr["UnitCost"] = UnitCost(ds.Tables[0].Rows[i]["ItemNum"].ToString(),ds.Tables[0].Rows[i]["BusinessRegistrationNum"].ToString());
						dr["TotalPaymentCost"] = decimal.Parse(ds.Tables[0].Rows[i]["TotalPaymentCost"].ToString());
				
						dt.Rows.Add(dr);

						PaymentQuantity += decimal.Parse(ds.Tables[0].Rows[i]["PaymentQuantity"].ToString());
						TotalPaymentCost += decimal.Parse(ds.Tables[0].Rows[i]["TotalPaymentCost"].ToString());
					}
					else
					{
						PaymentQuantity1 += decimal.Parse(ds.Tables[0].Rows[i]["PaymentQuantity"].ToString());
						TotalPaymentCost1 += decimal.Parse(ds.Tables[0].Rows[i]["TotalPaymentCost"].ToString());

						DataRow dr = dt.NewRow();
						dr["CompanyName"] = ds.Tables[0].Rows[i]["CompanyName"].ToString();
						dr["BusinessRegistrationNum"] = ds.Tables[0].Rows[i]["BusinessRegistrationNum"].ToString();
						dr["ItemNum"] = ds.Tables[0].Rows[i]["ItemNum"].ToString();
						dr["ItemName"] = ds.Tables[0].Rows[i]["ItemName"].ToString();
						dr["PaymentQuantity"] = decimal.Parse(ds.Tables[0].Rows[i]["PaymentQuantity"].ToString());
						dr["UnitCost"] = UnitCost(ds.Tables[0].Rows[i]["ItemNum"].ToString(),ds.Tables[0].Rows[i]["BusinessRegistrationNum"].ToString());
						dr["TotalPaymentCost"] = decimal.Parse(ds.Tables[0].Rows[i]["TotalPaymentCost"].ToString());
				
						dt.Rows.Add(dr);

						PaymentQuantity += decimal.Parse(ds.Tables[0].Rows[i]["PaymentQuantity"].ToString());
						TotalPaymentCost += decimal.Parse(ds.Tables[0].Rows[i]["TotalPaymentCost"].ToString());

						DataRow dr1 = dt.NewRow();
						dr1["CompanyName"] = Convert.DBNull;
						dr1["BusinessRegistrationNum"] = Convert.DBNull;
						dr1["ItemNum"] = "소계";
						dr1["ItemName"] = Convert.DBNull;
						dr1["PaymentQuantity"] = PaymentQuantity1;
						dr1["UnitCost"] = Convert.DBNull;
						dr1["TotalPaymentCost"] = TotalPaymentCost1;
				
						dt.Rows.Add(dr1);

						PaymentQuantity1 = 0;
						TotalPaymentCost1 = 0;
					}
					
				}
			}
			return dt;
		}

		private decimal UnitCost(string ItemNum,string Num)
		{
			decimal cost = 0;
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			string str = @"Select StandardUnitCost From UCI_MT where ItemNum = @Item and BusinessRegistrationNum = @num
						and RecodingState = 1 and UnitCostDistinction ='구매단가' ";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@Item",ItemNum);
			comm.Parameters.Add("@num",Num);
			conn.Open();
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				cost = decimal.Parse(dr["StandardUnitCost"].ToString());
			}
			conn.Close();
			
			return cost;

		}

		

		private void Button3_Click(object sender, System.EventArgs e)
		{
			if(UltraWebGrid1.Visible == true)
			{
				UltraWebGridExcelExporter1.Export(UltraWebGrid1);
			}
			else
			{
				UltraWebGridExcelExporter1.Export(UltraWebGrid2);
			}
		}

		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			UltraWebGrid1.DataSource = Search();
			UltraWebGrid1.DataBind();
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.Visible = false;
			UltraWebGrid2.Visible = true;
			UltraWebGrid2.DataSource = Search1();
			UltraWebGrid2.DataBind();
		}

		private DataTable Search1()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			SqlDataAdapter adap = new SqlDataAdapter("SPBuyingItemPaymentPC",conn);
			adap.SelectCommand.Parameters.Add("@ItemNum",ItemSearchControl1.ItemNum);
			adap.SelectCommand.Parameters.Add("@ItemDrawNum",ItemSearchControl1.ItemDrawNum);
			adap.SelectCommand.Parameters.Add("@ItemName",ItemSearchControl1.ItemName);
			if(wcCompany.DataValue == null)
				adap.SelectCommand.Parameters.Add("@BusinessRegistrationNum","");
			else
				adap.SelectCommand.Parameters.Add("@BusinessRegistrationNum",wcCompany.DataValue.ToString());
			adap.SelectCommand.Parameters.Add("@StartDate",wdcFromDate.Text);
			adap.SelectCommand.Parameters.Add("@EndDate",wdcToDate.Text);
			adap.SelectCommand.CommandType = CommandType.StoredProcedure;
			
			DataSet ds = new DataSet();
			
			adap.Fill(ds);			


			DataTable dt = new DataTable();
			DataColumn dc = new DataColumn("CompanyName", typeof(string));
			dt.Columns.Add(dc);
			dc = new DataColumn("BusinessRegistrationNum", typeof(string));
			dt.Columns.Add(dc);
			dc = new DataColumn("ItemNum", typeof(string));
			dt.Columns.Add(dc);
			dc = new DataColumn("ItemName", typeof(string));
			dt.Columns.Add(dc);
			dc = new DataColumn("PaymentQuantity", typeof(Decimal));
			dt.Columns.Add(dc);
			dc = new DataColumn("UnitCost", typeof(Decimal));
			dt.Columns.Add(dc);
			dc = new DataColumn("TotalPaymentCost", typeof(Decimal));
			dt.Columns.Add(dc);
			
			decimal PaymentQuantity = 0;
			decimal TotalPaymentCost = 0;

			decimal PaymentQuantity1 = PaymentQuantity;
			decimal TotalPaymentCost1 = TotalPaymentCost;


			for(int i = 0 ; i < ds.Tables[0].Rows.Count; i++)
			{
				
				if(i == (ds.Tables[0].Rows.Count-1))
				{
					PaymentQuantity1 += decimal.Parse(ds.Tables[0].Rows[i]["PaymentQuantity"].ToString());
					TotalPaymentCost1 += decimal.Parse(ds.Tables[0].Rows[i]["TotalPaymentCost"].ToString());

					DataRow dr = dt.NewRow();
					dr["CompanyName"] = ds.Tables[0].Rows[i]["CompanyName"].ToString();
					dr["BusinessRegistrationNum"] = ds.Tables[0].Rows[i]["BusinessRegistrationNum"].ToString();
					dr["ItemNum"] = ds.Tables[0].Rows[i]["ItemNum"].ToString();
					dr["ItemName"] = ds.Tables[0].Rows[i]["ItemName"].ToString();
					dr["PaymentQuantity"] = decimal.Parse(ds.Tables[0].Rows[i]["PaymentQuantity"].ToString());
					dr["UnitCost"] = UnitCost(ds.Tables[0].Rows[i]["ItemNum"].ToString(),ds.Tables[0].Rows[i]["BusinessRegistrationNum"].ToString());
					dr["TotalPaymentCost"] = decimal.Parse(ds.Tables[0].Rows[i]["TotalPaymentCost"].ToString());
				
					dt.Rows.Add(dr);

					DataRow dr1 = dt.NewRow();
					dr1["CompanyName"] = "소계";
					dr1["BusinessRegistrationNum"] = Convert.DBNull;
					dr1["ItemNum"] = Convert.DBNull;
					dr1["ItemName"] = Convert.DBNull;
					dr1["PaymentQuantity"] = PaymentQuantity1;
					dr1["UnitCost"] = Convert.DBNull;
					dr1["TotalPaymentCost"] = TotalPaymentCost1;
				
					dt.Rows.Add(dr1);




					PaymentQuantity += decimal.Parse(ds.Tables[0].Rows[i]["PaymentQuantity"].ToString());
					TotalPaymentCost += decimal.Parse(ds.Tables[0].Rows[i]["TotalPaymentCost"].ToString());
					



					DataRow dr2 = dt.NewRow();
					dr2["CompanyName"] = "";
					dr2["BusinessRegistrationNum"] = "";
					dr2["ItemNum"] = "합계";
					dr2["ItemName"] = "";
					dr2["PaymentQuantity"] = PaymentQuantity;
					dr2["UnitCost"] = Convert.DBNull;
					dr2["TotalPaymentCost"] = TotalPaymentCost;
				
					dt.Rows.Add(dr2);
				}
				else
				{
					if(ds.Tables[0].Rows[i]["ItemNum"].ToString() == ds.Tables[0].Rows[i+1]["ItemNum"].ToString())
					{
						
						PaymentQuantity1 += decimal.Parse(ds.Tables[0].Rows[i]["PaymentQuantity"].ToString());
						TotalPaymentCost1 += decimal.Parse(ds.Tables[0].Rows[i]["TotalPaymentCost"].ToString());

						DataRow dr = dt.NewRow();
						dr["CompanyName"] = ds.Tables[0].Rows[i]["CompanyName"].ToString();
						dr["BusinessRegistrationNum"] = ds.Tables[0].Rows[i]["BusinessRegistrationNum"].ToString();
						dr["ItemNum"] = ds.Tables[0].Rows[i]["ItemNum"].ToString();
						dr["ItemName"] = ds.Tables[0].Rows[i]["ItemName"].ToString();
						dr["PaymentQuantity"] = decimal.Parse(ds.Tables[0].Rows[i]["PaymentQuantity"].ToString());
						dr["UnitCost"] = UnitCost(ds.Tables[0].Rows[i]["ItemNum"].ToString(),ds.Tables[0].Rows[i]["BusinessRegistrationNum"].ToString());
						dr["TotalPaymentCost"] = decimal.Parse(ds.Tables[0].Rows[i]["TotalPaymentCost"].ToString());
				
						dt.Rows.Add(dr);

						PaymentQuantity += decimal.Parse(ds.Tables[0].Rows[i]["PaymentQuantity"].ToString());
						TotalPaymentCost += decimal.Parse(ds.Tables[0].Rows[i]["TotalPaymentCost"].ToString());
					}
					else
					{
						PaymentQuantity1 += decimal.Parse(ds.Tables[0].Rows[i]["PaymentQuantity"].ToString());
						TotalPaymentCost1 += decimal.Parse(ds.Tables[0].Rows[i]["TotalPaymentCost"].ToString());

						DataRow dr = dt.NewRow();
						dr["CompanyName"] = ds.Tables[0].Rows[i]["CompanyName"].ToString();
						dr["BusinessRegistrationNum"] = ds.Tables[0].Rows[i]["BusinessRegistrationNum"].ToString();
						dr["ItemNum"] = ds.Tables[0].Rows[i]["ItemNum"].ToString();
						dr["ItemName"] = ds.Tables[0].Rows[i]["ItemName"].ToString();
						dr["PaymentQuantity"] = decimal.Parse(ds.Tables[0].Rows[i]["PaymentQuantity"].ToString());
						dr["UnitCost"] = UnitCost(ds.Tables[0].Rows[i]["ItemNum"].ToString(),ds.Tables[0].Rows[i]["BusinessRegistrationNum"].ToString());
						dr["TotalPaymentCost"] = decimal.Parse(ds.Tables[0].Rows[i]["TotalPaymentCost"].ToString());
				
						dt.Rows.Add(dr);

						PaymentQuantity += decimal.Parse(ds.Tables[0].Rows[i]["PaymentQuantity"].ToString());
						TotalPaymentCost += decimal.Parse(ds.Tables[0].Rows[i]["TotalPaymentCost"].ToString());

						DataRow dr1 = dt.NewRow();
						dr1["CompanyName"] = "소계";
						dr1["BusinessRegistrationNum"] = Convert.DBNull;
						dr1["ItemNum"] = Convert.DBNull;
						dr1["ItemName"] = Convert.DBNull;
						dr1["PaymentQuantity"] = PaymentQuantity1;
						dr1["UnitCost"] = Convert.DBNull;
						dr1["TotalPaymentCost"] = TotalPaymentCost1;
				
						dt.Rows.Add(dr1);

						PaymentQuantity1 = 0;
						TotalPaymentCost1 = 0;
					}
					
				}
			}
			return dt;
		}
	}
}
