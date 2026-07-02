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

using System.Configuration;
using System.Data.SqlClient;

namespace KIT_ERP.ProductionManagement
{
	/// <summary>
	/// IncongruenceIndex에 대한 요약 설명입니다.
	/// </summary>
	public class IncongruenceIndex : System.Web.UI.Page
	{
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected System.Web.UI.WebControls.Button Button2;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser txtStartDate;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.WebControls.DropDownList dlWorkDivision;
		protected System.Web.UI.WebControls.DropDownList dlProcess;
		protected Infragistics.WebUI.WebCombo.WebCombo wcWCName;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser txtEndDate;
		protected System.Web.UI.WebControls.DropDownList ddlWorker;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			ItemSearchControl1.Products = true;					//제품
			ItemSearchControl1.HalfFinishedProducts = true;		//반제품
			ItemSearchControl1.RawMaterials = true;				//원자재
			ItemSearchControl1.Commodity = true;				//상품
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			
			if(!Page.IsPostBack)
			{
				

				//	*************************************************
				//	**  대표공정 드롭다운리스트... 데이타바인딩... ** 
				//	*************************************************
				//코드분류표에서 대분류명이 공정인 소분류명을 가지고 옴.
				//string str_Process = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and SmallClassificationCode != '14000000' and SmallClassificationCode != '14009999' and RecodingState = 1 and LargeClassificationCode = '1400'" ;
				string str_Process = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '1400' and (SmallClassificationCode != '14000000' and SmallClassificationCode != '14009999')" ;
				SqlCommand comm_Process = new SqlCommand(str_Process,conn);
				SqlDataAdapter da_Process = new SqlDataAdapter(comm_Process) ;
				DataSet ds_Process = new DataSet() ;
				da_Process.Fill(ds_Process);
				
				dlProcess.DataSource = ds_Process;
				dlProcess.DataTextField = ds_Process.Tables[0].Columns[0].ToString();
				dlProcess.DataValueField = ds_Process.Tables[0].Columns[1].ToString();
				dlProcess.DataBind();
				dlProcess.Items.Insert(0, "-선택-") ;
				dlProcess.Items[0].Value = "";



				string str1 = "Select WCName, WCInfoIndex From WCI_MT where RecodingState = 1 order by WCName";
				SqlCommand comm1 = new SqlCommand(str1,conn);
				SqlDataAdapter da1 = new SqlDataAdapter(comm1) ;
				DataSet ds1 = new DataSet() ;
				da1.Fill(ds1);

				wcWCName.DataSource = ds1;
				wcWCName.DataTextField = ds1.Tables[0].Columns[0].ToString();
				wcWCName.DataValueField = ds1.Tables[0].Columns[0].ToString();
				wcWCName.DataBind();

				str1 = "Select ID, Name From UI_MT where RecodingState = 1 and BusinessRegistrationNum = '' order by Name";
				SqlCommand comm_Id = new SqlCommand(str1,conn);
				SqlDataAdapter da_Id = new SqlDataAdapter(comm_Id) ;
				DataSet ds_Id = new DataSet() ;
				da_Id.Fill(ds_Id);
				
				ddlWorker.DataSource = ds_Id;
				ddlWorker.DataTextField = ds_Id.Tables[0].Columns[1].ToString();
				ddlWorker.DataValueField = ds_Id.Tables[0].Columns[0].ToString();
				ddlWorker.DataBind();
				ddlWorker.Items.Insert(0, "-선택-") ;
				ddlWorker.Items[0].Value = "";

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
			this.UltraWebGrid1.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.UltraWebGrid1_PageIndexChanged);
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSearch_Click(object sender, System.EventArgs e)
		{	
			UltraWebGrid1.DataSource = search();
			UltraWebGrid1.DataBind();
		}

		private DataSet search()
		{
			Search search = new Search("IncongruenceIndex",ItemSearchControl1.ItemNum, ItemSearchControl1.ItemDrawNum, ItemSearchControl1.ItemName, dlWorkDivision, wcWCName, dlProcess,txtStartDate,txtEndDate,ddlWorker);
			return search.DataSet_search();
		}

		/// <summary>
		/// 엑셀저장
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button2_Click(object sender, System.EventArgs e)
		{
			if(UltraWebGrid1.Rows.Count == 0)
			{
				RegisterClientScriptBlock("","<script>alert('저장할 항목이 없습니다!');</script>");
			}
			else
			{
				Infragistics.WebUI.UltraWebGrid.UltraWebGrid  uwgExcelImport = new Infragistics.WebUI.UltraWebGrid.UltraWebGrid();
				uwgExcelImport = UltraWebGrid1;
				uwgExcelImport.DisplayLayout.Pager.AllowPaging = false;

				uwgExcelImport.DataSource = search();
				uwgExcelImport.DataBind();

				UltraWebGridExcelExporter1.Export(uwgExcelImport);		
			}		
		}

		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;			
			UltraWebGrid1.DataSource = search();
			UltraWebGrid1.DataBind();
		}
	}
}
