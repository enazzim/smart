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
using System.Data.SqlClient;


namespace KIT_ERP.QualityInspection
{
	public class AutonomyInspectStatistics :  QualityInspection_BaseClass
	{
		protected Infragistics.WebUI.WebCombo.WebCombo wcWCName;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification1;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification2;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification4;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wcStartDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wcEndDate;
		protected System.Web.UI.WebControls.Button Button2;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid dgStatisticsResult;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification3;
	

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			if(!IsPostBack)
			{
				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 품질관리 > 자주검사 통계';</script>");

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

			
			QualityInspection_Search qis = 
								new QualityInspection_Search
								(	
									PageNames.자주검사통계   
									, strWCName		// 사용자가 입력한 wc명 또는 선택된 행(작업장) 의 인덱스 번호									
									, wcStartDate.Text 
									, wcEndDate.Text
									, strItemClassification1	
									, strItemClassification2
									, strItemClassification3	
									, strItemClassification4 
								) ;

			DataTable dt_GridSource = qis.GetDataSet().Tables[0];
			dgStatisticsResult.DataSource = dt_GridSource.DefaultView;
			dgStatisticsResult.DataBind();

			
			this.FooterGen(dt_GridSource);


//			dgStatisticsResult.Columns[0].FooterText = "ewfwefwfewef";
		}


		private void FooterGen(DataTable FooterGen)
		{
			decimal nTotalLot = 0 ;					//총 LOT 수
			decimal nUnLot = 0 ;						// 부적합 LOT 건수
			decimal deTotalQuantity = 0.00M;			//총 거래수량
			decimal deUnsQuantity = 0.00M;			// 부적합량

			for ( int i = 0 ; i < FooterGen.Rows.Count ; i ++ )			
			{
				nTotalLot += Convert.ToInt32(FooterGen.Rows[i][1]);
				nUnLot += Convert.ToInt32(FooterGen.Rows[i][2]);

				deTotalQuantity += Convert.ToInt32(FooterGen.Rows[i][5]);
				deUnsQuantity += Convert.ToInt32(FooterGen.Rows[i][6]);
			}

			// Footer 에 출력될 부적합률 계산
			decimal deUnPer = ( nUnLot / nTotalLot ) * 100.00M;
			dgStatisticsResult.Columns[3].FooterText = deUnPer.ToString("###,###,##0.00");

			// Footer 에 출력될 PPM 계산
			decimal dePPM = deUnsQuantity / deTotalQuantity * 1000000.00M;
			dgStatisticsResult.Columns[7].FooterText = dePPM.ToString("###,###,##0.00");
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
