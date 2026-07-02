using System;
using System.Configuration;
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
using Infragistics.WebUI;
using Infragistics.WebUI.WebSchedule;
using Infragistics.WebUI.WebCombo;
using Infragistics.WebUI.UltraWebGrid;

namespace KIT_ERP.BusinessManagement
{
	/// <summary>
	/// AddItemReceivePC에 대한 요약 설명입니다.
	/// </summary>
	public class AddItemReceivePC : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.LinkButton lnk_Update;
		protected System.Web.UI.WebControls.DropDownList dl_ProgressState;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcStartDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcEndDate;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification1;
		protected System.Web.UI.WebControls.Button bt_Search;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Button Button3;
		protected System.Web.UI.WebControls.Button bt_Delete;
		protected System.Web.UI.WebControls.Button bt_Stop;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden tb_TotalCost;
		protected System.Web.UI.HtmlControls.HtmlInputHidden chkAll;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Total;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_RowSelectIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_RowIndex;

		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;
	
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			ItemSearchControl1.HalfFinishedProducts = true; //반제품 바인딩				
			ItemSearchControl1.RawMaterials = true; //원자재 바인딩					
			CSC1.UnitCostDistinction = "수주거래처";

			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}
			if(!Page.IsPostBack)
			{
				bt_Delete.Attributes.Add("onClick", "return OK('선택한 항목을 삭제 ');");
				bt_Stop.Attributes.Add("onClick", "return OK('선택한 항목을 중단 ');");

				//	*************************************************
				//	**  품목분류1 드롭다운리스트... 데이타바인딩... **		
				//	*************************************************
				//코드분류표에서 대분류명이 품목분류1인 소분류명을 가지고 옴.
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				string str_ItemClassification1 = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '0210'" ;
				SqlCommand comm_ItemClassification1 = new SqlCommand(str_ItemClassification1,conn);
				SqlDataAdapter da_ItemClassification1 = new SqlDataAdapter(comm_ItemClassification1) ;
				DataSet ds_ItemClassification1 = new DataSet() ;
				da_ItemClassification1.Fill(ds_ItemClassification1);
				
				ddlItemClassification1.DataSource = ds_ItemClassification1;
				ddlItemClassification1.DataTextField = ds_ItemClassification1.Tables[0].Columns[0].ToString();
				ddlItemClassification1.DataValueField = ds_ItemClassification1.Tables[0].Columns[1].ToString();
				ddlItemClassification1.DataBind();
				ddlItemClassification1.Items.Insert(0, "-선택하세요-") ;
				ddlItemClassification1.Items[0].Value = "";				
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
			this.lnk_Update.Click += new System.EventHandler(this.lnk_Update_Click);
			this.bt_Search.Click += new System.EventHandler(this.bt_Search_Click);
			this.UltraWebGrid1.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.UltraWebGrid1_PageIndexChanged);
			this.Button3.Click += new System.EventHandler(this.Button3_Click);
			this.bt_Delete.Click += new System.EventHandler(this.bt_Delete_Click);
			this.bt_Stop.Click += new System.EventHandler(this.bt_Stop_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void bt_Search_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = 1;
			UltraWebGrid1.Columns[0].AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.Yes;
			UltraWebGrid1.DataSource = Search();
			UltraWebGrid1.DataBind();
			
		}

		private void Button3_Click(object sender, System.EventArgs e)
		{
			if(UltraWebGrid1.Rows.Count == 0)
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('저장할 항목이 없습니다!');");
				Response.Write("</script>");
			}
			else
			{
				UltraWebGrid uwg_RO = new UltraWebGrid();
				uwg_RO = UltraWebGrid1;
				uwg_RO.DisplayLayout.Pager.AllowPaging = false;
				uwg_RO.Columns.FromKey("chk").Hidden = true;
				uwg_RO.DataSource = Search();
				uwg_RO.DataBind();
				UltraWebGridExcelExporter1.Export(uwg_RO);

			}
		}

		private void bt_Delete_Click(object sender, System.EventArgs e)
		{
			if(UltraWebGrid1.Rows.Count == 0)
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('삭제할 항목이 없습니다!');");
				Response.Write("</script>");
			}
			else
			{
				Delete del = new Delete(UltraWebGrid1,"ReceiveingPC");
				del.MainRowDelete();
				
				DataSet ds = Search();
				UltraWebGrid1.DataSource = ds;
				UltraWebGrid1.DataBind();				
			}
			
		}

		private void bt_Stop_Click(object sender, System.EventArgs e)
		{
			if(UltraWebGrid1.Rows.Count == 0)
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('중단할 항목이 없습니다!');");
				Response.Write("</script>");
			}
			else
			{
				Stop stop = new Stop(UltraWebGrid1,"ReceiveingPC",Session["ID"].ToString());
				stop.MainTableStop();

			
				DataSet ds = Search();
				UltraWebGrid1.DataSource = ds;
				UltraWebGrid1.DataBind();				
			}
		}

		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			UltraWebGrid1.DataSource = Search();
			UltraWebGrid1.DataBind();
		}

		/// <summary>
		/// 검색 함수
		/// </summary>
		private DataSet Search()
		{

			Search aa;
			if(ItemSearchControl1.hdItem.Trim() == "")
				aa = new Search("AddItemReceivePC",ItemSearchControl1.ItemNum,ItemSearchControl1.ItemDrawNum,ItemSearchControl1.ItemName,wdcStartDate,wdcEndDate,CSC1.Company, CSC1.BusinessRegistrationNum,dl_ProgressState.SelectedItem.Value,ddlItemClassification1.SelectedItem.Value);
			else
				aa = new Search("AddItemReceivePC",ItemSearchControl1.ItemNum,"","",wdcStartDate,wdcEndDate,CSC1.Company, CSC1.BusinessRegistrationNum,dl_ProgressState.SelectedItem.Value,ddlItemClassification1.SelectedItem.Value);
			return  aa.DataSet_search();
			
		}

		private void lnk_Update_Click(object sender, System.EventArgs e)
		{
			//총수량
			decimal total = decimal.Parse(lb_Total.Value);

			//총금액(총수량*적용단가)
			decimal totalcost = total * decimal.Parse(UltraWebGrid1.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("ApplyUnitCost").Value.ToString()) ;

			ArrayList arr = new ArrayList();
			arr.Add(total); // 총수량
			arr.Add(totalcost);//총금액
			arr.Add(total);//잔량
			arr.Add(int.Parse(lb_RowSelectIndex.Value));//선택된 그리드 인덱스

			if(!MonthClosing())
			{
				Update up = new Update("ReceiveingPC",arr,UltraWebGrid1,Session["ID"].ToString());
				up.MainTableUpdate();			
			}
			else
			{
				RegisterStartupScript("","<script>alert('월마감이 되어 수정이 불가능 합니다!');</script>");
			}

			

			UltraWebGrid1.DataSource = Search();
			UltraWebGrid1.DataBind();
		}


		
		/// <summary>
		/// 월마감 여부를 확인하는 함수
		/// </summary>
		/// <returns></returns>
		private bool MonthClosing()
		{
			int count = int.Parse(lb_RowSelectIndex.Value);
			int year = 0;
			int month = 0;
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select ClosingYear, ClosingMonth From MCI_MT Where AffairDistinction = @Distinction";
			SqlCommand comm =  new SqlCommand(str,conn);
			comm.Parameters.Add("@Distinction",SqlDbType.VarChar).Value = "영업";
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				year = int.Parse(dr[0].ToString());
				month = int.Parse(dr[1].ToString());
			}
			conn.Close();

			
			if(year < DateTime.Parse(UltraWebGrid1.Rows[count].Cells.FromKey("ReceivingOrderDate").Text).Year)
				return true;
			else if(year == DateTime.Parse(UltraWebGrid1.Rows[count].Cells.FromKey("ReceivingOrderDate").Text).Year)
			{
				if(month < DateTime.Parse(UltraWebGrid1.Rows[count].Cells.FromKey("ReceivingOrderDate").Text).Month)
					return true;
				else
					return false;
			}
			else
			{
				return false;
			}		
		}
	}
}
