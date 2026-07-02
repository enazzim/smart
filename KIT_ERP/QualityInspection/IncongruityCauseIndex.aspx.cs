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

namespace KIT_ERP.QualityInspection
{
	public class IncongruityCauseIndex : QualityInspection_BaseClass
	{
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Button Button2;
		protected Infragistics.WebUI.UltraWebChart.UltraChart UltraChart1;
		protected Infragistics.WebUI.UltraWebChart.UltraChart UltraChart2;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.WebControls.DropDownList ddlDiv;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
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
			this.UltraWebGrid1.SortColumn += new Infragistics.WebUI.UltraWebGrid.SortColumnEventHandler(this.UltraWebGrid1_SortColumn);
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		// 검색버튼 이벤트
		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			// 구분 드롭다운 리스트 값.
			string strDiv = ddlDiv.SelectedValue;

			// 객체생성, DataTable 결과 받음.
			QualityInspection_Search qis  = new QualityInspection_Search(PageNames.부적합원인지표, strDiv);
			DataTable dtGridSource = qis.GetDataSet().Tables[0];

			if ( dtGridSource.Rows.Count > 1 )
			{
				// 우측 파이챠트( 부적합 현상별 집계 ) 에 사용될 DataSource( dt2 ) 만들기
				DataTable dtChartSource2_Pie = new DataTable();
				DataTable dtChartSource1 = dtGridSource.Copy();

				dtChartSource2_Pie.Columns.Add(new DataColumn("Div",typeof(string)));
				dtChartSource2_Pie.Columns.Add(new DataColumn("Total", typeof(float)));
						
				for(int i = 1 ; i < dtGridSource.Rows.Count ; i++)
				{
					DataRow dr2 = dtChartSource2_Pie.NewRow();
					
					dr2[0] = dtGridSource.Rows[i][0].ToString();
					dr2[1] = dtGridSource.Rows[i][2].ToString();
					
					dtChartSource2_Pie.Rows.Add(dr2);
				}

				// 필요 없는 컬럼과 행을 삭제
				dtChartSource1.Columns.RemoveAt(1);
				dtChartSource1.Columns.RemoveAt(1);
				dtChartSource1.Rows.RemoveAt(0);

				// 그리드 바인딩
				UltraWebGrid1.DataSource = dtGridSource.DefaultView;
				UltraWebGrid1.DataBind();

				// 좌측 챠트
				UltraChart1.Data.DataSource = dtChartSource1.DefaultView;
				UltraChart1.Data.DataBind();
				// 우측 파이챠트
				UltraChart2.Data.DataSource = dtChartSource2_Pie.DefaultView;
				UltraChart2.Data.DataBind();

				this.SetGridStyle(dtGridSource.Rows.Count , dtGridSource.Columns.Count);
			}
			else
			{
				UltraChart1.EmptyChartText = "조회된 데이터가 없습니다.";
				//UltraChart1.Font = System.Drawing.Font = new Font(new FontFamily(
			}
		}

		private void UltraWebGrid1_ColumnMove(object sender, Infragistics.WebUI.UltraWebGrid.ColumnEventArgs e)
		{
			this.btnSearch_Click(sender, e);
		}

		private void UltraWebGrid1_SortColumn(object sender, Infragistics.WebUI.UltraWebGrid.SortColumnEventArgs e)
		{
			this.btnSearch_Click(sender, e);
		}

		// 엑셀 출력
		private void Button2_Click(object sender, System.EventArgs e)
		{
			if (UltraWebGrid1.Rows.Count > 0)
				UltraWebGridExcelExporter1.Export(UltraWebGrid1);
			else
				Alert("Excel 로 출력될 검색결과가 없습니다.");
		}

		// 반드시 Grid 에 바인딩을 하고 스타일을 정의.
		private void SetGridStyle(int rowCount, int colCount)
		{			
			// 각 컬럼의 Format 형식 또는 Align 정의
			for ( int i = 1 ; i < colCount ; i ++ )
			{
				UltraWebGrid1.Columns[i].Format = "###,###,##0.00";
				UltraWebGrid1.Columns[i].CellStyle.HorizontalAlign = HorizontalAlign.Right;
				UltraWebGrid1.Columns[i].CellStyle.Padding.Right = 7;
			}
		}
		
	}
}
