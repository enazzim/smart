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
	public class AutonomyInspectPC :  QualityInspection_BaseClass
	{
		protected Infragistics.WebUI.WebCombo.WebCombo wcWCName;
		protected Infragistics.WebUI.WebCombo.WebCombo wcProcessName;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser txtStartDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser txtEndDate;
		protected System.Web.UI.WebControls.Button Button2;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			ItemSearchControl1.Products = true;
			ItemSearchControl1.HalfFinishedProducts = true;

			if(!IsPostBack)
			{
				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 품질관리 > 자주검사 현황';</script>");
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


		// 페이지가 처음 로드될때 실행
		private void Load_WebCombo_Bind()
		{
			SqlDataAdapter adap = new SqlDataAdapter("PageLoad_GetDataSource", base.GetConnectionString);
			adap.SelectCommand.CommandType = CommandType.StoredProcedure;
			adap.SelectCommand.Parameters.Add("@Wcname", SqlDbType.Bit).Value = 1;
			//adap.SelectCommand.Parameters.Add("@ItemNum", SqlDbType.Bit).Value = 1;
			adap.SelectCommand.Parameters.Add("@ProcessName", SqlDbType.Bit).Value = 1;

			DataSet ds = new DataSet();
			adap.Fill(ds);
			
			wcWCName.DataSource = ds.Tables[0].DefaultView;
			wcWCName.DataTextField = "작업장 명";
			wcWCName.DataValueField = "WCInfoIndex";

			//wcItemName.DataSource = ds.Tables[1].DefaultView;

			wcProcessName.DataSource = ds.Tables[1].DefaultView;

			this.DataBind();
			
			// 0번 인덱스 컬럼 히든
			wcWCName.Columns[0].Hidden = true;

			wcWCName.Columns[1].Width = 150;

			wcWCName.Columns[2].Width = 130;

			wcWCName.Columns[3].Width = 80;

			// 공정명 컬럼 넓이 130
			wcProcessName.Columns[0].Width = 130;
		}


		//검색 버튼 클릭시
		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			string strProcessName = wcProcessName.DisplayValue;
			string strItemName = ItemSearchControl1.ItemName;//wcItemName.DisplayValue;
			
			string strStartDate = txtStartDate.Text;
			string strEndDate = txtEndDate.Text;
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
									PageNames.자주검사현황
									, strWCName
									, strProcessName
									, strItemName
									, strStartDate
									, strEndDate 
									);

			UltraWebGrid1.DataSource = qis.GetDataSet().Tables[0].DefaultView;
			UltraWebGrid1.DataBind();
		}


		// Excel Export
		private void Button2_Click(object sender, System.EventArgs e)
		{
			if (UltraWebGrid1.Rows.Count > 0)
				UltraWebGridExcelExporter1.Export(UltraWebGrid1);
			else
				Alert("Excel 로 출력될 검색결과가 없습니다.");
		}
		
	}
}
