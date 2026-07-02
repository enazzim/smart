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
	/// NonWorkingRatioIndex에 대한 요약 설명입니다.
	/// </summary>
	public class NonWorkingRatioIndex :  QualityInspection.QualityInspection_BaseClass
	{
		protected System.Web.UI.WebControls.Button btnSearch;
		protected Infragistics.WebUI.UltraWebChart.UltraChart UltraChart1;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid DataGrid1;
		protected System.Web.UI.WebControls.Button Button2;
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
				// 페이지가 처음 로드될때 실행
				this.Load_WebCombo_Bind();
				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 경영정보 > 비가동 지표';</script>");
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
			SqlDataAdapter adap = new SqlDataAdapter("PageLoad_GetDataSource" ,  base.GetConnectionString);
			adap.SelectCommand.CommandType = CommandType.StoredProcedure;
			
			adap.SelectCommand.Parameters.Add("@Wcname", SqlDbType.Bit).Value = 1;
			
			DataSet ds = new DataSet();
			adap.Fill(ds);

			wcWCName.DataSource = ds.Tables[0].DefaultView;
			wcWCName.DataTextField = "작업장 명";
			wcWCName.DataValueField = "WCInfoIndex";
			wcWCName.DataBind();
			wcWCName.Columns[0].Hidden = true;

			wcWCName.Columns[1].Width = 130;
			wcWCName.Columns[2].Width = 130;
			wcWCName.Columns[3].Width = 70;
		}



		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			string strWCName = "";
			bool webComboSelected;

			if( wcWCName.SelectedIndex != -1)
			{
				strWCName = wcWCName.SelectedRow.Cells[0].ToString();
				webComboSelected = true;
			}
			else
			{
				strWCName = wcWCName.DisplayValue;
				webComboSelected = false;
			}

			ManagementInfomation_Search mis = new ManagementInfomation_Search
				( PageNames.비가동지표, strWCName, webComboSelected );
			
			this.DataBind( mis.GetDataSet().Tables[0] );
			this.SetGridStyle( mis.GetDataSet().Tables[0].Rows.Count, mis.GetDataSet().Tables[0].Columns.Count);
		}

		
		

		private void DataBind( DataTable dt_GridSource )
		{
			// 데이터가 있는 경우
			if (  dt_GridSource.Rows.Count > 1 )
			{
				DataGrid1.DataSource = dt_GridSource.DefaultView;
				DataGrid1.DataBind();

				string[] strArr = new string[dt_GridSource.Rows.Count];
				int intNewIndex = 0;

				// 가변 행 RowSpan
				for ( int i = 0 ; i < dt_GridSource.Rows.Count ; i ++ )
				{
					if ( i == 0 ) 
					{
						DataGrid1.Rows[0].Cells[0].Style.HorizontalAlign = HorizontalAlign.Center;
						DataGrid1.Rows[0].Cells[0].Style.Font.Bold = true;
					}
					else
					{
						strArr[i] = DataGrid1.Rows[i].Cells[0].Text;
					
						if ( strArr[i] != strArr[i-1])
						{
							DataGrid1.Rows[intNewIndex].Cells[0].RowSpan = (i - intNewIndex);
							DataGrid1.Rows[intNewIndex].Cells[0].Style.HorizontalAlign = HorizontalAlign.Center;
							DataGrid1.Rows[intNewIndex].Cells[0].Style.Font.Bold = true;
							DataGrid1.Rows[intNewIndex].Style.BackColor = Color.WhiteSmoke;
							intNewIndex = i;
						}
						else if ( (dt_GridSource.Rows.Count - 1) == i )
						{
							DataGrid1.Rows[intNewIndex].Cells[0].RowSpan = (i - intNewIndex) + 1 ;
							DataGrid1.Rows[intNewIndex].Cells[0].Style.HorizontalAlign = HorizontalAlign.Center;
							DataGrid1.Rows[intNewIndex].Cells[0].Style.Font.Bold = true;
							DataGrid1.Rows[intNewIndex].Style.BackColor = Color.WhiteSmoke;
						}
					}
				}


				DataGrid1.Rows[0].Style.BackColor = Color.NavajoWhite;
				DataGrid1.Rows[0].Style.Font.Bold = true;


				DataRow[] dr_ChartSource = dt_GridSource.Select("[구분] = '합계'");
				DataTable dt_ChartSource = new DataTable();
				dt_ChartSource = dt_GridSource.Clone();

				foreach ( DataRow dr in dr_ChartSource )
				{
					DataRow tr = dt_ChartSource.NewRow();

					for ( int j = 0 ; j <  dr.Table.Columns.Count ; j ++ )
					{
						tr[j] =  dr[j];
					}

					dt_ChartSource.Rows.Add(tr);
				}
				
				dt_ChartSource.Columns.RemoveAt(1);
				dt_ChartSource.Columns.RemoveAt(1);
				dt_ChartSource.Columns.RemoveAt(1);

				UltraChart1.Data.DataSource = dt_ChartSource.DefaultView;
				UltraChart1.Data.DataBind();
			}
			else
			{
				Alert("검색된 데이타가 없습니다.");
				DataGrid1.DataBind();
			}
		}


		private void Button2_Click(object sender, System.EventArgs e)
		{
			if ( DataGrid1.Rows.Count > 0 )
				UltraWebGridExcelExporter1.Export( DataGrid1 );
			else
				Alert("Excel 로 출력될 검색결과가 없습니다.");
		}

		// 반드시 Grid 에 바인딩을 하고 스타일을 정의.
		private void SetGridStyle(int rowCount, int colCount)
		{			
			if (rowCount > 1)
			{
				// 각 컬럼의 Format 형식 또는 Align 정의
				for ( int i = 2 ; i < colCount ; i ++ )
				{
					DataGrid1.Columns[i].Format = "###,###,##0.00";
					DataGrid1.Columns[i].CellStyle.HorizontalAlign = HorizontalAlign.Right;
					DataGrid1.Columns[i].CellStyle.Padding.Right = 7;
				}

				DataGrid1.Rows[0].Style.BackColor = Color.NavajoWhite;
				DataGrid1.Rows[0].Style.Font.Bold = true;

				DataGrid1.Rows[1].Style.BackColor = Color.NavajoWhite;

				DataGrid1.Rows[2].Style.BackColor = Color.NavajoWhite;
			}
		}

		private void DataGrid1_ColumnMove(object sender, Infragistics.WebUI.UltraWebGrid.ColumnEventArgs e)
		{
			this.btnSearch_Click(sender, e);
		}



	}
}
