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
using KIT_ERP.QualityInspection;



namespace KIT_ERP.ManagementInfomation
{
	/// <summary>
	/// AutonomyInspectSatistics에 대한 요약 설명입니다.
	/// </summary>
	public class AutonomyInspectSatistics :KIT_ERP.QualityInspection.QualityInspection_BaseClass
	{
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification1;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification2;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification3;
		protected Infragistics.WebUI.UltraWebChart.UltraChart UltraChart1;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification4;
		protected Infragistics.WebUI.WebCombo.WebCombo wcWCName;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}
			if(!IsPostBack)
			{
				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 경영정보 > 자주검사 지표';</script>");
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
			this.UltraWebGrid1.ColumnMove += new Infragistics.WebUI.UltraWebGrid.ColumnMoveEventHandler(this.UltraWebGrid1_ColumnMove);
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Load_WebCombo_Bind()
		{
			SqlDataAdapter adap = new SqlDataAdapter("PageLoad_GetDataSource" , base.GetConnectionString);
			adap.SelectCommand.CommandType = CommandType.StoredProcedure;

			adap.SelectCommand.Parameters.Add("@Wcname", SqlDbType.Bit).Value = 1;
			adap.SelectCommand.Parameters.Add("@ItemClassification1", SqlDbType.Bit).Value = 1;
			adap.SelectCommand.Parameters.Add("@ItemClassification2", SqlDbType.Bit).Value = 1;
			adap.SelectCommand.Parameters.Add("@ItemClassification3", SqlDbType.Bit).Value = 1;
			adap.SelectCommand.Parameters.Add("@ItemClassification4", SqlDbType.Bit).Value = 1;
			
			DataSet ds = new DataSet();
			adap.Fill(ds);
			wcWCName.DataSource = ds.Tables[0].DefaultView;
			wcWCName.DataTextField = "작업장 명";
			wcWCName.DataValueField = "WCInfoIndex";

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

			ListItem item = new ListItem("전체","");

			wcWCName.Columns[0].Hidden = true;

			ddlItemClassification1.Items.Insert(0, item);
			ddlItemClassification2.Items.Insert(0, item);
			ddlItemClassification3.Items.Insert(0, item);
			ddlItemClassification4.Items.Insert(0, item);

			wcWCName.Columns[1].Width = 130;
			wcWCName.Columns[2].Width = 130;
			wcWCName.Columns[3].Width = 70;
		}



		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			string strItemClassification1 = ddlItemClassification1.SelectedValue;
			string strItemClassification2 = ddlItemClassification2.SelectedValue;
			string strItemClassification3 = ddlItemClassification3.SelectedValue;
			string strItemClassification4 = ddlItemClassification4.SelectedValue;
			string strWCName = "";

			if( wcWCName.SelectedIndex != -1)
			{
				strWCName = wcWCName.SelectedRow.Cells[0].ToString();
			}
			else
			{
				strWCName = wcWCName.DisplayValue;
			}

			QualityInspection_Search qis = new QualityInspection_Search
				(
				PageNames.자주검사지표
				, strWCName
				, strItemClassification1
				, strItemClassification2
				, strItemClassification3
				, strItemClassification4 
				);

			DataTable dt_GridSource = qis.GetDataSet().Tables[0];

			// 데이터가 있는 작업장 이라면
			if ( dt_GridSource.Rows.Count > 3 )	
			{
				for ( int i = 0 ; i < dt_GridSource.Rows.Count ; i = i + 3 )
				{
					for ( int j = 3 ; j  < ( System.DateTime.Today.Month + 4 ) ; j ++)				
					{
						decimal de_IncongruityQuantity = (decimal)dt_GridSource.Rows[i+1][j];		// 현재행의 부적합수량
						decimal de_TotalQuantity = (decimal)dt_GridSource.Rows[i+2][j];			// 현재행의 총생산수량

						dt_GridSource.Rows[i][j] = base.CalculatePPM( de_IncongruityQuantity ,  de_TotalQuantity );
					}
				}

				UltraWebGrid1.DataSource = dt_GridSource.DefaultView;
				UltraWebGrid1.DataBind();
				this.SetGridStyle( dt_GridSource.Rows.Count ,  dt_GridSource.Columns.Count );
				
				UltraChart1.Data.DataSource =  this.ProcessChartSource( dt_GridSource );
				UltraChart1.Data.DataBind();
			}
			else	// 데이터가 없는 작업장
			{
				Alert( "검색된 데이타가 없습니다." );				
				UltraWebGrid1.DataBind();
			}
		}



