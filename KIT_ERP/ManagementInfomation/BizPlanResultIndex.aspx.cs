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
using Infragistics.UltraChart.Resources.Appearance;



namespace KIT_ERP.ManagementInfomation
{
	/// <summary>
	/// BizPlanResultIndex에 대한 요약 설명입니다.
	/// </summary>
	public class BizPlanResultIndex :  QualityInspection.QualityInspection_BaseClass
	{
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification4;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification3;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification2;
		protected Infragistics.WebUI.UltraWebChart.UltraChart UltraChart1;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid DataGrid1;
		protected System.Web.UI.WebControls.Button btnAll;
		protected System.Web.UI.WebControls.Button btn_totPer;
		protected System.Web.UI.WebControls.Button btnP_Cost;
		protected System.Web.UI.WebControls.Button btnS_Cost;
		protected System.Web.UI.WebControls.Button Button2;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected Infragistics.WebUI.WebCombo.WebCombo wcCompanyName;
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
				this.Load_WebCombo_Bind();
				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 경영정보 > 사업계획/실적지표';</script>");
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
			this.btn_totPer.Click += new System.EventHandler(this.btn_totPer_Click);
			this.btnP_Cost.Click += new System.EventHandler(this.btnP_Cost_Click);
			this.btnS_Cost.Click += new System.EventHandler(this.btnS_Cost_Click);
			this.DataGrid1.ColumnMove += new Infragistics.WebUI.UltraWebGrid.ColumnMoveEventHandler(this.DataGrid1_ColumnMove);
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		private void Load_WebCombo_Bind()
		{
			SqlDataAdapter adap = new SqlDataAdapter("PageLoad_GetDataSource" , base.GetConnectionString);
			adap.SelectCommand.CommandType = CommandType.StoredProcedure;

			adap.SelectCommand.Parameters.Add("@ReceiveingOrderCompanyName", SqlDbType.Bit).Value = 1;
			adap.SelectCommand.Parameters.Add("@ItemClassification1", SqlDbType.Bit).Value = 1;
			adap.SelectCommand.Parameters.Add("@ItemClassification2", SqlDbType.Bit).Value = 1;
			adap.SelectCommand.Parameters.Add("@ItemClassification3", SqlDbType.Bit).Value = 1;
			adap.SelectCommand.Parameters.Add("@ItemClassification4", SqlDbType.Bit).Value = 1;
			
			DataSet ds = new DataSet();
			adap.Fill(ds);

			wcCompanyName.DataSource = ds.Tables[0].DefaultView;
			wcCompanyName.DataTextField = "거래처 명";
			
			//wcCompanyName.DataValueField = "사업자등록번호";

			ddlItemClassification1.DataSource = ds.Tables[1].DefaultView;
			ddlItemClassification1.DataTextField = "SmallClassificationName";
			ddlItemClassification1.DataValueField = "SmallClassificationCode";

			ddlItemClassification2.DataSource = ds.Tables[2].DefaultView;
			ddlItemClassification2.DataTextField = "SmallClassificationName";
			ddlItemClassification2.DataValueField = "SmallClassificationCode";

			ddlItemClassification3.DataSource = ds.Tables[3].DefaultView;
			ddlItemClassification3.DataTextField = "SmallClassificationName";
			ddlItemClassification3.DataValueField = "SmallClassificationCode";

			ddlItemClassification4.DataSource = ds.Tables[4].DefaultView;
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
			string strCompanyName="";
			string strBusinessRegistrationNum = "";
			
			if ( wcCompanyName.SelectedIndex != -1 )
			{
				strBusinessRegistrationNum = wcCompanyName.SelectedRow.Cells[2].ToString();
				strCompanyName = wcCompanyName.DisplayValue;
			}
			else
			{
				strCompanyName = wcCompanyName.DisplayValue;
			}

			ManagementInfomation_Search mis = 
				new ManagementInfomation_Search
				( 
					PageNames.사업계획실적지표
					, strItemClassification1
					, strItemClassification2
					, strItemClassification3
					, strItemClassification4
					, strBusinessRegistrationNum
					, strCompanyName 
				);

			DataTable dt_GridSource = mis.GetDataSet().Tables[0];

			if (  dt_GridSource.Rows.Count > 3 )	
			{
				DataGrid1.DataSource = ProcessGridSource( dt_GridSource ) ;
				DataGrid1.DataBind();
				this.SetGridStyle(dt_GridSource.Rows.Count, dt_GridSource.Columns.Count);

				DataTable dt_ChartSource = ProcessChartSource( dt_GridSource );
				UltraChart1.Data.DataSource = dt_ChartSource;
				UltraChart1.Data.DataBind();
			
				// 챠트 데이터 상태관리
				Session["DT1"] = dt_ChartSource;

				// 버튼 활성화
				btnAll.Visible = true;			
				btn_totPer.Visible = true;
				btnP_Cost.Visible = true;
				btnS_Cost.Visible = true;
			}
			else	// 데이터가 없는 경우
			{
				Alert( "기록된 데이터가 없습니다." );
				DataGrid1.DataBind();
			}
		}



