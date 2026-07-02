using System;
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
using Infragistics.WebUI.UltraWebGrid;
using System.Text;
namespace KIT_ERP.EtcPresentCondition
{
	/// <summary>
	/// QualityInspectionPC에 대한 요약 설명입니다.
	/// </summary>
	public class QualityInspectionPC : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DropDownList ddlProcessDiv;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser txtStartDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser txtEndDate;
		protected System.Web.UI.WebControls.DropDownList ddlDivision;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid DataGrid1;
		protected System.Web.UI.WebControls.Literal Literal1;
		protected System.Web.UI.WebControls.Literal Literal2;
		protected System.Web.UI.WebControls.Literal Literal3;
		protected System.Web.UI.WebControls.Literal Literal4;
		protected System.Web.UI.WebControls.Literal Literal5;
		protected System.Web.UI.WebControls.Button btExcel;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl  ItemSearchControl1;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			ItemSearchControl1.Commodity = true;
			ItemSearchControl1.Products = true;
			ItemSearchControl1.HalfFinishedProducts = true;
			ItemSearchControl1.RawMaterials = true;
			CSC1.UnitCostDistinction ="구매외주";

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
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.btExcel.Click += new System.EventHandler(this.btExcel_Click);
			this.DataGrid1.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.DataGrid1_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			DataGrid1.DisplayLayout.Pager.CurrentPageIndex = 1;
			SByte sbyteProcessDiv = Convert.ToSByte(ddlProcessDiv.SelectedItem.Value);
			
			// 검색 객체 생성
			KIT_ERP.QualityInspection.QualityInspection_Search qis = new KIT_ERP.QualityInspection.QualityInspection_Search
				(
				PageNames.품질검사현황
				, ItemSearchControl1.ItemNum
				, ItemSearchControl1.ItemDrawNum
				, ItemSearchControl1.ItemName
				, txtStartDate.Text
				, txtEndDate.Text
				, CSC1.Company
				, CSC1.BusinessRegistrationNum
				, sbyteProcessDiv
				, ddlDivision.SelectedItem.Value
				);

			DataTable dt_Source = qis.GetDataSet().Tables[0];
			
			// 그리드 하단 통계 출력 메소드 호출
			this.SUM_and_AVG_(dt_Source);
			
			DataGrid1.DataSource = dt_Source.DefaultView;
			DataGrid1.DataKeyField = "번호";
			DataGrid1.DataBind();

			Session["DT1"] = dt_Source;
		}

		// 그리드 하단 레코드 통계 계산하여 출력하는 메소드
		private void SUM_and_AVG_(DataTable dt)
		{
			decimal dE_totalRequestQuantity = 0;
			decimal dE_totalSuitabilityQuantity = 0;
			decimal dE_totalUnSuitabilityQuantity = 0;
			decimal dE_TotalPPM = 0;

			foreach ( DataRow drow in dt.Rows )
			{
				dE_totalRequestQuantity += Convert.ToDecimal(drow[8]);
				dE_totalSuitabilityQuantity +=  Convert.ToDecimal(drow[20]);
				dE_totalUnSuitabilityQuantity +=  Convert.ToDecimal(drow[22]);
			}

			if ( dE_totalRequestQuantity != 0 )
			{
				dE_TotalPPM = ( dE_totalUnSuitabilityQuantity / dE_totalRequestQuantity ) * 1000000;

				Literal1.Text = "총 <b>\" " + dt.Rows.Count.ToString() + " \"</b> 건" ;
				Literal2.Text = "의뢰수량 : <b>" + dE_totalRequestQuantity.ToString().Substring(0, dE_totalRequestQuantity.ToString().IndexOf(".") + 2) + "</b>";
				Literal3.Text = "적합수량 : <b>" + dE_totalSuitabilityQuantity.ToString().Substring(0, dE_totalSuitabilityQuantity.ToString().IndexOf(".") + 2) + "</b>";
				Literal4.Text = "부적합수량 : <b>" + dE_totalUnSuitabilityQuantity.ToString().Substring(0, dE_totalUnSuitabilityQuantity.ToString().IndexOf(".") + 2 ) + "</b>";

			
				if (dE_TotalPPM > 0)
				{
					try
					{
						Literal5.Text = "<b>" + dE_TotalPPM.ToString().Substring( 0, (dE_TotalPPM.ToString()).IndexOf(".") + 3 ) + "</b> PPM";
					}
					catch
					{
						Literal5.Text = "<b>" + dE_TotalPPM + "</b> PPM";	
					}
				}
				else 
					Literal5.Text = "<b>0</b> PPM";
			}
		}

		private void DataGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			DataGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			DataGrid1.DataSource = ((DataTable)Session["DT1"]).DefaultView;
			DataGrid1.DataBind();
		}

		private void btExcel_Click(object sender, System.EventArgs e)
		{
			SByte sbyteProcessDiv = Convert.ToSByte(ddlProcessDiv.SelectedItem.Value);
			
			// 검색 객체 생성
			KIT_ERP.QualityInspection.QualityInspection_Search qis = new KIT_ERP.QualityInspection.QualityInspection_Search
				(
				PageNames.품질검사현황
				, ItemSearchControl1.ItemNum
				, ItemSearchControl1.ItemDrawNum
				, ItemSearchControl1.ItemName
				, txtStartDate.Text
				, txtEndDate.Text
				, CSC1.Company
				, CSC1.BusinessRegistrationNum
				, sbyteProcessDiv
				, ddlDivision.SelectedItem.Value
				);

			DataTable dt_Source = qis.GetDataSet().Tables[0];

			UltraWebGrid uwg_QI = new UltraWebGrid();
			uwg_QI = DataGrid1;
			uwg_QI.DisplayLayout.Pager.AllowPaging = false;
			uwg_QI.DataSource = dt_Source;
			uwg_QI.DataSource = dt_Source.DefaultView;
			uwg_QI.DataKeyField = "번호";
			uwg_QI.DataBind();
			UltraWebGridExcelExporter1.Export(uwg_QI);
		
		}
	}
}
