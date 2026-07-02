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
	/// StockMoneyIndex에 대한 요약 설명입니다.
	/// </summary>
	public class StockMoneyIndex :  QualityInspection.QualityInspection_BaseClass
	{
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid DataGrid1;
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Data.SqlClient.SqlDataAdapter sqlDataAdapter1;
		protected System.Data.SqlClient.SqlCommand sqlSelectCommand1;
		protected System.Data.SqlClient.SqlConnection sqlConnection1;
		protected Infragistics.WebUI.UltraWebChart.UltraChart UltraChart1;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			if ( !IsPostBack )
			{	//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 경영정보 > 재고금액 지표';</script>");

				sqlConnection1.ConnectionString = base.GetConnectionString;

				DataSet ds = new DataSet();
				sqlDataAdapter1.Fill(ds);

				DataTable dt_GridSource = ds.Tables[0];
				DataTable dt_ChartSource = dt_GridSource.Clone();
			
				DataGrid1.DataSource = dt_GridSource.DefaultView;
				DataGrid1.DataBind();

				for ( int i = 1 ; i < DataGrid1.Columns.Count ; i ++ )
				{
					DataGrid1.Columns[i].Format = "###,###,##0.00";
					DataGrid1.Columns[i].FooterTotal = Infragistics.WebUI.UltraWebGrid.SummaryInfo.Sum;
				
					DataGrid1.Columns[i].FooterStyle.HorizontalAlign = HorizontalAlign.Right;
					DataGrid1.Columns[i].FooterStyle.Padding.Right = 5;

					DataGrid1.Columns[i].CellStyle.HorizontalAlign = HorizontalAlign.Right;
					DataGrid1.Columns[i].CellStyle.Padding.Right = 5;
				}

				DataGrid1.Columns[0].Width = 100;

				UltraChart1.Data.DataSource = this.ProcessChartSource( dt_ChartSource , dt_GridSource);
				UltraChart1.Data.DataBind();
				
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
			this.sqlDataAdapter1 = new System.Data.SqlClient.SqlDataAdapter();
			this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
			this.sqlConnection1 = new System.Data.SqlClient.SqlConnection();
			this.DataGrid1.ColumnMove += new Infragistics.WebUI.UltraWebGrid.ColumnMoveEventHandler(this.DataGrid1_ColumnMove);
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			// 
			// sqlDataAdapter1
			// 
			this.sqlDataAdapter1.SelectCommand = this.sqlSelectCommand1;
			this.sqlDataAdapter1.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																		  new System.Data.Common.DataTableMapping("Table", "StockMoneyIndex_Load", new System.Data.Common.DataColumnMapping[] {
																																						    new System.Data.Common.DataColumnMapping("도면", "도면"),
																																						    new System.Data.Common.DataColumnMapping("전년도 금액", "전년도 금액"),
																																						    new System.Data.Common.DataColumnMapping("1월", "1월"),
																																						    new System.Data.Common.DataColumnMapping("2월", "2월"),
																																						    new System.Data.Common.DataColumnMapping("3월", "3월"),
																																						    new System.Data.Common.DataColumnMapping("4월", "4월"),
																																						    new System.Data.Common.DataColumnMapping("5월", "5월"),
																																						    new System.Data.Common.DataColumnMapping("6월", "6월"),
																																						    new System.Data.Common.DataColumnMapping("7월", "7월"),
																																						    new System.Data.Common.DataColumnMapping("8월", "8월"),
																																						    new System.Data.Common.DataColumnMapping("9월", "9월"),
																																						    new System.Data.Common.DataColumnMapping("10월", "10월"),
																																						    new System.Data.Common.DataColumnMapping("11월", "11월"),
																																						    new System.Data.Common.DataColumnMapping("12월", "12월")})});
			// 
			// sqlSelectCommand1
			// 
			this.sqlSelectCommand1.CommandText = "[StockMoneyIndex_Load]";
			this.sqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure;
			this.sqlSelectCommand1.Connection = this.sqlConnection1;
			this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion



		private DataTable ProcessChartSource(DataTable dt_ChartSource, DataTable dt_GridSource)
		{
			dt_ChartSource.Columns.RemoveAt(1);
			dt_GridSource.Columns.RemoveAt(1);

			DataRow drow = dt_ChartSource.NewRow();
			
			drow[0] = "합 계";
			
			// 0 으로 초기화
                        for ( int k = 1 ; k < drow.Table.Columns.Count ; k ++ )
			{
				drow[k] = 0.00m;
			}

			for ( int i = 0 ; i < dt_GridSource.Rows.Count ; i ++ )
			{
				for ( int j = 1 ; j < dt_GridSource.Columns.Count ; j ++ )
				{
					if ( !Convert.IsDBNull( dt_GridSource.Rows[i][j] )  )
					{
						drow[j] = Convert.ToDecimal( drow[j] ) + Convert.ToDecimal( dt_GridSource.Rows[i][j].ToString() );
					}
				}
			}
			
			dt_ChartSource.Rows.InsertAt(drow, 0);
		
			return dt_ChartSource;
		}


		// Excel Export
		private void Button2_Click(object sender, System.EventArgs e)
		{
			if (DataGrid1.Rows.Count > 0)
				UltraWebGridExcelExporter1.Export(DataGrid1);
			else
				Alert("Excel 로 출력될 검색결과가 없습니다.");
		}

		private void DataGrid1_ColumnMove(object sender, Infragistics.WebUI.UltraWebGrid.ColumnEventArgs e)
		{
			Page_Load(sender, e);
		}

		
	}
}