		private DataTable  ProcessGridSource ( DataTable dt_GridSource )
		{
			for ( int i = 0 ; i < dt_GridSource.Rows.Count ; i = i + 3 )
			{
				for ( int j = 3 ; j < ( System.DateTime.Today.Month + 4 ) ; j ++ )
				{
					decimal P_Cost = Convert.ToDecimal(dt_GridSource.Rows[i+1][j]);	// 계획금액
					decimal S_Cost = Convert.ToDecimal(dt_GridSource.Rows[i+2][j]);	// 실적금액
	
					if ( S_Cost == 0 )
						dt_GridSource.Rows[i][j] = 0.00m;
					else if ( P_Cost == 0 )
						dt_GridSource.Rows[i][j] = S_Cost;
					else
						dt_GridSource.Rows[i][j] = (decimal)((S_Cost / P_Cost) * 100.00m);
				}
			}

			dt_GridSource.AcceptChanges();
			return dt_GridSource;
		}



		private DataTable ProcessChartSource(DataTable dt_GridSource)
		{
			dt_GridSource.Columns.RemoveAt(0);
			dt_GridSource.Columns.RemoveAt(1);
			dt_GridSource.Columns.RemoveAt(1);

			for ( int i = 3 ; i < dt_GridSource.Rows.Count ; i ++ )
			{
				dt_GridSource.Rows[i].Delete();	
			}
			UltraChart1.Tooltips.FormatString = "<DATA_VALUE:###,###,##>";

			dt_GridSource.AcceptChanges();

			return dt_GridSource;
		}



		// 반드시 Grid 에 바인딩을 하고 스타일을 정의.
		private void SetGridStyle(int rowCount, int colCount)
		{
			//  셀합침 , FontBold
			for ( int i = 0 ; i < rowCount ; i = i + 3 )
			{
				DataGrid1.Rows[i].Cells[0].RowSpan = 3;
				DataGrid1.Rows[i].Cells[0].Style.HorizontalAlign = HorizontalAlign.Center;
				DataGrid1.Rows[i].Cells[0].Style.Font.Bold = true;
				DataGrid1.Rows[i].Style.BackColor = Color.WhiteSmoke;
			}
				
			// 각 컬럼의 Format 형식 또는 Align 정의
			for ( int i = 2 ; i < colCount ; i ++ )
			{
				DataGrid1.Columns[i].Format = "###,###,##0.00";//###,###,#0.00
				DataGrid1.Columns[i].CellStyle.HorizontalAlign = HorizontalAlign.Right;
				DataGrid1.Columns[i].CellStyle.Padding.Right = 7;
			}

			DataGrid1.Rows[0].Style.BackColor = Color.NavajoWhite;
			DataGrid1.Rows[0].Style.Font.Bold = true;
			DataGrid1.Rows[1].Style.BackColor = Color.NavajoWhite;
			DataGrid1.Rows[1].Style.Font.Bold = true;
			DataGrid1.Rows[2].Style.BackColor = Color.NavajoWhite;
			DataGrid1.Rows[2].Style.Font.Bold = true;
			
			DataGrid1.Columns[1].Width = 80;
		}



		// 실적금액
		private void btnS_Cost_Click(object sender, System.EventArgs e)
		{
			DataTable dt_S_Cost = ((DataTable)Session["DT1"]).Copy();
		
			dt_S_Cost.Rows.RemoveAt(0);
			dt_S_Cost.Rows.RemoveAt(0);
			
			UltraChart1.Data.DataSource = dt_S_Cost.DefaultView;
			UltraChart1.Tooltips.FormatString = "<DATA_VALUE:###,###,##.00> 원";
			UltraChart1.Data.DataBind();
			UltraChart1.ChartType = Infragistics.UltraChart.Shared.Styles.ChartType.ColumnChart;
		}


		// 계획금액
		private void btnP_Cost_Click(object sender, System.EventArgs e)
		{
			DataTable dt_P_Cost = ((DataTable)Session["DT1"]).Copy();
		
			dt_P_Cost.Rows.RemoveAt(0);
			dt_P_Cost.Rows.RemoveAt(1);
			
			UltraChart1.Data.DataSource = dt_P_Cost.DefaultView;
			UltraChart1.Tooltips.FormatString = "<DATA_VALUE:###,###,##> 원";
			UltraChart1.Data.DataBind();
			UltraChart1.ChartType = Infragistics.UltraChart.Shared.Styles.ChartType.ColumnChart;
		}


		// 합계율(%)
		private void btn_totPer_Click(object sender, System.EventArgs e)
		{
			DataTable dt_Tp_Cost = ((DataTable)Session["DT1"]).Copy();
		
			dt_Tp_Cost.Rows.RemoveAt(1);
			dt_Tp_Cost.Rows.RemoveAt(1);
			
			UltraChart1.Data.DataSource = dt_Tp_Cost.DefaultView;
			UltraChart1.Data.DataBind();

			UltraChart1.Tooltips.FormatString = "<DATA_VALUE:00.00> %";
			UltraChart1.ChartType = Infragistics.UltraChart.Shared.Styles.ChartType.ColumnChart;
			ChartTextAppearance[] text = new ChartTextAppearance[12];

			for ( int i = 0 ; i < text.Length ; i ++ )
			{
				text[i] = new ChartTextAppearance(
											UltraChart1
											, i
											, 0
											, true
											, new Font( "Arial", 8f), Color.Navy
											, "<DATA_VALUE:00.00> %"
											, StringAlignment.Near
											, StringAlignment.Near
											, 0
										);
				UltraChart1.ColumnChart.ChartText.Add( text[i]);
			}
		}



		// 전체
		private void btnAll_Click(object sender, System.EventArgs e)
		{
			UltraChart1.Data.DataSource = ((DataTable)Session["DT1"]).DefaultView;
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
