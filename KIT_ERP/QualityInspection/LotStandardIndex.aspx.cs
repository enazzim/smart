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
	/// LotStandardIndex에 대한 요약 설명입니다.
	/// </summary>
	public class LotStandardIndex :  QualityInspection.QualityInspection_BaseClass
	{
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification1;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification2;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification3;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification4;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid DataGrid1;
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.DropDownList ddlDIV;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.WebControls.Button Button1;
		protected Infragistics.WebUI.UltraWebChart.UltraChart UltraChart1;
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
				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 경영정보 > 로트기준 지표';</script>");
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
			adap.SelectCommand.Parameters.Add("@ReceiveingOrderCompanyName", SqlDbType.Bit).Value = 1;
			
			
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


			ManagementInfomation_Search mis = new ManagementInfomation_Search
				(PageNames.로트기준지표, strItemClassification1, strItemClassification2, strItemClassification3, strItemClassification4,  strDIV, strCompanyName );

			DataTable dt_GridSource =  mis.GetDataSet().Tables[0] ;


			if ( dt_GridSource.Rows.Count > 3 )
			{
				DataGrid1.DataSource = ProcessGridSource( dt_GridSource ).DefaultView;
				DataGrid1.DataBind();
				this.SetGridStyle( dt_GridSource.Rows.Count ,  dt_GridSource.Columns.Count ) ;

				UltraChart1.Data.DataSource = this.ProcessChartSource(dt_GridSource).DefaultView;
				UltraChart1.Data.DataBind();
			}
		}


		private DataTable ProcessGridSource( DataTable dt_GridSource )
		{
			if ( dt_GridSource.Rows.Count > 3 )
			{
				for ( int i = 0 ; i < dt_GridSource.Rows.Count ; i = i + 3 )
				{
					for ( int j = 3 ; j < ( System.DateTime.Today.Month + 4 ) ; j ++ )
					{
						decimal P_Cost = Convert.ToDecimal(dt_GridSource.Rows[i+1][j]);
						decimal S_Cost = Convert.ToDecimal(dt_GridSource.Rows[i+2][j]);

						if ( P_Cost != 0 && S_Cost != 0 )
						{
							dt_GridSource.Rows[i][j] = (decimal)((P_Cost / S_Cost) * 100.00m);
						}
						else
						{
							dt_GridSource.Rows[i][j] = 0.00m;
						}
					}
				}
			}
			return dt_GridSource;
		}


		private DataTable ProcessChartSource( DataTable dt_GridSource )
		{
			for ( int i = 1 ; i < dt_GridSource.Rows.Count ; i ++ )
			{
				dt_GridSource.Rows[i].Delete();
			}
	
			dt_GridSource.AcceptChanges();
			return dt_GridSource;
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

		private void DataGrid1_ColumnMove(object sender, Infragistics.WebUI.UltraWebGrid.ColumnEventArgs e)
		{
			this.btnSearch_Click(sender, e);
		}
	}
}
