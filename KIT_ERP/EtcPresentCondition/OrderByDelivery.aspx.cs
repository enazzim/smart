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
	/// OrderByDelivery에 대한 요약 설명입니다.
	/// </summary>
	public class OrderByDelivery : System.Web.UI.Page
	{
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcFromDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcToDate;
		protected System.Web.UI.WebControls.DropDownList ddlDivision;
		protected System.Web.UI.WebControls.Button btItemSearch;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Button Button1;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CompanySearchControl1;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcStartDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcEndDate;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			ItemSearchControl1.Commodity = true;
			ItemSearchControl1.Products = true;
			ItemSearchControl1.RawMaterials = true;
			ItemSearchControl1.Phantom = false;
			ItemSearchControl1.HalfFinishedProducts = true;
			CompanySearchControl1.UnitCostDistinction="";
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
			this.btItemSearch.Click += new System.EventHandler(this.btItemSearch_Click);
			this.UltraWebGrid1.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.UltraWebGrid1_PageIndexChanged);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btItemSearch_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = 1;

			DataTable dt = new DataTable();
			dt = Search();
			if(dt.Rows.Count != 0)
			{
				UltraWebGrid1.DataSource = dt;
				UltraWebGrid1.DataBind();
			}
			else
			{
				DataTable dt1 = new DataTable();
				UltraWebGrid1.DataSource = dt1;
				UltraWebGrid1.DataBind();
				Response.Write("<script language=javascript>");
				Response.Write("alert('검색결과가 없습니다!');");
				Response.Write("</script>");
			}			
		}

		private DataTable Search()
		{

			DataTable dt = new DataTable();

			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string str = "SPOrderByDelivery";

			if(ddlDivision.SelectedItem.Value.Trim() == "구매")
			{
				str = @"Select VO.Model, VO.ItemNum, VO.ItemDrawNum, VO.ItemName, VO.EndProcessCode, VO.EndProcess, VO.CompanyName
					, VO.BusinessRegistrationNum, OrderDate, OrderQuantity, OrderUnitCost, OrderTotalCost, OrderPerson
					, VO.DeliveryRequestDate, VD.DeliveryDate, isnull(VD.DeliveryQuantity,0) as DeliveryQuantity
					, isnull(VD.DeliveryUnitCost,0) as DeliveryUnitCost, isnull(VD.DeliveryTotalCost,0) as DeliveryTotalCost
					, NowStockQuantity, isnull(OrderQuantity-DeliveryQuantity,0) as [QuantityDiffer]
					, isnull( OrderTotalCost - (DeliveryTotalCost) ,0  )as [CostDiffer], isnull(Division,'') as Division
					From ViewOrder as VO left outer join ViewDelivery as VD on VO.BuyingOrderHistoryIndex = HistoryIndex2
					where VO.ItemNum like '%'+@ItemNum+'%' and VO.ItemDrawNum like '%'+@ItemDrawNum+'%'
											and VO.ItemName like '%'+@ItemName+'%' and VO.BusinessRegistrationNum like '%'+@BusinessRegistrationNum+'%'
											and VO.CompanyName like '%'+@CompanyName+'%'
											and (OrderDate BETWEEN @FromDate and @ToDate) 
											and (DeliveryRequestDate BETWEEN @StartDate and @EndDate)
					Order by CompanyName, OrderDate desc,  DeliveryDate desc, DeliveryRequestDate desc, VO.ItemNum";
			}
			else
			{
				str = @"Select VO.Model, VO.ItemNum, VO.ItemDrawNum, VO.ItemName, VO.EndProcessCode, VO.EndProcess
				, VO.CompanyName, VO.BusinessRegistrationNum
				, VO.OrderDate, VO.OrderQuantity, VO.OrderUnitCost, VO.OrderTotalCost, VO.OrderPerson
				, VO.DeliveryRequestDate, VD.DeliveryDate, isnull(VD.DeliveryQuantity,0) as DeliveryQuantity
				, isnull(VD.DeliveryUnitCost,0) as DeliveryUnitCost, isnull(VD.DeliveryTotalCost,0) as DeliveryTotalCost
				, NowStockQuantity, isnull(OrderQuantity -DeliveryQuantity,0) as QuantityDiffer
				, isnull(OrderTotalCost -DeliveryTotalCost,0) as CostDiffer, isnull(Division,'') as Division
				From ViewOutOrder VO left outer join ViewOutDelivery VD on VO.OutSideOrderHistoryIndex = HistoryIndex2				
				where VO.ItemNum like '%'+@ItemNum+'%' and VO.ItemDrawNum like '%'+@ItemDrawNum+'%'
										and VO.ItemName like '%'+@ItemName+'%' and VO.BusinessRegistrationNum like '%'+@BusinessRegistrationNum+'%'
										and VO.CompanyName like '%'+@CompanyName+'%'
										and (OrderDate BETWEEN @FromDate and @ToDate) 
										and (DeliveryRequestDate BETWEEN @StartDate and @EndDate)
				Order by CompanyName, OrderDate desc,  DeliveryDate desc, DeliveryRequestDate desc, VO.ItemNum";
			}

			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = str;
			//comm.CommandType = CommandType.StoredProcedure;
			comm.Parameters.Add("@ItemNum",ItemSearchControl1.ItemNum);
			comm.Parameters.Add("@ItemDrawNum",ItemSearchControl1.ItemDrawNum);
			comm.Parameters.Add("@ItemName",ItemSearchControl1.ItemName);
			comm.Parameters.Add("@hdItemNum",ItemSearchControl1.hdItem);
			comm.Parameters.Add("@CompanyName",CompanySearchControl1.Company);
			comm.Parameters.Add("@BusinessRegistrationNum",CompanySearchControl1.BusinessRegistrationNum);
			comm.Parameters.Add("@Division",ddlDivision.SelectedItem.Value.Trim());
			if(wdcFromDate.Text.Trim() == "")
				comm.Parameters.Add("@FromDate","1990-01-01");
			else
				comm.Parameters.Add("@FromDate",wdcFromDate.Text.Trim());
			if(wdcToDate.Text.Trim() == "")
				comm.Parameters.Add("@ToDate","2049-01-01");
			else
				comm.Parameters.Add("@ToDate",wdcToDate.Text.Trim());
			if(wdcStartDate.Text.Trim() =="")
				comm.Parameters.Add("@StartDate","1990-01-01");
            else
				comm.Parameters.Add("@StartDate",wdcStartDate.Text.Trim());
			if(wdcEndDate.Text.Trim() == "")
				comm.Parameters.Add("@EndDate","2049-01-01");
			else
				comm.Parameters.Add("@EndDate",wdcEndDate.Text.Trim());

			SqlDataAdapter da = new SqlDataAdapter(comm) ;
			DataSet ds = new DataSet() ;
			da.Fill(ds);

			return ds.Tables[0];
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

		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			UltraWebGrid1.DataSource = Search();
			UltraWebGrid1.DataBind();
		}

	}
}
