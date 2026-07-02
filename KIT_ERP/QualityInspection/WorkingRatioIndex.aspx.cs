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
	/// WorkingRatioIndex에 대한 요약 설명입니다.
	/// </summary>
	public class WorkingRatioIndex :  QualityInspection.QualityInspection_BaseClass
	{
		protected System.Web.UI.WebControls.Button btnSearch;
		protected Infragistics.WebUI.UltraWebChart.UltraChart UltraChart1;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid DataGrid1;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected Infragistics.WebUI.WebCombo.WebCombo wcWCName;
		protected System.Web.UI.WebControls.Button Button2;
	
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
				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 경영정보 > 가동율 지표';</script>");
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

		/// <summary>
		/// 검색에 필요하거나 기타 등 페이지 초기상태를 세팅한다.
		/// </summary>
		private void Load_WebCombo_Bind()
		{
			SqlDataAdapter adap = new SqlDataAdapter("PageLoad_GetDataSource" , base.GetConnectionString);
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

			strWCName = wcWCName.DisplayValue;
			webComboSelected = false;

			ManagementInfomation_Search mis = new ManagementInfomation_Search
				(PageNames.가동률지표, strWCName, webComboSelected);

			try
			{
				DataTable dt_GridSource = mis.GetDataSet().Tables[0];
				this.ProcessGridData( dt_GridSource );
			}
			catch(Exception Ex)
			{
				Alert("예기치 않은 오류가 발생하였습니다 : " + Ex.Message);
			}	
		}


		/// <summary>
		///  Grid에 표현될 데이터의 세부내용(가동률)을 계산하고 챠트데이터를 계산하여 각각 Grid와 Chart에 바인딩 하며 
		///  Grid의 스타일을 정의하는 SetGridStyle() 함수를 본 함수 내부에서 호출하여 Grid 스타일을 정의해 준다.
		/// </summary>
		/// <param name="dt_GridSource">Grid에 표현될 데이터 원본</param>
		private void ProcessGridData( DataTable dt_GridSource )
		{
			if (  dt_GridSource.Rows.Count > 3 )		// 데이터가 있는 거래처 이라면 - ( 유효한 데이터 )
			{
				for ( int i = 0 ; i < dt_GridSource.Rows.Count ; i = i + 3 )
				{
					for ( int j = 3 ; j < ( System.DateTime.Today.Month + 4 ) ; j ++ )
					{
						decimal deP_Time = Convert.ToDecimal(dt_GridSource.Rows[i+1][j]);	// 계획시간
						decimal deW_Time = Convert.ToDecimal(dt_GridSource.Rows[i+2][j]);	// 작업시간
	
						if ( deW_Time == 0 )			// 작업시간이 없는 경우 당연히 가동률은 0.00% 이다.
						{
							dt_GridSource.Rows[i][j] = 0.00M;
						}
						else if ( deP_Time == 0 )		// 계획시간이 없는 경우에 작업시간이 존재할 수 있으므로 가동률은 작업시간% 이다.
						{
							dt_GridSource.Rows[i][j] = deW_Time;
						}
						else						// 계획시간과 작업시간이 모두 존재 하는 경우에 가동률을 계산. 
						{							// (계획시간과 작업시간이 모두 0 인경우는 첫번째 if 에서 걸림)
							dt_GridSource.Rows[i][j] = (decimal)((deW_Time / deP_Time) * 100.00m);
						}
					}
				}

				DataGrid1.DataSource = dt_GridSource.DefaultView;
				DataGrid1.DataBind();

				this.SetGridStyle(dt_GridSource.Rows.Count, dt_GridSource.Columns.Count);

				dt_GridSource.Columns.RemoveAt(0);			// 차트에 표현될 데이터는 작업장 구분, 전년도, 누계는 제외하고
				dt_GridSource.Columns.RemoveAt(1);			// 개별적인 작업장들은 제외한 "합계"만 필요하므로 합계를 제외한 
				dt_GridSource.Columns.RemoveAt(1);			// 다른 행들은 모두 삭제 한다.
				for ( int i = 3 ; i < dt_GridSource.Rows.Count ; i ++ )
					dt_GridSource.Rows[i].Delete();	

				UltraChart1.Data.DataSource = dt_GridSource.DefaultView;
				UltraChart1.Data.DataBind();
			}
			else	// 데이터가 없는 경우
			{
				Alert( "기록된 데이터가 없습니다." );
				DataGrid1.DataBind();
			}
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
				DataGrid1.Columns[i].Format = "###,###,##0.00";
				DataGrid1.Columns[i].CellStyle.HorizontalAlign = HorizontalAlign.Right;
				DataGrid1.Columns[i].CellStyle.Padding.Right = 7;
			}

			DataGrid1.Rows[0].Style.BackColor = Color.NavajoWhite;
			DataGrid1.Rows[0].Style.Font.Bold = true;
			DataGrid1.Rows[1].Style.BackColor = Color.NavajoWhite;
			DataGrid1.Rows[2].Style.BackColor = Color.NavajoWhite;
		}

		private void Button2_Click(object sender, System.EventArgs e)
		{
			if (DataGrid1.Rows.Count > 0)
				UltraWebGridExcelExporter1.Export(DataGrid1);
			else
				Alert("Excel 로 출력될 검색결과가 없습니다.");
		}

		private void DataGrid1_ColumnMove(object sender, Infragistics.WebUI.UltraWebGrid.ColumnEventArgs e)
		{
			btnSearch_Click(sender, e);
		}

	}
}
