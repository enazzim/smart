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

namespace KIT_ERP.Management
{
	public class QuantityStandardIndex :  QualityInspection.QualityInspection_BaseClass
	{
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification1;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification2;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification3;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification4;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected Infragistics.WebUI.UltraWebChart.UltraChart UltraChart1;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid DataGrid1;
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.DropDownList ddlDIV;
		protected Infragistics.WebUI.WebCombo.WebCombo wcCompanyName;
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
				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 품질관리 > 수량기준 지표';</script>");
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
			SqlDataAdapter adap = new SqlDataAdapter("PageLoad_GetDataSource" , base.GetConnectionString);
			adap.SelectCommand.CommandType = CommandType.StoredProcedure;
			
			adap.SelectCommand.Parameters.Add("@ItemClassification1", SqlDbType.Bit).Value = 1;
			adap.SelectCommand.Parameters.Add("@ItemClassification2", SqlDbType.Bit).Value = 1;
			adap.SelectCommand.Parameters.Add("@ItemClassification3", SqlDbType.Bit).Value = 1;
			adap.SelectCommand.Parameters.Add("@ItemClassification4", SqlDbType.Bit).Value = 1;
			adap.SelectCommand.Parameters.Add("@CompanyName", SqlDbType.Bit).Value = 1;
			
			DataSet ds = new DataSet();
			adap.Fill(ds);

			wcCompanyName.DataSource = ds.Tables[0].DefaultView;
			wcCompanyName.DataTextField = "거래처 명";
			wcCompanyName.DataValueField = "사업자등록번호";

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

			wcCompanyName.Columns[0].Width = 150;
			wcCompanyName.Columns[1].Width = 80;
			wcCompanyName.Columns[2].Width = 115;
		}






		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			string strCompanyName;
			string strItemClassification1 = "";
			string strItemClassification2 = "";
			string strItemClassification3 = "";
			string strItemClassification4 = "";
			string strDIV = "" ;

			if ( ddlDIV.SelectedIndex > 0 )
				strDIV = ddlDIV.SelectedItem.Text;

			if ( ddlItemClassification1.SelectedIndex > 0 )
				strItemClassification1 = ddlItemClassification1.SelectedValue;

			if ( ddlItemClassification2.SelectedIndex > 0 )
				strItemClassification2 = ddlItemClassification2.SelectedValue;
			
			if ( ddlItemClassification3.SelectedIndex > 0 )
				strItemClassification3 = ddlItemClassification3.SelectedValue;

			if ( ddlItemClassification4.SelectedIndex > 0 )
				strItemClassification4 = ddlItemClassification4.SelectedValue;

			strCompanyName = wcCompanyName.DisplayValue;

			KIT_ERP.ManagementInfomation.ManagementInfomation_Search mis = new KIT_ERP.ManagementInfomation.ManagementInfomation_Search
				(PageNames.수량기준지표, strItemClassification1, strItemClassification2, strItemClassification3, strItemClassification4,  strDIV, strCompanyName );

			DataTable dt_GridSource =  mis.GetDataSet().Tables[0] ;

			if ( dt_GridSource.Rows.Count > 3 )
			{
				dt_GridSource = this.ProcessGridSource( dt_GridSource );
				DataGrid1.DataSource = dt_GridSource.DefaultView;
				DataGrid1.DataBind();

				this.SetGridStyle( dt_GridSource.Rows.Count ,  dt_GridSource.Columns.Count );

				DataTable dt_ChartSource = this.ProcessChartSource( dt_GridSource );
				UltraChart1.Data.DataSource = dt_ChartSource.DefaultView;
				UltraChart1.Data.DataBind();

				Session["DT1"] = dt_ChartSource.DefaultView;
			}
			else
			{
				Alert("검색된 데이타가 없습니다.");
			}
		}




		private DataTable ProcessGridSource( DataTable dt_GridSource )
		{
			for ( int i = 0 ; i < dt_GridSource.Rows.Count ; i = i + 3 )
			{
				for ( int j = 3 ; j < ( System.DateTime.Today.Month + 4 ) ; j ++ )
				{
					decimal de_IncongruityQuantity = Convert.ToDecimal(dt_GridSource.Rows[i+1][j]);
					decimal de_TotalQuantity = Convert.ToDecimal(dt_GridSource.Rows[i+2][j]);
	
					// PPM 계산
					dt_GridSource.Rows[i][j] = base.CalculatePPM(de_IncongruityQuantity, de_TotalQuantity);
				}
			}

			return dt_GridSource;
		}





		private DataTable ProcessChartSource( DataTable dt_ChartSource )
		{
			dt_ChartSource.Columns.RemoveAt(0);
			dt_ChartSource.Columns.RemoveAt(1);
			dt_ChartSource.Columns.RemoveAt(1);
			dt_ChartSource.Rows[0].Delete();

			for ( int i = 3 ; i < dt_ChartSource.Rows.Count ; i ++ )
			{
				dt_ChartSource.Rows[i].Delete();
			}

			dt_ChartSource.AcceptChanges();

			for( int j = 1 ; j < dt_ChartSource.Columns.Count ; j ++ )
			{
				dt_ChartSource.Rows[1][j] = ( Convert.ToDecimal( dt_ChartSource.Rows[1][j] ) - Convert.ToDecimal( dt_ChartSource.Rows[0][j] ) );
			}

			dt_ChartSource.Rows[1][0] = "적합 수량";

			return dt_ChartSource;
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
			UltraChart1.Data.DataSource = Session["DT1"];
			UltraChart1.Data.DataBind();
		}
	}
}
