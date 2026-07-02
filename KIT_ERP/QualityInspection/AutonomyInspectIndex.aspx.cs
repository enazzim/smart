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

namespace KIT_ERP.QualityInspection
{
	/// <summary>
	/// AutonomyInspectIndex에 대한 요약 설명입니다.
	/// </summary>
	public class AutonomyInspectIndex : QualityInspection_BaseClass
	{
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification4;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification3;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification2;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected Infragistics.WebUI.UltraWebChart.UltraChart UltraChart1;
		protected System.Web.UI.WebControls.Button Button2;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected Infragistics.WebUI.WebCombo.WebCombo wcWCName;
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
				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 품질관리 > 자주검사 지표';</script>");
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

			ListItem item = new ListItem("전체", "");

			wcWCName.Columns[0].Hidden = true;

			ddlItemClassification1.Items.Insert(0, item);
			ddlItemClassification2.Items.Insert(0, item);
			ddlItemClassification3.Items.Insert(0, item);
			ddlItemClassification4.Items.Insert(0, item);

			wcWCName.Columns[1].Width = 150;
			wcWCName.Columns[2].Width = 130;
			wcWCName.Columns[3].Width = 80;
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
			DataSet ds = qis.GetDataSet();

			// DataTable의 [0]째 Row의 [0]째 컬럼이 DBNull 이면 
			// 작업일보가 등록되지 않은 작업장 이므로 별도의 메세지를 출력한다.
			if (  ds.Tables[0].Rows.Count > 3 )	// 데이터가 있는 작업장 이라면
			{		
				for ( int i = 0 ; i < ds.Tables[0].Rows.Count ; i = i + 3 )
				{
					for ( int j = 3 ; j  < ( System.DateTime.Today.Month + 4 ) ; j ++)				
					{
						decimal unq = (decimal)ds.Tables[0].Rows[i+1][j];	// 현재행의 부적합수량
						decimal qua = (decimal)ds.Tables[0].Rows[i+2][j];	// 현재행의 적합수량

						if (qua == 0)		// 생산수량이 없다면 부적합수량도 없으므로 0 을 1 로 나눈다
							qua = 1;
														
						ds.Tables[0].Rows[i][j] = (decimal)((unq / qua) * 1000000.00m);
					}
				}

				UltraWebGrid1.DataSource = ds.Tables[0].DefaultView;
				UltraWebGrid1.DataBind();
				this.SetGridStyle(ds.Tables[0].Rows.Count, ds.Tables[0].Columns.Count);
			}
			else	// 데이터가 없는 작업장
			{
				System.Text.StringBuilder sb = new System.Text.StringBuilder("검색조건인 작업장 (", 30);
				//sb.Append(wcWCName.SelectedRow.Cells[1].ToString());
				sb.Append(wcWCName.DisplayValue);
				sb.Append( ") 은(는) 현재 작업일보 원장에 기록된 데이터가 없습니다." );

				Alert( sb.ToString() );
				
				// DataTable의 [0]째 Row의 [0]째 컬럼이 DBNull 이면 DataSource 없이 바인딩 한다.
				UltraWebGrid1.DataBind();
			}

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
				dRow[i] = ds.Tables[0].Rows[0][i+4];
			}
			
			dtChartSource.Rows.Add(dRow);

			UltraChart1.Data.DataSource = dtChartSource.DefaultView;
			UltraChart1.Data.DataBind();
		}



		// 반드시 Grid 에 바인딩을 하고 스타일을 정의.
		private void SetGridStyle(int rowCount, int colCount)
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

			UltraWebGrid1.Columns[0].Width = 140;
		}


		private void UltraWebGrid1_ColumnMove(object sender, Infragistics.WebUI.UltraWebGrid.ColumnEventArgs e)
		{
			btnSearch_Click(sender, e);
		}



		private void Button2_Click(object sender, System.EventArgs e)
		{
			if (UltraWebGrid1.Rows.Count > 0)
				UltraWebGridExcelExporter1.Export(UltraWebGrid1);
			else
				Alert("Excel 로 출력될 검색결과가 없습니다.");
		}


	}
}
