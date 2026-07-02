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

namespace KIT_ERP.ManagementInfomation
{
	/// <summary>
	/// SaleIndex에 대한 요약 설명입니다.
	/// </summary>
	public class SaleIndex : QualityInspection.QualityInspection_BaseClass
	{
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification1;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification2;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification3;
		protected Infragistics.WebUI.UltraWebChart.UltraChart UltraChart1;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid DataGrid1;
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification4;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			CSC1.UnitCostDistinction="수주거래처";
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}
			if(!IsPostBack)
			{
				// 페이지가 처음 로드될때 실행
				this.Load_WebCombo_Bind();
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
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.DataGrid1.ColumnMove += new Infragistics.WebUI.UltraWebGrid.ColumnMoveEventHandler(this.DataGrid1_ColumnMove);
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion



		private void Load_WebCombo_Bind()
		{
			SqlDataAdapter adap = new SqlDataAdapter("PageLoad_GetDataSource" , base.GetConnectionString);
			adap.SelectCommand.CommandType = CommandType.StoredProcedure;

			// 수주원장 거래처만 가져옴
			adap.SelectCommand.Parameters.Add("@ItemClassification1", SqlDbType.Bit).Value = 1;
			adap.SelectCommand.Parameters.Add("@ItemClassification2", SqlDbType.Bit).Value = 1;
			adap.SelectCommand.Parameters.Add("@ItemClassification3", SqlDbType.Bit).Value = 1;
			adap.SelectCommand.Parameters.Add("@ItemClassification4", SqlDbType.Bit).Value = 1;
			
			DataSet ds = new DataSet();
			adap.Fill(ds);

			
			ddlItemClassification1.DataSource = ds.Tables[0].DefaultView;
			ddlItemClassification1.DataTextField = "SmallClassificationName";
			ddlItemClassification1.DataValueField = "SmallClassificationCode";

			ddlItemClassification2.DataSource = ds.Tables[1].DefaultView;
			ddlItemClassification2.DataTextField = "SmallClassificationName";
			ddlItemClassification2.DataValueField = "SmallClassificationCode";

			ddlItemClassification3.DataSource = ds.Tables[2].DefaultView;
			ddlItemClassification3.DataTextField = "SmallClassificationName";
			ddlItemClassification3.DataValueField = "SmallClassificationCode";

			ddlItemClassification4.DataSource = ds.Tables[3].DefaultView;
			ddlItemClassification4.DataTextField = "SmallClassificationName";
			ddlItemClassification4.DataValueField = "SmallClassificationCode";

			this.DataBind();
			ListItem item = new ListItem("- 선택하세요 -", "");

			ddlItemClassification1.Items.Insert(0, item);
			ddlItemClassification2.Items.Insert(0, item);
			ddlItemClassification3.Items.Insert(0, item);
			ddlItemClassification4.Items.Insert(0, item);
		}



		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			string strItemClassification1 = ddlItemClassification1.SelectedValue;
			string strItemClassification2 = ddlItemClassification2.SelectedValue;
			string strItemClassification3 = ddlItemClassification3.SelectedValue;
			string strItemClassification4 = ddlItemClassification4.SelectedValue;
			string strCompanyName= CSC1.Company.Trim();
			string strBusinessRegistrationNum = CSC1.BusinessRegistrationNum.Trim();
			

			ManagementInfomation_Search mis = new ManagementInfomation_Search
				( PageNames.매출지표, strItemClassification1, strItemClassification2, strItemClassification3, strItemClassification4
				, strBusinessRegistrationNum, strCompanyName );
				
			DataTable dt_GridSource =  mis.GetDataSet().Tables[0];

			// DataTable의 행이 0 이 아니라면...
			// 거래처명이 등록되지 않은 거래처 이므로 별도의 메세지를 출력한다.
			if ( dt_GridSource.Rows.Count > 1 )	// 데이터가 있는 거래처 이라면 - ( 유효한 데이터 )
			{

				DataGrid1.DataSource = dt_GridSource;
				DataGrid1.DataBind();
				this.SetGridStyle( dt_GridSource.Rows.Count, dt_GridSource.Columns.Count );

				DataView dv_Chartsource = this.ChartDataSource_Create( dt_GridSource ).DefaultView;
				UltraChart1.Data.DataSource = dv_Chartsource ;
				UltraChart1.Tooltips.FormatString = "<DATA_VALUE:###,###,##.00> 원";
				UltraChart1.Data.DataBind();

				Session["DT2"] = dv_Chartsource;
			}
			else
			{
				Alert("입력된 데이터가 없습니다.");
				DataGrid1.DataBind();
			}
		}

		
		
		private void DataGrid1_ColumnMove(object sender, Infragistics.WebUI.UltraWebGrid.ColumnEventArgs e)
		{
			UltraChart1.Data.DataSource = Session["DT2"];
			UltraChart1.Tooltips.FormatString = "<DATA_VALUE:###,###,##.00> 원";
			UltraChart1.Data.DataBind();
		}



		// 챠트 DataSource 만들기.
		private  DataTable  ChartDataSource_Create(DataTable dt_ChartSource)
		{
			for ( int i = 1 ; i < dt_ChartSource.Rows.Count ; i ++ )
			{
				dt_ChartSource.Rows[i].Delete();
			}

			dt_ChartSource.Columns.RemoveAt(0);
			dt_ChartSource.Columns.RemoveAt(0);
			dt_ChartSource.Columns.RemoveAt(0);

			dt_ChartSource.AcceptChanges();

			return dt_ChartSource;
		}



		// Excel 버튼
		private void Button2_Click(object sender, System.EventArgs e)
		{
			if (DataGrid1.Rows.Count > 0)
				UltraWebGridExcelExporter1.Export(DataGrid1);
			else
				Alert("Excel 로 출력될 검색결과가 없습니다.");
		}



		// 반드시 Grid 에 바인딩을 하고 스타일을 정의.
		private void SetGridStyle(int rowCount, int colCount)
		{			
			// 각 컬럼의 Format 형식 또는 Align 정의
			for ( int i = 1 ; i < colCount ; i ++ )
			{
				DataGrid1.Columns[i].Format = "###,###,##0.00";
				DataGrid1.Columns[i].CellStyle.HorizontalAlign = HorizontalAlign.Right;
				DataGrid1.Columns[i].CellStyle.Padding.Right = 7;
			}

			DataGrid1.Rows[0].Style.BackColor = Color.NavajoWhite;
			DataGrid1.Rows[0].Style.Font.Bold = true;
			DataGrid1.Rows[0].Cells[0].Style.HorizontalAlign = HorizontalAlign.Center;

			DataGrid1.Columns[0].Width = 100;
		}


	}
}
