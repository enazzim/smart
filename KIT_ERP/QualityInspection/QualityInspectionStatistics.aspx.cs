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
	/// <summary>
	/// QualityInspectionStatistics에 대한 요약 설명입니다.
	/// </summary>
	public class QualityInspectionStatistics :  KIT_ERP.QualityInspection.QualityInspection_BaseClass
	{
		protected System.Web.UI.WebControls.Button Button2;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid dgStatisticsResult;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser txtEndDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser txtStartDate;
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
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			string strDiv = ddlDiv.SelectedItem.Value;
			string startDate = txtStartDate.Text;
			string endDate = txtEndDate.Text;
			
			QualityInspection_Search qis = new QualityInspection_Search(PageNames.품질검사통계, strDiv, startDate, endDate);

			DataTable dt_GridSource = qis.GetDataSet().Tables[0];
			dgStatisticsResult.Columns.Clear();

			dgStatisticsResult.DataSource = dt_GridSource.DefaultView;
			dgStatisticsResult.DataBind();
			FooterGen( dt_GridSource , strDiv );

			if ( strDiv == "2") // 사내부적합
			{
				// WC 명
				dgStatisticsResult.Columns[0].Width = 155;

				// 총로트수
				dgStatisticsResult.Columns[1].Width = 90;
				dgStatisticsResult.Columns[1].CellStyle.HorizontalAlign=HorizontalAlign.Right;
				dgStatisticsResult.Columns[1].CellStyle.Padding.Right=7;
				dgStatisticsResult.Columns[1].Format="###,###,##0.00";
				dgStatisticsResult.Columns[1].FooterTotal = Infragistics.WebUI.UltraWebGrid.SummaryInfo.Sum;
				dgStatisticsResult.Columns[1].FooterStyle.HorizontalAlign = HorizontalAlign.Right;
				dgStatisticsResult.Columns[1].FooterStyle.Padding.Right = 7;

				// 부적합 건수
				dgStatisticsResult.Columns[2].Width = 90;
				dgStatisticsResult.Columns[2].CellStyle.HorizontalAlign=HorizontalAlign.Right;
				dgStatisticsResult.Columns[2].CellStyle.Padding.Right=7;
				dgStatisticsResult.Columns[2].Format="###,###,##0.00";
				dgStatisticsResult.Columns[2].FooterTotal = Infragistics.WebUI.UltraWebGrid.SummaryInfo.Sum;
				dgStatisticsResult.Columns[2].FooterStyle.HorizontalAlign = HorizontalAlign.Right;
				dgStatisticsResult.Columns[2].FooterStyle.Padding.Right = 7;

				// 부적합 율
				dgStatisticsResult.Columns[3].Width = 100;
				dgStatisticsResult.Columns[3].CellStyle.HorizontalAlign=HorizontalAlign.Right;
				dgStatisticsResult.Columns[3].CellStyle.Padding.Right=7;
				dgStatisticsResult.Columns[3].Format="###,###,##0.00";

				// 손실 금액
				dgStatisticsResult.Columns[4].Width = 130;
				dgStatisticsResult.Columns[4].CellStyle.HorizontalAlign=HorizontalAlign.Right;
				dgStatisticsResult.Columns[4].CellStyle.Padding.Right=7;
				dgStatisticsResult.Columns[4].Format="###,###,##0.00";
				dgStatisticsResult.Columns[4].FooterTotal = Infragistics.WebUI.UltraWebGrid.SummaryInfo.Sum;
				dgStatisticsResult.Columns[4].FooterStyle.HorizontalAlign = HorizontalAlign.Right;
				dgStatisticsResult.Columns[4].FooterStyle.Padding.Right = 7;

				// 거래수량
				dgStatisticsResult.Columns[5].Width = 105;
				dgStatisticsResult.Columns[5].CellStyle.HorizontalAlign=HorizontalAlign.Right;
				dgStatisticsResult.Columns[5].CellStyle.Padding.Right=7;
				dgStatisticsResult.Columns[5].Format="###,###,##0.00";
				dgStatisticsResult.Columns[5].FooterTotal = Infragistics.WebUI.UltraWebGrid.SummaryInfo.Sum;
				dgStatisticsResult.Columns[5].FooterStyle.HorizontalAlign = HorizontalAlign.Right;
				dgStatisticsResult.Columns[5].FooterStyle.Padding.Right = 7;

				// 부적합 수량
				dgStatisticsResult.Columns[6].Width = 105;
				dgStatisticsResult.Columns[6].CellStyle.HorizontalAlign=HorizontalAlign.Right;
				dgStatisticsResult.Columns[6].CellStyle.Padding.Right=7;
				dgStatisticsResult.Columns[6].Format="###,###,##0.00";
				dgStatisticsResult.Columns[6].FooterTotal = Infragistics.WebUI.UltraWebGrid.SummaryInfo.Sum;
				dgStatisticsResult.Columns[6].FooterStyle.HorizontalAlign = HorizontalAlign.Right;
				dgStatisticsResult.Columns[6].FooterStyle.Padding.Right = 7;
				
				// PPM
				dgStatisticsResult.Columns[7].Width = 100;
				dgStatisticsResult.Columns[7].CellStyle.HorizontalAlign=HorizontalAlign.Right;
				dgStatisticsResult.Columns[7].CellStyle.Padding.Right=7;
				dgStatisticsResult.Columns[7].Format="###,###,##0.00";
			}
			else if( strDiv == "0" || strDiv == "1" || strDiv == "3" || strDiv == "4" ) // 전체, 제품 부적합, 구매 부적합, 외주 부적합
			{
				// 거래처 명
				dgStatisticsResult.Columns[0].Width = 110;

				// 총로트수
				dgStatisticsResult.Columns[1].Width = 60;
				dgStatisticsResult.Columns[1].CellStyle.HorizontalAlign = HorizontalAlign.Right;
				dgStatisticsResult.Columns[1].CellStyle.Padding.Right = 7;
				dgStatisticsResult.Columns[1].Format="###,###,##0.00";
				dgStatisticsResult.Columns[1].FooterTotal = Infragistics.WebUI.UltraWebGrid.SummaryInfo.Sum;
				dgStatisticsResult.Columns[1].FooterStyle.HorizontalAlign = HorizontalAlign.Right;
				dgStatisticsResult.Columns[1].FooterStyle.Padding.Right = 7;

				// 부적합 건수
				dgStatisticsResult.Columns[2].Width = 80;
				dgStatisticsResult.Columns[2].CellStyle.HorizontalAlign=HorizontalAlign.Right;
				dgStatisticsResult.Columns[2].CellStyle.Padding.Right=7;
				dgStatisticsResult.Columns[2].Format="###,###,##0.00";
				dgStatisticsResult.Columns[2].FooterTotal = Infragistics.WebUI.UltraWebGrid.SummaryInfo.Sum;
				dgStatisticsResult.Columns[2].FooterStyle.HorizontalAlign = HorizontalAlign.Right;
				dgStatisticsResult.Columns[2].FooterStyle.Padding.Right = 7;

				// 부적합 율
				dgStatisticsResult.Columns[3].Width = 85;
				dgStatisticsResult.Columns[3].CellStyle.HorizontalAlign=HorizontalAlign.Right;
				dgStatisticsResult.Columns[3].CellStyle.Padding.Right=7;
				dgStatisticsResult.Columns[3].Format="###,###,##0.00";

				// 총 금액
				dgStatisticsResult.Columns[4].Width = 140;
				dgStatisticsResult.Columns[4].CellStyle.HorizontalAlign=HorizontalAlign.Right;
				dgStatisticsResult.Columns[4].CellStyle.Padding.Right=7;
				dgStatisticsResult.Columns[4].Format="###,###,##0.00";
				dgStatisticsResult.Columns[4].FooterTotal = Infragistics.WebUI.UltraWebGrid.SummaryInfo.Sum;
				dgStatisticsResult.Columns[4].FooterStyle.HorizontalAlign = HorizontalAlign.Right;
				dgStatisticsResult.Columns[4].FooterStyle.Padding.Right = 7;

				// 손실 금액
				dgStatisticsResult.Columns[5].Width = 140;
				dgStatisticsResult.Columns[5].CellStyle.HorizontalAlign=HorizontalAlign.Right;
				dgStatisticsResult.Columns[5].CellStyle.Padding.Right=7;
				dgStatisticsResult.Columns[5].Format="###,###,##0.00";
				dgStatisticsResult.Columns[5].FooterTotal = Infragistics.WebUI.UltraWebGrid.SummaryInfo.Sum;
				dgStatisticsResult.Columns[5].FooterStyle.HorizontalAlign = HorizontalAlign.Right;
				dgStatisticsResult.Columns[5].FooterStyle.Padding.Right = 7;

				// 손실율
				dgStatisticsResult.Columns[6].Width = 70;
				dgStatisticsResult.Columns[6].CellStyle.HorizontalAlign=HorizontalAlign.Right;
				dgStatisticsResult.Columns[6].CellStyle.Padding.Right=7;
				dgStatisticsResult.Columns[6].Format="###,###,##0.00";

				// 거래수량
				dgStatisticsResult.Columns[7].Width = 100;
				dgStatisticsResult.Columns[7].CellStyle.HorizontalAlign=HorizontalAlign.Right;
				dgStatisticsResult.Columns[7].CellStyle.Padding.Right=7;
				dgStatisticsResult.Columns[7].Format="###,###,##0.00";
				dgStatisticsResult.Columns[7].FooterTotal = Infragistics.WebUI.UltraWebGrid.SummaryInfo.Sum;
				dgStatisticsResult.Columns[7].FooterStyle.HorizontalAlign = HorizontalAlign.Right;
				dgStatisticsResult.Columns[7].FooterStyle.Padding.Right = 7;

				// 부적합 수량
				dgStatisticsResult.Columns[8].Width = 90;
				dgStatisticsResult.Columns[8].CellStyle.HorizontalAlign=HorizontalAlign.Right;
				dgStatisticsResult.Columns[8].CellStyle.Padding.Right=7;
				dgStatisticsResult.Columns[8].Format="###,###,##0.00";
				dgStatisticsResult.Columns[8].FooterTotal = Infragistics.WebUI.UltraWebGrid.SummaryInfo.Sum;
				dgStatisticsResult.Columns[8].FooterStyle.HorizontalAlign = HorizontalAlign.Right;
				dgStatisticsResult.Columns[8].FooterStyle.Padding.Right = 7;

				// PPM
				dgStatisticsResult.Columns[9].Width = 100;
				dgStatisticsResult.Columns[9].CellStyle.HorizontalAlign=HorizontalAlign.Right;
				dgStatisticsResult.Columns[9].CellStyle.Padding.Right=7;
				dgStatisticsResult.Columns[9].Format="###,###,##0.00";
			}
		}



		/// <summary>
		/// 웹 그리드의 하단부의 Footer 에 나타나는 통계치를 계산합니다. 웹 그리드에 바인딩 후 호출해야 합니다.
		/// </summary>
		/// <param name="dtGridSource">웹 그리드에 바인딩 된 DataTable</param>
		private void FooterGen(DataTable dtGridSource ,  string strDiv)
		{
			decimal deTotalLot = 0 ;				//총 LOT 수
			decimal deUnLot = 0 ;					// 부적합 LOT 건수
			decimal deTotalQuantity = 0.00M;		//총 거래수량
			decimal deUnsQuantity = 0.00M;		// 부적합량

			if ( strDiv == "2" ) // 사내부적합
			{
				for ( int i = 0 ; i < dtGridSource.Rows.Count ; i ++ )			
				{
					deTotalLot += Convert.ToInt32(dtGridSource.Rows[i][1]);
					deUnLot += Convert.ToInt32(dtGridSource.Rows[i][2]);

					deTotalQuantity += Convert.ToInt32(dtGridSource.Rows[i][5]);
					deUnsQuantity += Convert.ToInt32(dtGridSource.Rows[i][6]);
				}

				// Footer 에 출력될 부적합률 계산
				decimal deUnPer = ( deUnLot / deTotalLot ) * 100.00M;
				dgStatisticsResult.Columns[3].FooterText = deUnPer.ToString("###,###,##0.00");
				dgStatisticsResult.Columns[3].FooterStyle.HorizontalAlign = HorizontalAlign.Right;
				dgStatisticsResult.Columns[3].FooterStyle.Padding.Right = 7;
			
				// Footer 에 출력될 PPM 계산
				decimal dePPM = deUnsQuantity / deTotalQuantity * 1000000.00M;
				dgStatisticsResult.Columns[7].FooterText = dePPM.ToString("###,###,##0.00");
				dgStatisticsResult.Columns[7].FooterStyle.HorizontalAlign = HorizontalAlign.Right;
				dgStatisticsResult.Columns[7].FooterStyle.Padding.Right = 7;
			}
			else	// 전체, 제품 부적합, 구매 부적합, 외주 부적합
			{
				for ( int i = 0 ; i < dtGridSource.Rows.Count ; i ++ )			
				{
					deTotalLot += Convert.ToInt32(dtGridSource.Rows[i][1]);
					deUnLot += Convert.ToInt32(dtGridSource.Rows[i][2]);

					deTotalQuantity += Convert.ToInt32(dtGridSource.Rows[i][7]);
					deUnsQuantity += Convert.ToInt32(dtGridSource.Rows[i][8]);
				}

				// Footer 에 출력될 부적합률 계산
				if ( deTotalLot > 0 )
				{
					decimal deUnPer = ( deUnLot / deTotalLot ) * 100.00M;
					dgStatisticsResult.Columns[3].FooterText = deUnPer.ToString("###,###,##0.00");
					dgStatisticsResult.Columns[3].FooterStyle.HorizontalAlign = HorizontalAlign.Right;
					dgStatisticsResult.Columns[3].FooterStyle.Padding.Right = 7;
				}
				if ( deTotalQuantity > 0 )
				{
					// Footer 에 출력될 PPM 계산
					decimal dePPM = deUnsQuantity / deTotalQuantity * 1000000.00M;
					dgStatisticsResult.Columns[9].FooterText = dePPM.ToString("###,###,##0.00");
					dgStatisticsResult.Columns[9].FooterStyle.HorizontalAlign = HorizontalAlign.Right;
					dgStatisticsResult.Columns[9].FooterStyle.Padding.Right = 7;
				}
			}
		}


		private void Button2_Click(object sender, System.EventArgs e)
		{
			if (dgStatisticsResult.Rows.Count > 0)
				UltraWebGridExcelExporter1.Export(dgStatisticsResult);
			else
				Alert("Excel 로 출력될 검색결과가 없습니다.");
		}

	}
}

