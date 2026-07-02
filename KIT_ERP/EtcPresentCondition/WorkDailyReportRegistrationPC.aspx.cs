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
using System.Configuration;

namespace KIT_ERP.EtcPresentCondition
{
	/// <summary>
	/// WorkDailyReportRegistrationPC에 대한 요약 설명입니다.
	/// </summary>
	public class WorkDailyReportRegistrationPC : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DropDownList ddlWorker;
		protected System.Web.UI.WebControls.Button bt_Search;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Button bt_Excel;
		protected System.Web.UI.WebControls.DropDownList dlWCName;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcFromDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcToDate;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
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

			if(!Page.IsPostBack)
			{
				page_Load();
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
			this.bt_Search.Click += new System.EventHandler(this.bt_Search_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void page_Load()
		{


			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			//	*************************************************
			//	**  WC명 드롭다운리스트... 데이타바인딩... **
			//	*************************************************
			//WC정보테이블에서에서 WC명과 WC정보번호를 가지고옴
			string str = "Select WCName, WCInfoIndex from WCI_MT where RecodingState = 1 order by WCName";
			SqlCommand comm = new SqlCommand(str,conn);
			SqlDataAdapter da = new SqlDataAdapter(comm) ;
			DataSet ds = new DataSet() ;
			da.Fill(ds);
				
			dlWCName.DataSource = ds;
			dlWCName.DataTextField = ds.Tables[0].Columns[1].ToString();
			dlWCName.DataValueField = ds.Tables[0].Columns[0].ToString();
			dlWCName.DataBind();
			dlWCName.Items.Insert(0, "-선택-") ;
			dlWCName.Items[0].Value = "";

			str = "Select ID, Name From UI_MT where RecodingState = 1 and BusinessRegistrationNum = '' order by Name";
			SqlCommand comm_Id = new SqlCommand(str,conn);
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

		/// <summary>
		/// 검색버튼
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Search_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = 1;
			UltraWebGrid1.DataSource = search();
			UltraWebGrid1.DataBind();
		}

		private DataSet search()
		{
			KIT_ERP.Search search;
			if(ItemSearchControl1.hdItem.Trim() == "")
				search = new Search("WorkDailyReportRegistrationPC",ItemSearchControl1.ItemNum, ItemSearchControl1.ItemDrawNum, ItemSearchControl1.ItemName,dlWCName,wdcFromDate,wdcToDate, ddlWorker);
			else
				search = new Search("WorkDailyReportRegistrationPC",ItemSearchControl1.ItemNum, "", "",dlWCName,wdcFromDate,wdcToDate, ddlWorker);
			return search.DataSet_search();
			
		}

		/// <summary>
		/// 엑셀저장
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Excel_Click(object sender, System.EventArgs e)
		{
			if(UltraWebGrid1.Rows.Count == 0)
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('저장할 항목이 없습니다!');");
				Response.Write("</script>");
			}
			else
			{
				Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwg_PR = new Infragistics.WebUI.UltraWebGrid.UltraWebGrid();
				uwg_PR = UltraWebGrid1;
				uwg_PR.DisplayLayout.Pager.AllowPaging = false;
				uwg_PR.DataSource = search();
				uwg_PR.DataBind();
				UltraWebGridExcelExporter1.Export(uwg_PR);

			}
		}

		/// <summary>
		/// 페이지 이동
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			UltraWebGrid1.DataSource = search();
			UltraWebGrid1.DataBind();
		}
	}
}
