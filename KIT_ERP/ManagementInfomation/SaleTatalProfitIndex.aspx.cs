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
	/// SaleTatalProfitIndex에 대한 요약 설명입니다.
	/// </summary>
	public class SaleTatalProfitIndex : QualityInspection.QualityInspection_BaseClass
	{
		protected System.Web.UI.WebControls.Button btnS_Cost;
		protected System.Web.UI.WebControls.Button btnAll;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification3;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification2;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid DataGrid1;
		protected Infragistics.WebUI.UltraWebChart.UltraChart UltraChart1;
		protected System.Web.UI.WebControls.Button btn_totProfit;
		protected System.Web.UI.WebControls.Button btnB_Cost;
		protected System.Web.UI.WebControls.Button Button2;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification4;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}
			if(!IsPostBack)
			{
				// 페이지가 처음 로드될때 실행
				this.Load_Bind();
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
			this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
			this.btn_totProfit.Click += new System.EventHandler(this.btn_totProfit_Click);
			this.btnB_Cost.Click += new System.EventHandler(this.btnB_Cost_Click);
			this.btnS_Cost.Click += new System.EventHandler(this.btnS_Cost_Click);
			this.DataGrid1.ColumnMove += new Infragistics.WebUI.UltraWebGrid.ColumnMoveEventHandler(this.DataGrid1_ColumnMove);
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion




		private void Load_Bind()
		{
			SqlDataAdapter adap = new SqlDataAdapter("PageLoad_GetDataSource" , base.GetConnectionString);
			adap.SelectCommand.CommandType = CommandType.StoredProcedure;
			
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
			ManagementInfomation_Search mis = new ManagementInfomation_Search(PageNames.매출총이익지표, ddlItemClassification1.SelectedValue,
				ddlItemClassification2.SelectedValue, ddlItemClassification3.SelectedValue, ddlItemClassification4.SelectedValue);
			
			DataTable dt_GridSource = mis.GetDataSet().Tables[0];

			for ( int i = 3 ; i < System.DateTime.Now.Month + 4 ; i ++)
			{
				dt_GridSource.Rows[0][i] = (decimal)dt_GridSource.Rows[5][i] - (decimal)dt_GridSource.Rows[1][i];
			}

			DataGrid1.DataSource = dt_GridSource.DefaultView;
			DataGrid1.DataBind();

			this.SetGridStyle(dt_GridSource.Rows.Count, dt_GridSource.Columns.Count);

			dt_GridSource.Columns.RemoveAt(0);
			dt_GridSource.Columns.RemoveAt(1);
			dt_GridSource.Columns.RemoveAt(1);	

			dt_GridSource.Rows.RemoveAt(2);
			dt_GridSource.Rows.RemoveAt(2);
			dt_GridSource.Rows.RemoveAt(2);
			dt_GridSource.Rows.RemoveAt(3);
			dt_GridSource.Rows.RemoveAt(3);

			dt_GridSource.Rows[1][0] = "매입누계";
			dt_GridSource.Rows[2][0] = "매출누계";

			UltraChart1.Data.DataSource = dt_GridSource.DefaultView;
			UltraChart1.Tooltips.FormatString = "<DATA_VALUE:###,###,##.00> 원";
			UltraChart1.Data.DataBind();

			Session["DT2"] = dt_GridSource;

			btnAll.Visible = true;			// 버튼 활성화
			btn_totProfit.Visible = true;
			btnB_Cost.Visible = true;
			btnS_Cost.Visible = true;
		}



		// 반드시 Grid 에 바인딩을 하고 스타일을 정의.
		private void SetGridStyle(int rowCount, int colCount)
		{
			//  셀합침 , FontBold
			//  총이익
			DataGrid1.Rows[0].Cells[0].Style.HorizontalAlign = HorizontalAlign.Center;
			DataGrid1.Rows[0].Style.Font.Bold = true;
			DataGrid1.Rows[0].Style.BackColor = Color.NavajoWhite;

			// 매입
			DataGrid1.Rows[1].Cells[0].RowSpan = 4;
			DataGrid1.Rows[1].Cells[0].Style.HorizontalAlign = HorizontalAlign.Center;
			DataGrid1.Rows[1].Cells[0].Style.Font.Bold = true;
			DataGrid1.Rows[1].Style.BackColor = Color.WhiteSmoke;

			// 매출
			DataGrid1.Rows[5].Cells[0].RowSpan = 3;
			DataGrid1.Rows[5].Cells[0].Style.HorizontalAlign = HorizontalAlign.Center;
			DataGrid1.Rows[5].Cells[0].Style.Font.Bold = true;
			DataGrid1.Rows[5].Style.BackColor = Color.WhiteSmoke;
				
			// 각 컬럼의 Format 형식 또는 Align 정의
			for ( int i = 2 ; i < colCount ; i ++ )
			{
				DataGrid1.Columns[i].Format = "###,###,##0.00";
				DataGrid1.Columns[i].CellStyle.HorizontalAlign = HorizontalAlign.Right;
				DataGrid1.Columns[i].CellStyle.Padding.Right = 7;
			}

			DataGrid1.Columns[0].Width = 80;
			DataGrid1.Columns[1].Width = 80;
		}




		private void btnAll_Click(object sender, System.EventArgs e)
		{
			ManagementInfomation_Search mis = new ManagementInfomation_Search(PageNames.매출총이익지표, ddlItemClassification1.SelectedValue,
				ddlItemClassification2.SelectedValue, ddlItemClassification3.SelectedValue, ddlItemClassification4.SelectedValue);
			
			DataTable dt_GridSource = mis.GetDataSet().Tables[0];

			for ( int i = 3 ; i < System.DateTime.Now.Month + 4 ; i ++)
			{
				dt_GridSource.Rows[0][i] = (decimal)dt_GridSource.Rows[5][i] - (decimal)dt_GridSource.Rows[1][i];
			}

			for ( int i = 3 ; i < System.DateTime.Now.Month + 4 ; i ++)
			{
				dt_GridSource.Rows[0][i] = (decimal)dt_GridSource.Rows[5][i] - (decimal)dt_GridSource.Rows[1][i];
			}

			DataGrid1.DataSource = dt_GridSource.DefaultView;
			DataGrid1.DataBind();

			this.SetGridStyle(dt_GridSource.Rows.Count, dt_GridSource.Columns.Count);

			dt_GridSource.Columns.RemoveAt(0);
			dt_GridSource.Columns.RemoveAt(1);
			dt_GridSource.Columns.RemoveAt(1);	

			dt_GridSource.Rows.RemoveAt(2);
			dt_GridSource.Rows.RemoveAt(2);
			dt_GridSource.Rows.RemoveAt(2);
			dt_GridSource.Rows.RemoveAt(3);
			dt_GridSource.Rows.RemoveAt(3);

			dt_GridSource.Rows[1][0] = "매입누계";
			dt_GridSource.Rows[2][0] = "매출누계";

			UltraChart1.Data.DataSource = dt_GridSource.DefaultView;
			UltraChart1.Tooltips.FormatString = "<DATA_VALUE:###,###,##.00> 원";
			UltraChart1.Data.DataBind();
		}



		private void btn_totProfit_Click(object sender, System.EventArgs e)
		{
			ManagementInfomation_Search mis = new ManagementInfomation_Search(PageNames.매출총이익지표, ddlItemClassification1.SelectedValue,
				ddlItemClassification2.SelectedValue, ddlItemClassification3.SelectedValue, ddlItemClassification4.SelectedValue);
			
			DataTable dt_GridSource = mis.GetDataSet().Tables[0];

			for ( int i = 3 ; i < System.DateTime.Now.Month + 4 ; i ++)
			{
				dt_GridSource.Rows[0][i] = (decimal)dt_GridSource.Rows[5][i] - (decimal)dt_GridSource.Rows[1][i];
			}

			for ( int i = 3 ; i < System.DateTime.Now.Month + 4 ; i ++)
			{
				dt_GridSource.Rows[0][i] = (decimal)dt_GridSource.Rows[5][i] - (decimal)dt_GridSource.Rows[1][i];
			}

			DataGrid1.DataSource = dt_GridSource.DefaultView;
			DataGrid1.DataBind();

			this.SetGridStyle(dt_GridSource.Rows.Count, dt_GridSource.Columns.Count);

			dt_GridSource.Columns.RemoveAt(0);
			dt_GridSource.Columns.RemoveAt(1);
			dt_GridSource.Columns.RemoveAt(1);	

			dt_GridSource.Rows.RemoveAt(2);
			dt_GridSource.Rows.RemoveAt(2);
			dt_GridSource.Rows.RemoveAt(2);
			dt_GridSource.Rows.RemoveAt(3);
			dt_GridSource.Rows.RemoveAt(3);

			dt_GridSource.Rows[1][0] = "매입누계";
			dt_GridSource.Rows[2][0] = "매출누계";

			dt_GridSource.Rows.RemoveAt(1);
			dt_GridSource.Rows.RemoveAt(1);

			UltraChart1.Data.DataSource = dt_GridSource.DefaultView;
			UltraChart1.Tooltips.FormatString = "<DATA_VALUE:###,###,##.00> 원";
			UltraChart1.Data.DataBind();
		}



		private void btnB_Cost_Click(object sender, System.EventArgs e)
		{
			ManagementInfomation_Search mis = new ManagementInfomation_Search(PageNames.매출총이익지표, ddlItemClassification1.SelectedValue,
				ddlItemClassification2.SelectedValue, ddlItemClassification3.SelectedValue, ddlItemClassification4.SelectedValue);
			
			DataTable dt_GridSource = mis.GetDataSet().Tables[0];

			for ( int i = 3 ; i < System.DateTime.Now.Month + 4 ; i ++)
			{
				dt_GridSource.Rows[0][i] = (decimal)dt_GridSource.Rows[5][i] - (decimal)dt_GridSource.Rows[1][i];
			}

			for ( int i = 3 ; i < System.DateTime.Now.Month + 4 ; i ++)
			{
				dt_GridSource.Rows[0][i] = (decimal)dt_GridSource.Rows[5][i] - (decimal)dt_GridSource.Rows[1][i];
			}

			DataGrid1.DataSource = dt_GridSource.DefaultView;
			DataGrid1.DataBind();

			this.SetGridStyle(dt_GridSource.Rows.Count, dt_GridSource.Columns.Count);

			dt_GridSource.Columns.RemoveAt(0);
			dt_GridSource.Columns.RemoveAt(1);
			dt_GridSource.Columns.RemoveAt(1);	

			dt_GridSource.Rows.RemoveAt(2);
			dt_GridSource.Rows.RemoveAt(2);
			dt_GridSource.Rows.RemoveAt(2);
			dt_GridSource.Rows.RemoveAt(3);
			dt_GridSource.Rows.RemoveAt(3);

			dt_GridSource.Rows[1][0] = "매입누계";
			dt_GridSource.Rows[2][0] = "매출누계";


			dt_GridSource.Rows.RemoveAt(0);
			dt_GridSource.Rows.RemoveAt(1);

			UltraChart1.Data.DataSource = dt_GridSource.DefaultView;
			UltraChart1.Tooltips.FormatString = "<DATA_VALUE:###,###,##.00> 원";
			UltraChart1.Data.DataBind();
		}



		private void btnS_Cost_Click(object sender, System.EventArgs e)
		{
			ManagementInfomation_Search mis = new ManagementInfomation_Search(PageNames.매출총이익지표, ddlItemClassification1.SelectedValue,
				ddlItemClassification2.SelectedValue, ddlItemClassification3.SelectedValue, ddlItemClassification4.SelectedValue);
			
			DataTable dt_GridSource = mis.GetDataSet().Tables[0];

			for ( int i = 3 ; i < System.DateTime.Now.Month + 4 ; i ++)
			{
				dt_GridSource.Rows[0][i] = (decimal)dt_GridSource.Rows[5][i] - (decimal)dt_GridSource.Rows[1][i];
			}

			for ( int i = 3 ; i < System.DateTime.Now.Month + 4 ; i ++)
			{
				dt_GridSource.Rows[0][i] = (decimal)dt_GridSource.Rows[5][i] - (decimal)dt_GridSource.Rows[1][i];
			}

			DataGrid1.DataSource = dt_GridSource.DefaultView;
			DataGrid1.DataBind();

			this.SetGridStyle(dt_GridSource.Rows.Count, dt_GridSource.Columns.Count);

			dt_GridSource.Columns.RemoveAt(0);
			dt_GridSource.Columns.RemoveAt(1);
			dt_GridSource.Columns.RemoveAt(1);	

			dt_GridSource.Rows.RemoveAt(2);
			dt_GridSource.Rows.RemoveAt(2);
			dt_GridSource.Rows.RemoveAt(2);
			dt_GridSource.Rows.RemoveAt(3);
			dt_GridSource.Rows.RemoveAt(3);

			dt_GridSource.Rows[1][0] = "매입누계";
			dt_GridSource.Rows[2][0] = "매출누계";

			dt_GridSource.Rows.RemoveAt(0);
			dt_GridSource.Rows.RemoveAt(0);

			UltraChart1.Data.DataSource = dt_GridSource.DefaultView;
			UltraChart1.Tooltips.FormatString = "<DATA_VALUE:###,###,##.00> 원";
			UltraChart1.Data.DataBind();
		}



		private void DataGrid1_ColumnMove(object sender, Infragistics.WebUI.UltraWebGrid.ColumnEventArgs e)
		{
			this.btnAll_Click(sender, null);
		}



		// Excel 버튼
		private void Button2_Click(object sender, System.EventArgs e)
		{
			if (DataGrid1.Rows.Count > 0)
				UltraWebGridExcelExporter1.Export(DataGrid1);
			else
				Alert("Excel 로 출력될 검색결과가 없습니다.");
		}









	}
}
