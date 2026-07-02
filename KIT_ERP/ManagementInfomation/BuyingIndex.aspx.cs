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
	/// BuyingIndex에 대한 요약 설명입니다.
	/// </summary>
	public class BuyingIndex :  QualityInspection.QualityInspection_BaseClass
	{
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification1;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification2;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification3;
		protected Infragistics.WebUI.UltraWebChart.UltraChart UltraChart1;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid DataGrid1;
		protected System.Web.UI.WebControls.DropDownList ddlBuyingBiv;
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification4;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}
			CSC1.UnitCostDistinction ="구매외주";
			if(!IsPostBack)
			{
				// 페이지가 처음 로드될때 실행
				this.Load_WebCombo_Bind();
				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 경영정보 > 매입지표';</script>");
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
			this.DataGrid1.SortColumn += new Infragistics.WebUI.UltraWebGrid.SortColumnEventHandler(this.DataGrid1_SortColumn);
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion



		private void Load_WebCombo_Bind()
		{
			SqlDataAdapter adap = new SqlDataAdapter("PageLoad_GetDataSource" , base.GetConnectionString);
			adap.SelectCommand.CommandType = CommandType.StoredProcedure;

			// 수주원장 거래처만 가져옴
			adap.SelectCommand.Parameters.Add("@ItemClassification1", SqlDbType.Bit).Value = 1;
			adap.SelectCommand.Parameters.Add("@ItemClassification2", SqlDbType.Bit).Value = 1;
			adap.SelectCommand.Parameters.Add("@ItemClassification3", SqlDbType.Bit).Value = 1;
			adap.SelectCommand.Parameters.Add("@ItemClassification4", SqlDbType.Bit).Value = 1;
			
			DataSet ds = new DataSet();
			adap.Fill(ds);

			
			//wcCompanyName.DataValueField = "사업자등록번호";

			ddlItemClassification1.DataSource = ds.Tables[0].DefaultView;
			ddlItemClassification1.DataTextField = "SmallClassificationName";
			ddlItemClassification1.DataValueField = "SmallClassificationCode";

			ddlItemClassification2.DataSource = ds.Tables[1].DefaultView;
			ddlItemClassification2.DataTextField = "SmallClassificationName";
			ddlItemClassification2.DataValueField = "SmallClassificationCode";

			ddlItemClassification3.DataSource = ds.Tables[2].DefaultView;
			ddlItemClassification3.DataTextField = "SmallClassificationName";
			ddlItemClassification3.DataValueField = "SmallClassificationCode";

			ddlItemClassification4.DataSource = ds.Tables[3].DefaultView;
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
			string strCompanyName=CSC1.Company.Trim();
			string strBusinessRegistrationNum = CSC1.BusinessRegistrationNum.Trim();
			string strBuyingBiv = ddlBuyingBiv.SelectedValue;
			

			ManagementInfomation_Search mis = new ManagementInfomation_Search
				( PageNames.매입지표, strItemClassification1, strItemClassification2, strItemClassification3, strItemClassification4
				, strBusinessRegistrationNum, strCompanyName , strBuyingBiv);

			DataTable dt_GridSource = mis.GetDataSet().Tables[0];

			// 결과가 있다면.
			if ( dt_GridSource.Rows.Count > 1 )
			{
				DataGrid1.DataSource = dt_GridSource;
				DataGrid1.DataBind();
				this.SetGridStyle( dt_GridSource.Rows.Count, dt_GridSource.Columns.Count );
				
				DataView dv_ChartSource = this.ChartDataSource_Create( dt_GridSource );

				UltraChart1.Data.DataSource = dv_ChartSource;
				UltraChart1.Data.DataBind();

				//Session["DT1"] = dv_ChartSource;
			}
			else
			{
				Alert("입력된 데이터가 없습니다.");
				DataGrid1.DataBind();
			}
		}



		// 챠트 DataSource 만들기.
		private  DataView  ChartDataSource_Create( DataTable dt_ChartSource )
		{
			for ( int i = 1 ; i < dt_ChartSource.Rows.Count ; i ++ )
			{
				dt_ChartSource.Rows[i].Delete();
			}

			dt_ChartSource.Columns.RemoveAt(0);
			dt_ChartSource.Columns.RemoveAt(0);
			dt_ChartSource.Columns.RemoveAt(0);

			dt_ChartSource.AcceptChanges();
			return dt_ChartSource.DefaultView;
		}



		private void Button2_Click(object sender, System.EventArgs e)
		{
			if (DataGrid1.Rows.Count > 0)
				UltraWebGridExcelExporter1.Export(DataGrid1);
			else
				Alert("Excel 로 출력될 검색결과가 없습니다.");
		}




		// 반드시 Grid 에 바인딩을 하고 스타일을 정의.
		private void SetGridStyle(int rowCount, int colCount)
		{			
			// 각 컬럼의 Format 형식 또는 Align 정의
			for ( int i = 1 ; i < colCount ; i ++ )
			{
				DataGrid1.Columns[i].Format = "###,###,##0.00";
				DataGrid1.Columns[i].CellStyle.HorizontalAlign = HorizontalAlign.Right;
				DataGrid1.Columns[i].CellStyle.Padding.Right = 7;
			}

			DataGrid1.Rows[0].Style.BackColor = Color.NavajoWhite;
			DataGrid1.Rows[0].Style.Font.Bold = true;
			DataGrid1.Rows[0].Cells[0].Style.HorizontalAlign = HorizontalAlign.Center;
			
			DataGrid1.Columns[0].Width = 100;
		}




		private void DataGrid1_ColumnMove(object sender, Infragistics.WebUI.UltraWebGrid.ColumnEventArgs e)
		{
			this.btnSearch_Click(sender, e);
		}

		private void DataGrid1_SortColumn(object sender, Infragistics.WebUI.UltraWebGrid.SortColumnEventArgs e)
		{
			this.btnSearch_Click(sender, e);
		}




	}
}