		private DataTable ProcessChartSource( DataTable dt_GridSource )
		{
			DataTable dtChartSource = new DataTable();
			dtChartSource.Columns.Add("1월", typeof(decimal));
			dtChartSource.Columns.Add("2월", typeof(decimal));
			dtChartSource.Columns.Add("3월", typeof(decimal));
			dtChartSource.Columns.Add("4월", typeof(decimal));
			dtChartSource.Columns.Add("5월", typeof(decimal));
			dtChartSource.Columns.Add("6월", typeof(decimal));
			dtChartSource.Columns.Add("7월", typeof(decimal));
			dtChartSource.Columns.Add("8월", typeof(decimal));
			dtChartSource.Columns.Add("9월", typeof(decimal));
			dtChartSource.Columns.Add("10월", typeof(decimal));
			dtChartSource.Columns.Add("11월", typeof(decimal));
			dtChartSource.Columns.Add("12월", typeof(decimal));

			DataRow dRow = dtChartSource.NewRow();

			for ( int i = 0 ; i < 12 ; i ++)
			{
				dRow[i] = dt_GridSource.Rows[0][i+4];
			}
			
			dtChartSource.Rows.Add( dRow );
			Session["DT1"] = dtChartSource.DefaultView;
			
			return dtChartSource;
		}



		// 반드시 Grid 에 바인딩을 하고 스타일을 정의.
		private void SetGridStyle( int rowCount ,  int colCount )
		{
			//  셀합침 , FontBold
			for ( int i = 0 ; i < rowCount ; i = i + 3 )
			{
				UltraWebGrid1.Rows[i].Cells[0].RowSpan = 3;
				UltraWebGrid1.Rows[i].Cells[0].Style.HorizontalAlign = HorizontalAlign.Center;
				UltraWebGrid1.Rows[i].Cells[0].Style.Font.Bold = true;
				UltraWebGrid1.Rows[i].Style.BackColor = Color.WhiteSmoke;
			}
				
			// 각 컬럼의 Format 형식 또는 Align 정의
			for ( int i = 2 ; i < colCount ; i ++ )
			{
				UltraWebGrid1.Columns[i].Format = "###,###,##0.00";
				UltraWebGrid1.Columns[i].CellStyle.HorizontalAlign = HorizontalAlign.Right;
				UltraWebGrid1.Columns[i].CellStyle.Padding.Right = 7;
			}

			UltraWebGrid1.Rows[0].Style.BackColor = Color.NavajoWhite;
			UltraWebGrid1.Rows[0].Style.Font.Bold = true;

			UltraWebGrid1.Rows[1].Style.BackColor = Color.NavajoWhite;
			UltraWebGrid1.Rows[2].Style.BackColor = Color.NavajoWhite;
		}



		private void UltraWebGrid1_ColumnMove(object sender, Infragistics.WebUI.UltraWebGrid.ColumnEventArgs e)
		{
			UltraChart1.Data.DataSource = Session["DT1"];
			UltraChart1.Data.DataBind();
		}



		private void Button2_Click(object sender, System.EventArgs e)
		{
			if ( UltraWebGrid1.Rows.Count > 0 )
				UltraWebGridExcelExporter1.Export( UltraWebGrid1 );
			else
				Alert("Excel 로 출력될 검색결과가 없습니다.");
		}



	}
}
